using Microsoft.AspNetCore.Mvc;
using NewLife;
using NewLife.Cube;
using NewLife.Log;
using NewLife.Security;
using NewLife.Web;
using System.Diagnostics;
using XCode;
using XCode.Membership;

namespace Big.Data.Web.Areas.Data.Controllers;

[DataArea]
public class SalesOrderController : EntityController<SalesOrder>
{
    static SalesOrderController()
    {
        SalesOrder.Meta.Factory.OrderByKey = true;
    }

    protected override IEnumerable<SalesOrder> Search(Pager p)
    {
        // 禁止非索引字段排序
        if (!p.Sort.EqualIgnoreCase("", "ID", "Number")) p.Sort = null;

        return SalesOrder.Search(p["dtStart"].ToDateTime(), p["dtEnd"].ToDateTime(), p["Q"], p);
    }

    [EntityAuthorize(PermissionFlags.Insert)]
    public ActionResult BuildData()
    {
        try
        {
            var task = Task.Run(() =>
            {
                using var _ = SalesOrder.Meta.Session.Dal.Session.SetShowSql(false);
                var sw = Stopwatch.StartNew();

                var actions = new[] { "入库", "出库", "退回", "调拨", "盘点" };

                // 根据批大小生成100万数据，并插入数据库
                var batchSize = XCodeSetting.Current.BatchSize;
                var count = 1_000_000;
                var times = count / batchSize;
                XTrace.WriteLine("准备生成{0:n0}条数据，分{1:n0}批，批大小{2:n0}", count, times, batchSize);

                for (var i = 0; i < times; i++)
                {
                    var list = new List<SalesOrder>();
                    for (var j = 0; j < batchSize; j++)
                    {
                        var entity = new SalesOrder
                        {
                            Number = Rand.NextString(16),
                            NodeId = Rand.Next(1, 10000),
                            Action = actions[Rand.Next(actions.Length)],
                            CreateTime = DateTime.Now,
                        };
                        list.Add(entity);
                    }
                    list.Insert();
                }

                sw.Stop();

                var msg = $"生成{count:n0}条数据，耗时{sw.Elapsed}，速度{count * 1000 / sw.ElapsedMilliseconds:n0}tps";
                XTrace.WriteLine(msg);
                LogProvider.Provider.WriteLog(typeof(SalesOrder), "BuildData", true, msg);
            });

            // 稍微等待一下，让后台线程有机会执行
            task.Wait(1000);

            return Json(0, task.IsCompleted ? "完成" : "后台任务正在执行");
        }
        catch (Exception ex)
        {
            return Json(500, ex.Message);
        }
    }
}