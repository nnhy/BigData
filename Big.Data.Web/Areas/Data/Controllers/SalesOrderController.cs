using NewLife;
using NewLife.Cube;
using NewLife.Web;

namespace Big.Data.Web.Areas.Data.Controllers;

[DataArea]
public class SalesOrderController : EntityController<SalesOrder>
{
    ///// <summary>列表页视图。子控制器可重载，以传递更多信息给视图，比如修改要显示的列</summary>
    ///// <param name="p"></param>
    ///// <returns></returns>
    //protected override ActionResult IndexView(Pager p)
    //{
    //    // 禁止非索引字段排序
    //    if (!p.Sort.EqualIgnoreCase("", "ID", "Number")) p.Sort = null;

    //    var list = SalesOrder.Search(p["dtStart"].ToDateTime(), p["dtEnd"].ToDateTime(), p["Q"], p);

    //    return View("List", list);
    //}

    protected override IEnumerable<SalesOrder> Search(Pager p)
    {
        // 禁止非索引字段排序
        if (!p.Sort.EqualIgnoreCase("", "ID", "Number")) p.Sort = null;

        return SalesOrder.Search(p["dtStart"].ToDateTime(), p["dtEnd"].ToDateTime(), p["Q"], p);
    }
}