using Big.Data.Web.Services;
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
    private readonly BuildService _buildService;

    static SalesOrderController()
    {
        SalesOrder.Meta.Factory.OrderByKey = true;
    }

    public SalesOrderController(BuildService buildService) => _buildService = buildService;

    protected override IEnumerable<SalesOrder> Search(Pager p)
    {
        // 禁止非索引字段排序
        if (!p.Sort.EqualIgnoreCase("", "ID", "Number")) p.Sort = null;

        return SalesOrder.Search(p["dtStart"].ToDateTime(), p["dtEnd"].ToDateTime(), p["Q"], p);
    }

    [EntityAuthorize(PermissionFlags.Insert)]
    public ActionResult BuildData()
    {
        var task = Task.Run(() =>
        {
            // 根据批大小生成100万数据，并插入数据库
            var batchSize = XCodeSetting.Current.BatchSize;
            var count = 1_000_000;

            // 管理员放大100倍
            if (ManageProvider.User.Roles.Any(e => e.IsSystem)) count *= 100;

            _buildService.Build(count, batchSize);
        });

        // 稍微等待一下，让后台线程有机会执行
        task.Wait(1000);

        return Json(0, task.IsCompleted ? "完成" : "后台任务正在执行");
    }

    [EntityAuthorize(PermissionFlags.Insert)]
    public ActionResult BuildData2()
    {
        var task = Task.Run(() =>
        {
            // 根据批大小生成100万数据，并插入数据库
            var batchSize = XCodeSetting.Current.BatchSize;
            var count = 1_000_000;

            // 管理员放大100倍
            if (ManageProvider.User.Roles.Any(e => e.IsSystem)) count *= 100;

            _buildService.ActorBuild(count, batchSize);
        });

        // 稍微等待一下，让后台线程有机会执行
        task.Wait(1000);

        return Json(0, task.IsCompleted ? "完成" : "后台任务正在执行");
    }
}