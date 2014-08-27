using MvcFramework.Dac;
using MvcFramework.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MvcFramework.Logic
{
    public class Loc_Mng010 : Utill
    {
        private List<Model_Emp_Key> GetUserKey(int emp_ref_id)
        {
            List<Model_Emp_Key> list = ConvertToList<Model_Emp_Key>(new Dac_Com_Emp_Key().GetUserKey(emp_ref_id));
            return list;
        }

        public dynamic GetUserKeyGrid(int rows, int page, int emp_ref_id)
        {
            List<Model_Emp_Key> list = GetUserKey(emp_ref_id);
            for(int i=0; i < 10; i++)
            {
                bool exsist = false;
                foreach(var data in list)
                {
                    exsist = data.KEY_CODE == i ? true : false;
                    if (exsist)
                    {
                        break;
                    }
                }

                if(!exsist)
                {
                    Model_Emp_Key model = new Model_Emp_Key();
                    model.KEY_CODE = i;
                    model.MENU_ID = 0;
                    list.Add(model);
                }
            }

            int totalCount = list.ToList().Count;
            decimal total = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(totalCount) / rows));
            string lanType = new Loc_LanguageInfo().GetLanguageType();
            var result = new
            {
                total = total,
                records = totalCount,
                page = page,
                rows = (from e in list.OrderBy(x => x.KEY_CODE)
                        select new
                        {
                           EMP_REF_ID = e.EMP_REF_ID,
                           MENU_NAME = new Loc_LanguageInfo().GetLanguage(emp_ref_id, e.MENU_NAME_KO, e.MENU_NAME_JP, e.MENU_NAME_EN, e.MENU_NAME_CH),
                           KEY_CODE = e.KEY_CODE,
                           MENU_ID = e.MENU_ID
                        }
                       ).Skip((page - 1) * rows).Take(page * rows).ToArray()
            };
            return result;
        }

        public List<Model_User_Menu> GetOptions(int emp_Ref_id)
        {
            DataTable dt = new Dac_RoleMenu().GetUserMenu(emp_Ref_id);
            List<Model_User_Menu> info = ConvertToList<Model_User_Menu>(dt);
            return info;
        }

        public int InsertKey(List<int> menu_id, List<int> key_code, int emp_ref_id)
        {
            int row = 0;
            Dac_Com_Emp_Key dac = new Dac_Com_Emp_Key();
            for (int i = 0; i < menu_id.Count; i++ )
            {
                dac.DeleteKey(key_code[i], emp_ref_id);
                row += dac.InsertKey(menu_id[i], key_code[i], emp_ref_id);
            }
            return row;
        }
    }
}
