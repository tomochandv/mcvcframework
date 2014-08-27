using MvcFramework.Logic;
using MvcFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcFramework.Web.Controllers
{
    public class WebbaseController : BaseController
    {
        public JsonResult Menu()
        {
            List<Model_User_Menu> model = new Loc_MenuUser().GetMenuList(AUser().EMP_REF_ID);
            return Json(model.ToList());
        }

        public JsonResult Role()
        {
            Model_Role_Info model = new Loc_MenuUser().GetRoleInfo(AUser().EMP_REF_ID);
            return Json(model);
        }

        public JsonResult SMenu(int id = 0)
        {
            return Json(new Loc_MenuUser().GetSideMenu(AUser().EMP_REF_ID, id));
        }

        [HttpPost]
        public RedirectResult TopMenuRedirect(int topMenuId = 0, string topMenuUrl = "/Home/Index")
        {
            return Redirect(topMenuUrl + "/" + topMenuId);
        }

        public JsonResult UserKey()
        {
            return Json(new Com_Emp_Key().GetUserKey(AUser().EMP_REF_ID));
        }
    }
}
