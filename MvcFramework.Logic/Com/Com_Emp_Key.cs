using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcFramework.Dac;
using MvcFramework.Model;

namespace MvcFramework.Logic
{
    public class Com_Emp_Key : Utill
    {
        public dynamic GetUserKey(int emp_ref_id)
        {
            List<Model_Emp_Key> list = ConvertToList<Model_Emp_Key>(new Dac_Com_Emp_Key().GetUserKey(emp_ref_id));
            var result = (from p in list
                          select new {
                              EMP_REF_ID = p.EMP_REF_ID,
                              MENU_ID = p.MENU_ID,
                              KEY_CODE = p.KEY_CODE,
                              PAGE_URL  = p.PAGE_URL,
                              UP_MENU_ID = ConvertObjToInt(p.MENU_TREE.Split('/')[1])
                          }).ToArray();

            return result;
        }
    }
}
