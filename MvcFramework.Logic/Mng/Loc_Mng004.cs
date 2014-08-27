using MvcFramework.Dac;
using MvcFramework.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Script.Serialization;

namespace MvcFramework.Logic
{
    public class Loc_Mng004 : Utill
    {
        public dynamic GetAllMenuTree(int emp_ref_id)
        {
            List<Menu_Tree_Info> list = new List<Menu_Tree_Info>();
            List<Model_User_Menu> menuList = GetMenuListForTree().OrderBy(x => x.SORT_ORDER).ToList(); 

            foreach (var data in menuList)
            {
                Menu_Tree_Info menu = new Menu_Tree_Info();
                menu.key = data.MENU_ID;
                menu.up_key = data.UP_MENU_ID;
                menu.title = new Loc_LanguageInfo().GetLanguage(emp_ref_id, data.MENU_NAME_KO, data.MENU_NAME_JP, data.MENU_NAME_EN, data.MENU_NAME_CH);
                var children = from p in menuList
                               where data.UP_MENU_ID == menu.key
                               orderby p.SORT_ORDER ascending
                               select p;
                menu.isFolder = false;
                list.Add(menu);
            }
            var resultJson = GetTreeNoLink(list, 0);
            return resultJson;
        }

        public List<Model_User_Menu> GetMenuListForTree()
        {
            DataTable dt = new Dac_RoleMenu().GetUserMenu();
            List<Model_User_Menu> info = ConvertToList<Model_User_Menu>(dt);
            return info;
        }

        private List<Menu_Tree_Info> GetTreeNoLink(List<Menu_Tree_Info> list, int parent)
        {
            return list.Where(x => x.up_key == parent).Select(x => new Menu_Tree_Info
            {
                key = x.key,
                title = x.title,
                children = GetTreeNoLink(list, x.key),
                isFolder = (GetTreeNoLink(list, x.key).Count > 0 || x.up_key == 0) ? true : false
            }).ToList();
        }

        public dynamic GetPageList()
        {
            return ConvertToList<Model_Page_Info>(new Dac_Com_Page_Info().GetPageList()).AsQueryable().OrderBy("PAGE_URL asc, PAGE_NAME_KO asc").ToList();
        }

        public dynamic GetUsedOrder(int id)
        {
            return DataTableToJson(new Dac_RoleMenu().GetUsedOrder(id));
        }

        public int InsertMenu(int up_menu_id, int page_id, string menu_name_ko, string menu_name_en, string menu_name_jp, string menu_name_ch, int sort_order, string use_yn, int emp_ref_id)
        {
            Dac_RoleMenu dac = new Dac_RoleMenu();
            string menu_tree = up_menu_id != 0 ?
                string.Format("{0}/{1}", dac.GetMenuInfo(up_menu_id).Rows[0]["MENU_TREE"], up_menu_id) : "0";
            return dac.InsertMenu(up_menu_id, page_id, menu_tree, menu_name_ko, menu_name_en, menu_name_jp, menu_name_ch, sort_order, use_yn, emp_ref_id);
        }

        public dynamic GetMenuPageInfoById(int id)
        {
            return DataTableToJson(new Dac_RoleMenu().GetMenuInfo(id));
        }

        public int UpdateMenu(int menu_id, int up_menu_id, int page_id, string menu_name_ko, string menu_name_en, string menu_name_jp, string menu_name_ch, int sort_order, string use_yn, int emp_ref_id)
        {
            Dac_RoleMenu dac = new Dac_RoleMenu();
            string menu_tree = up_menu_id != 0 ?
                string.Format("{0}/{1}", dac.GetMenuInfo(up_menu_id).Rows[0]["MENU_TREE"], up_menu_id) : "0";
            return dac.UpdateMenu(menu_id, up_menu_id, page_id, menu_tree, menu_name_ko, menu_name_en, menu_name_jp, menu_name_ch, sort_order, use_yn, emp_ref_id);
        }

        public int DeleteMenu(int menu_id)
        {
            return  new Dac_RoleMenu().DeleteMenu(menu_id); 
        }
    }
}
