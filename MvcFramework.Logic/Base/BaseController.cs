using MvcFramework.Logic;
using MvcFramework.Model;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace MvcFramework.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnAuthorization(AuthorizationContext context)
        {
            FormsIdentity id = (FormsIdentity)context.HttpContext.User.Identity;
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                AUser();
                ViewBag.system_type = new Loc_SystemSetup().GetSetupValue("SYSTEM_TYPE");
            }
            else
            {
                context.Result = new RedirectResult("/Login");
            }

        }

        /// <summary>
        /// 사용자 정보 알아오기
        /// </summary>
        /// <returns></returns>
        public Model_User_Info AUser()
        {
            Model_User_Info info = new Model_User_Info();
            FormsIdentity id = (FormsIdentity)User.Identity;
            if (id.IsAuthenticated)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                info = serializer.Deserialize<Model_User_Info>(id.Ticket.UserData);
                ViewBag.Name = info.EMP_NAME;
                ViewBag.Emp_ref_id = info.EMP_REF_ID;
            }
            return info;
        }

        /// <summary>
        /// 언어가져오기
        /// </summary>
        /// <param name="id">아이디</param>
        /// <param name="txt">한글</param>
        /// <returns></returns>
        public string GetLanguage(int id, string txt)
        {
            string lan = new Loc_LanguageInfo().GetLanguage(id);
            return lan == "" ? txt : lan;
        }
    }

    public class DownloadResult : ActionResult
    {
        public string FileName { get; set; }
        public string Path { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Buffer = true;
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
            context.HttpContext.Response.ContentType = "application/unknown";   // 모든 파일 강제 다운로드
            context.HttpContext.Response.WriteFile(context.HttpContext.Server.MapPath(Path));
        }
    }
}
