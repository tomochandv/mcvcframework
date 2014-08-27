using MvcFramework.Logic;
using MvcFramework.Model;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Linq.Dynamic;
using System;

namespace MvcFramework.Web.Controllers
{
    public class SampleController : BaseController
    {
        public ActionResult Sample()
        {
            return View();
        }

        public ActionResult Alert()
        {
            return View();
        }

        public ActionResult Grid()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GridList(int rows = 50, int page = 1, string sidx = "SYS_ID", string sord = "asc")
        {
            List<Model_System_Setup> list = new Loc_SystemSetup().GetSystemSetup();
            int totalCount = list.Count;
            decimal total = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(totalCount) / rows));
            list = list.AsQueryable().OrderBy(sidx + " " + sord).ToList();
            var data = new
            {
                total = total,
                records = totalCount,
                page = page,
                rows = (from e in list
                        select new
                        {
                            SYS_ID = e.SYS_ID,
                            SYS_KEY = e.SYS_KEY
                        }
                       ).Skip((page - 1) * rows).Take(page * rows).ToArray()

            };

            return Json(data);
        }
    }
}

