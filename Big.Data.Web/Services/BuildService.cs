using NewLife.Log;
using NewLife.Security;
using System.Diagnostics;
using XCode.Membership;
using XCode;
using NewLife.Model;

namespace Big.Data.Web.Services;

public class BuildService
{
    public Int32 Build(Int32 count, Int32 batchSize)
    {
        using var _ = SalesOrder.Meta.Session.Dal.Session.SetShowSql(false);
        var sw = Stopwatch.StartNew();

        var actions = new[] { "入库", "出库", "退回", "调拨", "盘点" };

        var times = count / batchSize;
        XTrace.WriteLine("准备生成{0:n0}条数据，分{1:n0}批，批大小{2:n0}", count, times, batchSize);

        var rs = 0;
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
            rs += list.Insert();
        }

        sw.Stop();

        var msg = $"生成{count:n0}条数据，耗时{sw.Elapsed}，速度{count * 1000 / sw.ElapsedMilliseconds:n0}tps";
        XTrace.WriteLine(msg);
        LogProvider.Provider.WriteLog(typeof(SalesOrder), "BuildData", true, msg);

        return rs;
    }

    public Int32 ActorBuild(Int32 count, Int32 batchSize)
    {
        var sw = Stopwatch.StartNew();

        var actions = new[] { "入库", "出库", "退回", "调拨", "盘点" };

        var times = count / batchSize;
        XTrace.WriteLine("准备生成{0:n0}条数据，分{1:n0}批，批大小{2:n0}", count, times, batchSize);

        // 并行生成数据，每批送入Actor处理
        var actor = new BuildActor();
        Parallel.For(0, times, i =>
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
            actor.Tell(list);
        });
        XTrace.WriteLine("数据生成完毕，等待处理");

        actor.Stop(60_000);

        sw.Stop();

        var msg = $"生成{count:n0}条数据，耗时{sw.Elapsed}，速度{count * 1000 / sw.ElapsedMilliseconds:n0}tps";
        XTrace.WriteLine(msg);
        LogProvider.Provider.WriteLog(typeof(SalesOrder), "BuildData", true, msg);

        return actor.Result;
    }

    class BuildActor : Actor
    {
        public Int32 Result { get; set; }

        private IDisposable _showSql;

        protected override Task ReceiveAsync(ActorContext context, CancellationToken cancellationToken)
        {
            // 关闭SQL日志
            using var _ = SalesOrder.Meta.Session.Dal.Session.SetShowSql(false);

            var list = context.Message as List<SalesOrder>;
            Result += list.Insert();

            return base.ReceiveAsync(context, cancellationToken);
        }
    }
}
