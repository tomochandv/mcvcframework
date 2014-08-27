using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcFramework.Model
{
    public class Model_Emp_Key
    {
        public int idx { get; set; }
        public int EMP_REF_ID { get; set; }
        public int MENU_ID { get; set; }
        public int KEY_CODE { get; set; }
        public string PAGE_URL { get; set; }
        public int UP_MENU_ID { get; set; }
        public string MENU_TREE { get; set; }
        public string MENU_NAME_KO { get; set; }
        public string MENU_NAME_JP { get; set; }
        public string MENU_NAME_EN { get; set; }
        public string MENU_NAME_CH { get; set; }
    }
}
