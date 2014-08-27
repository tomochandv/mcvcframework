using MvcFramework.Model;
using System.Web.Mvc;

namespace MvcFramework.Logic
{
    public static class HtmlExtension
    {
        /// <summary>
        /// 멀티랭기지 html에서 쓸때
        /// </summary>
        /// <param name="html"></param>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static MvcHtmlString Language(this HtmlHelper html, int id, string text)
        {
            string lang = string.Empty;
            string lan = new Loc_LanguageInfo().GetLanguage(id);
            lang = lan == "" ? text : lan;
            return MvcHtmlString.Create(lang);
        }

        /// <summary>
        /// 해당 유저에대한 렝기지 알려줌
        /// </summary>
        /// <param name="html"></param>
        /// <returns>KO, EN, CH, JP</returns>
        public static MvcHtmlString LanguageType(this HtmlHelper html)
        {
            string lan = new Loc_LanguageInfo().GetLanguageType();
            return MvcHtmlString.Create(lan);
        }

        /// <summary>
        /// 권한버튼
        /// </summary>
        /// <param name="html"></param>
        /// <param name="type">S, A, I, U, D</param>
        /// <param name="id">아이디</param>
        /// <param name="value">보여질 명칭</param>
        /// <returns></returns>
        public static MvcHtmlString Button(this HtmlHelper html, string type, string id, string value)
        {
            string _css = "btn-Info";
           
            switch(type)
            {
                case "S":
                    _css = "btn-primary";
                    break;
                case "A":
                    _css = "btn-info";
                    break;
                case "I":
                    _css = "btn-success";
                    break;
                case "U":
                    _css = "btn-warning";
                    break;
                case "D":
                    _css = "btn-danger";
                    break;
                case "C":
                    _css = "btn-default";
                    break;
            }

            string button = string.Format("<button type=\"button\" class=\"btn {0}\" id=\"{1}\" name=\"{1}\">{2}</button>", _css, id, value);
            button = type != "C" ? AuthButtonCheck(type, html.ViewContext.Controller.ViewBag.Emp_ref_id, button) : button;
            return MvcHtmlString.Create(button);
        }

        /// <summary>
        /// 이미지가 있는 버튼
        /// </summary>
        /// <param name="html"></param>
        /// <param name="type">S, A, I, U, D</param>
        /// <param name="id">아이디</param>
        /// <param name="value">보여질 명칭</param>
        /// <returns></returns>
        public static MvcHtmlString ButtonImg(this HtmlHelper html, string type, string id, string value)
        {
            string _css = "btn-Info";
            string _image = "glyphicon-plus";
            switch (type)
            {
                case "S":
                    _css = "btn-primary";
                    _image = "glyphicon-search";
                    break;
                case "A":
                    _css = "btn-info";
                    _image = "glyphicon-plus";
                    break;
                case "I":
                    _css = "btn-success";
                    _image = "glyphicon-ok";
                    break;
                case "U":
                    _css = "btn-warning";
                    _image = "glyphicon-pencil";
                    break;
                case "D":
                    _css = "btn-danger";
                    _image = "glyphicon-trash";
                    break;
                case "C":
                    _css = "btn-default";
                    _image = "glyphicon-remove";
                    break;
            }
            string button = string.Format("<button type=\"button\" class=\"btn btn-xs {0}\" id=\"{1}\" name=\"{1}\"><span class=\"glyphicon {3}\"></span> {2}</button>", _css, id, value, _image);
            button = type != "C" ? AuthButtonCheck(type, html.ViewContext.Controller.ViewBag.Emp_ref_id, button) : button;
            return MvcHtmlString.Create(button);
        }

        private static string AuthButtonCheck(string type, int emp_ref_id, string html)
        {
            string returnHtml = string.Empty;
            Model_Role_Info model = new Loc_ButtonRole().AuthRole(emp_ref_id);
            switch (type)
            {
                case "S":
                    returnHtml = model.ROLE_SELECT == "Y" ? html : "";
                    break;
                case "A":
                    returnHtml = model.ROLE_INSERT == "Y" ? html : "";
                    break;
                case "I":
                    returnHtml = model.ROLE_INSERT == "Y" ? html : "";
                    break;
                case "U":
                    returnHtml = model.ROLE_UPDATE == "Y" ? html : "";
                    break;
                case "D":
                    returnHtml = model.ROLE_DELETE == "Y" ? html : "";
                    break;
            }
            return returnHtml;
        }
    }
}
