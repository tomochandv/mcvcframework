using MvcFramework.Dac;
using MvcFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace MvcFramework.Logic
{
    public class Loc_Mng001
    {
        public dynamic GetSystemSetupList(int rows , int page, string sidx, string sord)
        {
            var list = new Utill().ConvertToList<Model_System_Setup>(new Dac_Com_System_Setup().GetSystemSetup());
            int totalCount = list.Count;
            decimal total = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(totalCount) / rows));
            list = list.AsQueryable().OrderBy(sidx + " " + sord).ToList();
            var data = new
            {
                total = total,
                records = totalCount,
                page = page,
                rows = (from e in list
                        select new
                        {
                            SYS_ID = e.SYS_ID,
                            SYS_KEY = e.SYS_KEY,
                            SYS_VALUE = e.SYS_VALUE,
                            SYS_DESC = e.SYS_DESC

                        }
                       ).Skip((page - 1) * rows).Take(page * rows).ToArray()

            };
            return data;
        }

        public int DeleteSystemSetup(List<int> id)
        {
            int row = 0;
            foreach(var data in id)
            {
                row += new Dac_Com_System_Setup().DeleteSystemSetup(data); 
            }
            return row;
        }

        public int UpdateSystemSetup(int sys_id, string sys_value, string sys_desc)
        {
            return new Dac_Com_System_Setup().UpdateSystemSetup(sys_id, sys_value, sys_desc);
        }

        public int InsertSystemSetup(string sys_key, string sys_value, string sys_desc, int emp_ref_id)
        {
            return new Dac_Com_System_Setup().InsertSystemSetup(sys_key, sys_value, sys_desc, emp_ref_id);
        }
    }
}
