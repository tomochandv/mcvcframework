
using System.Collections.Generic;
namespace MvcFramework.Model
{
    public class Model_User_Menu
    {
        public int ROLE_ID { get; set; }
        public int MENU_ID { get; set; }
        public int UP_MENU_ID { get; set; }
        public string MENU_NAME_KO { get; set; }
        public string MENU_NAME_EN { get; set; }
        public string MENU_NAME_JP { get; set; }
        public string MENU_NAME_CH { get; set; }
        public string PAGE_URL { get; set; }
        public string MENU_TREE { get; set; }
        public int SORT_ORDER { get; set; }
    }

    public class Menu_Tree_Info
    {
        public string title { get; set; }
        public int key { get; set; }
        public int up_key { get; set; }
        public bool isFolder { get; set; }
        public string href { get; set; }
        public bool noLink { get; set; }
        public IEnumerable<Menu_Tree_Info> children { get; set; }
    }
}
