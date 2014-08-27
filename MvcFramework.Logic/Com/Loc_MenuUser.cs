using MvcFramework.Dac;
using MvcFramework.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MvcFramework.Logic
{
    public class Loc_MenuUser
    {
        public List<Model_User_Menu> GetMenuList(int emp_ref_id)
        {
            DataTable dt = new Dac_RoleMenu().GetUserMenu(emp_ref_id);
            List<Model_User_Menu> info = new Utill().ConvertToList<Model_User_Menu>(dt);
            return info;
        }

        public List<Model_User_Menu> GetMenuList(int emp_ref_id, int up_menu_id)
        {
            DataTable dt = new Dac_RoleMenu().GetUserMenu(emp_ref_id);
            List<Model_User_Menu> info = new Utill().ConvertToList<Model_User_Menu>(dt).Where(p=> p.MENU_ID == up_menu_id || p.UP_MENU_ID == up_menu_id).ToList();
            return info;
        }
        public List<Model_User_Menu> GetMenuListForTree(int emp_ref_id, int up_menu_id)
        {
            DataTable dt = new Dac_RoleMenu().GetUserMenu(emp_ref_id);
            List<Model_User_Menu> info = new Utill().ConvertToList<Model_User_Menu>(dt).Where(p => p.MENU_ID == up_menu_id || p.MENU_TREE.Contains(up_menu_id.ToString())).ToList();
            return info;
        }
       
        public Model_Role_Info GetRoleInfo(int emp_ref_id)
        {
            DataTable dt = new Dac_RoleMenu().GetUserMenu(emp_ref_id);
            Model_Role_Info info = new Utill().ConvertToRow<Model_Role_Info>(dt);
            return info;
        }

        public dynamic GetSideMenu(int emp_ref_id)
        {
            List<Menu_Tree_Info> list = new List<Menu_Tree_Info>();
            List<Model_User_Menu> menuList = GetMenuList(emp_ref_id);
            
            foreach(var data in menuList)
            {
                Menu_Tree_Info menu = new Menu_Tree_Info();
                menu.key = data.MENU_ID;
                menu.up_key = data.UP_MENU_ID;
                menu.title = new Loc_LanguageInfo().GetLanguage(emp_ref_id, data.MENU_NAME_KO, data.MENU_NAME_JP, data.MENU_NAME_EN, data.MENU_NAME_CH);
                var children = from p in menuList
                               where data.UP_MENU_ID == menu.key 
                               orderby  p.SORT_ORDER ascending
                               select p;
                menu.isFolder = false;
                menu.href = data.PAGE_URL;
                list.Add(menu);
            }
            var resultJson = GetTree(list, 0);
            return resultJson;
        }

        public dynamic GetSideMenu(int emp_ref_id, int up_menu_id)
        {
            List<Menu_Tree_Info> list = new List<Menu_Tree_Info>();
            List<Model_User_Menu> menuList = GetMenuListForTree(emp_ref_id, up_menu_id).OrderBy(x => x.SORT_ORDER).ToList();

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
                menu.href = data.PAGE_URL + "/" + up_menu_id;
                list.Add(menu);
            }
            var resultJson = GetTree(list, 0);
            return resultJson;
        }

        

        private List<Menu_Tree_Info> GetTree(List<Menu_Tree_Info> list, int parent)
        {
            return list.Where(x => x.up_key == parent).Select(x => new Menu_Tree_Info
            {
                key = x.key,
                title = x.title,
                children = GetTree(list, x.key),
                isFolder = (GetTree(list, x.key).Count > 0 || x.up_key == 0) ? true : false,
                href =  GetTree(list, x.key).Count > 0 ? "" : x.href,
                noLink = GetTree(list, x.key).Count > 0 ? true : false
            }).ToList();
        }

       
    }
}
