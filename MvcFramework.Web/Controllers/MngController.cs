using MvcFramework.Logic;
using MvcFramework.Logic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MvcFramework.Web.Controllers
{
    public class MngController : BaseController
    {

        #region 시스템정보
        public ActionResult Mng001()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Mng001List(int rows = 50, int page = 1, string sidx = "SYS_ID", string sord = "asc")
        {
            return Json(new Loc_Mng001().GetSystemSetupList(rows, page, sidx, sord));
        }

        [HttpPost]
        public JsonResult Mng001Delete(string id)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<int> list = serializer.Deserialize<List<int>>(id);
            int count = new Loc_Mng001().DeleteSystemSetup(list);
            return Json(count);
        }

        [HttpPost]
        public JsonResult Mng001Update(int id, string values, string desc)
        {
            int count = new Loc_Mng001().UpdateSystemSetup(id, values, desc);
            return Json(count);
        }

        [HttpPost]
        public JsonResult Mng001Insert(string key, string values, string desc)
        {
            int count = new Loc_Mng001().InsertSystemSetup(key, values, desc, AUser().EMP_REF_ID);
            return Json(count);
        } 
        #endregion

        #region 언어정보
        public ActionResult Mng002()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Mng002List(int rows = 50, int page = 1, string sidx = "LANG_TYPE", string sord = "asc", string txt="")
        {
            return Json(new Loc_Mng002().GetLanguageList(rows, page, sidx, sord, txt));
        }

        [HttpPost]
        public JsonResult Mng002Insert(string lang_type, string lang_ko, string lang_en, string lang_jp, string lang_ch)
        {
            return Json(new Loc_Mng002().InsertLanguage(lang_type, lang_ko, lang_en, lang_jp, lang_ch, AUser().EMP_REF_ID));
        }

        [HttpPost]
        public JsonResult Mng002Update(int lang_id, string lang_type, string lang_ko, string lang_en, string lang_jp, string lang_ch)
        {
            return Json(new Loc_Mng002().UpdateLanguage(lang_id, lang_type, lang_ko, lang_en, lang_jp, lang_ch, AUser().EMP_REF_ID));
        }

        [HttpPost]
        public JsonResult Mng002Delete(string id)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<int> list = serializer.Deserialize<List<int>>(id);
            int count = new Loc_Mng002().DeleteLanguage(list);
            return Json(count);
        } 
        #endregion

        #region 페이지정보
        public ActionResult Mng003()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Mng003List(int rows = 50, int page = 1, string sidx = "PAGE_URL", string sord = "asc")
        {
            return Json(new Loc_Mng003().GetPageList(rows, page, sidx, sord));
        }

        [HttpPost]
        public JsonResult Mng003Insert(string page_name, string url)
        {
            return Json(new Loc_Mng003().InsertPageInfo(page_name, url, Request.Files, AUser().EMP_REF_ID));
        }

        [HttpPost]
        public JsonResult Mng003Update(int txtId = 0, string page_name = "", string imgname = "", string url = "")
        {
            return Json(new Loc_Mng003().UpdatePageInfo(txtId, page_name, imgname, url, Request.Files, AUser().EMP_REF_ID));
        }

        [HttpPost]
        public JsonResult Mng003Delete(string id)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<int> list = serializer.Deserialize<List<int>>(id);
            int count = new Loc_Mng003().DeletePageInfo(list);
            return Json(count);
        } 
        #endregion

        #region 메뉴관리
        public ActionResult Mng004()
        {
            return View(new Loc_Mng004().GetPageList());
        }

        [HttpPost]
        public JsonResult Mng004TreeList()
        {
            return Json(new Loc_Mng004().GetAllMenuTree(AUser().EMP_REF_ID));
        }

        [HttpPost]
        public JsonResult Mng004CheckOrder(int id)
        {
            return Json(new Loc_Mng004().GetUsedOrder(id));
        }

        [HttpPost]
        public JsonResult Mng004Insert(int up_menu_id, int page_id, string menu_name_ko, string menu_name_en, string menu_name_jp, string menu_name_ch, int sort_order, string use_yn)
        {
            int count = new Loc_Mng004().InsertMenu(up_menu_id, page_id, menu_name_ko, menu_name_en, menu_name_jp, menu_name_ch, sort_order, use_yn, AUser().EMP_REF_ID);
            return Json(count);
        }

        [HttpPost]
        public JsonResult Mng004View(int id)
        {
            return Json(new Loc_Mng004().GetMenuPageInfoById(id));
        }

        [HttpPost]
        public JsonResult Mng004Update(int menu_id, int up_menu_id, int page_id, string menu_name_ko, string menu_name_en, string menu_name_jp, string menu_name_ch, int sort_order, string use_yn)
        {
            int count = new Loc_Mng004().UpdateMenu(menu_id, up_menu_id, page_id, menu_name_ko, menu_name_en, menu_name_jp, menu_name_ch, sort_order, use_yn, AUser().EMP_REF_ID);
            return Json(count);
        }

        [HttpPost]
        public JsonResult Mng004Delete(int menu_id)
        {
            int count = new Loc_Mng004().DeleteMenu(menu_id);
            return Json(count);
        } 
        #endregion

        #region 권한관리
        public ActionResult Mng005()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Mng005List(int rows = 50, int page = 1, string sidx = "ROLE_KO_NAME", string sord = "asc")
        {
            return Json(new Loc_Mng005().RoleInfoList(rows, page, sidx, sord));
        }

        [HttpPost]
        public JsonResult Mng005Insert(string menus, string ko, string jp, string en, string ch, string s, string i, string u, string d)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<int> list = serializer.Deserialize<List<int>>(menus);

            return Json(new Loc_Mng005().CreateRole(ko, jp, en, ch, s, d, u, i, AUser().EMP_REF_ID, list));
        }

        [HttpPost]
        public JsonResult Mng005View(int role_id)
        {
            return Json(new Loc_Mng005().GetRoleMenu(role_id));
        }

        [HttpPost]
        public JsonResult Mng005Update(int role_id, string menus, string ko, string jp, string en, string ch, string s, string i, string u, string d)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<int> list = serializer.Deserialize<List<int>>(menus);

            return Json(new Loc_Mng005().UpdateRoleInfo(role_id, ko, jp, en, ch, s, d, u, i, AUser().EMP_REF_ID, list));
        }

        [HttpPost]
        public JsonResult Mng005CheckUser(int role_id)
        {
            return Json(new Loc_Mng005().CheckCountRoleUser(role_id));
        }

        [HttpPost]
        public JsonResult Mng005Delete(string role_id)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<int> list = serializer.Deserialize<List<int>>(role_id);

            return Json(new Loc_Mng005().DeleteRole(list));
        } 
        #endregion

        #region 사용자권한관리
        public ActionResult Mng006()
        {
            return View(new Loc_Mng006().RoleList());
        }

        [HttpPost]
        public JsonResult Mng006List(int rows = 50, int page = 1, string sidx = "EMP_NAME", string sord = "asc", string dept_name = "", string emp_name = "", int role_id = 0)
        {
            return Json(new Loc_Mng006().RoleInfoList(rows, page, sidx, sord, dept_name, emp_name, role_id));
        }

        [HttpPost]
        public JsonResult Mng006Insert(int role_id, string emp_ref_id)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<int> list = serializer.Deserialize<List<int>>(emp_ref_id);

            return Json(new Loc_Mng006().MatchRole(role_id, list, AUser().EMP_REF_ID));
        }  
        #endregion

        #region 카테고리관리
        public ActionResult Mng007()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Mng007List(int rows = 50, int page = 1, string sidx = "CATEGORY_NAME", string sord = "asc")
        {
            return Json(new Loc_Mng007().CategoryList(rows, page, sidx, sord));
        }

        [HttpPost]
        public JsonResult Mng007Update(string ids, string names, string descc)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<string> listId = serializer.Deserialize<List<string>>(ids);
            List<string> listName = serializer.Deserialize<List<string>>(names);
            List<string> listDesc = serializer.Deserialize<List<string>>(descc);
            return Json(new Loc_Mng007().UpdateCategory(listId, listName, listDesc, AUser().EMP_REF_ID));
        }

        [HttpPost]
        public JsonResult Mng007Check(string id)
        {
            return Json(new Loc_Mng007().CheckCategory(id));
        }

        [HttpPost]
        public JsonResult Mng007Insert(string id, string name, string desc)
        {
            return Json(new Loc_Mng007().InsertCategory(id, name, desc, AUser().EMP_REF_ID));
        }

        [HttpPost]
        public JsonResult Mng007Delete(string id)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<string> listId = serializer.Deserialize<List<string>>(id);
            return Json(new Loc_Mng007().DeleteCategory(listId));
        } 
        #endregion

        #region 코드관리
        public ActionResult Mng008()
        {
            return View(new Loc_Mng008().CategoryList());
        }
        [HttpPost]
        public JsonResult Mng008List(int rows = 50, int page = 1, string category = "")
        {
            return Json(new Loc_Mng008().CodeList(rows, page, category, AUser().EMP_REF_ID));
        }
        [HttpPost]
        public JsonResult Mng008SubList(string category = "")
        {
            return Json(new Loc_Mng008().CodeList(category));
        }

        [HttpPost]
        public JsonResult Mng008Insert(string category, string etc_code, string ko, string jp, string en, string ch)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<string> code = serializer.Deserialize<List<string>>(etc_code);
            List<string> code_ko = serializer.Deserialize<List<string>>(ko);
            List<string> code_jp = serializer.Deserialize<List<string>>(jp);
            List<string> code_en = serializer.Deserialize<List<string>>(en);
            List<string> code_ch = serializer.Deserialize<List<string>>(ch);

            return Json(new Loc_Mng008().InsertComCode(category, code, code_ko, code_jp, code_en, code_ch, AUser().EMP_REF_ID));
        }
        
        #endregion

        public ActionResult Mng009()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Mng009TreeList()
        {
            return Json(new Loc_Mng009().DeptAllListForTree());
        }

        [HttpPost]
        public JsonResult Mng009Insert(int up_dept_id, string dept_code, string dept_name)
        {
            return Json(new Loc_Mng009().InsertDeptInfo(up_dept_id, dept_code, dept_name, AUser().EMP_REF_ID, "Y"));
        }

        [HttpPost]
        public JsonResult Mng009Update(string dict)
        {
            return Json(new Loc_Mng009().UpdateDeptInfo(dict, AUser().EMP_REF_ID));
        }

        [HttpPost]
        public JsonResult Mng009UpdateNode(string dept_name, int dept_ref_id)
        {
            return Json(new Loc_Mng009().UpdateDeptInfoOneNode(dept_ref_id, dept_name, AUser().EMP_REF_ID));
        }

        [HttpPost]
        public JsonResult Mng009UpdateUse(int dept_ref_id)
        {
            return Json(new Loc_Mng009().UpdateDeptInfoUseYn(dept_ref_id, "N", AUser().EMP_REF_ID));
        }

        #region 단축키
        public ActionResult Mng010()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Mng010TreeList(int rows = 50, int page = 1)
        {
            return Json(new Loc_Mng010().GetUserKeyGrid(rows, page, AUser().EMP_REF_ID));
        }

        [HttpPost]
        public JsonResult Mng010MenuOption()
        {
            return Json(new Loc_Mng010().GetOptions(AUser().EMP_REF_ID));
        }

        [HttpPost]
        public JsonResult Mng010Insert(string menu_id, string key_code)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<int> menu_ids = serializer.Deserialize<List<int>>(menu_id);
            List<int> key_codes = serializer.Deserialize<List<int>>(key_code);

            return Json(new Loc_Mng010().InsertKey(menu_ids, key_codes, AUser().EMP_REF_ID));
        } 
        #endregion
    }
}
