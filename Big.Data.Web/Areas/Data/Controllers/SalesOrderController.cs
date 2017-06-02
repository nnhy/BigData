using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Big.Data.Entity;
using NewLife.Cube;
using NewLife.Web;

namespace Big.Data.Web.Areas.Data.Controllers
{
    public class SalesOrderController : EntityController<SalesOrder>
    {
        /// <summary>列表页视图。子控制器可重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override ActionResult IndexView(Pager p)
        {
            // 禁止
            var list = SalesOrder.Search(p["dtStart"].ToDateTime(), p["dtEnd"].ToDateTime(), p["Q"], p);

            return View("List", list);
        }
    }
}