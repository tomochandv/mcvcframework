using MvcFramework.Dac;
using MvcFramework.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace MvcFramework.Logic
{
    public class Loc_Mng006 :  Utill
    {
        public List<Model_Role_Info> RoleList()
        {
            return ConvertToList<Model_Role_Info>( new Dac_RoleMenu().GetRoleInfoList());
        }
        public dynamic RoleInfoList(int rows, int page, string semp, string sord, string dept_name = "", string emp_name = "", int role_id = 0)
        {
            DataTable dt = new Dac_RoleMenu().GetRoleUserList();
            var list = dt.AsEnumerable();
            if (dept_name != "")
            {
                list = list.Where(x => x.Field<string>("DEPT_NAME").ToString().Contains(dept_name));
            }
            if (emp_name != "")
            {
                list = list.Where(x => x.Field<string>("EMP_NAME").ToString().Contains(emp_name));
            }
            if(role_id != 0)
            {
                list = list.Where(x => x.Field<int>("ROLE_ID") == role_id);
            }
            

            if(sord == "desc")
            {
                list = list.OrderByDescending(x => x.Field<string>("EMP_NAME").ToString());
            }

            int totalCount = list.ToList().Count;
            decimal total = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(totalCount) / rows));
            string lanType = new Loc_LanguageInfo().GetLanguageType();
            var data = new
            {
                total = total,
                records = totalCount,
                page = page,
                rows = (from e in list
                        select new
                        {
                            EMP_REF_ID = e.Field<int>("EMP_REF_ID"),
                            EMP_NAME = e.Field<string>("EMP_NAME"),
                            DEPT_REF_ID = e.Field<int>("DEPT_REF_ID"),
                            DEPT_NAME = e.Field<string>("DEPT_NAME"),
                            ROLE_ID = e.Field<int>("ROLE_ID"),
                            ROLE_NAME = lanType  == "KO" ? e.Field<string>("ROLE_KO_NAME") : lanType == "JP" ? e.Field<string>("ROLE_JP_NAME") : lanType == "EN" ? e.Field<string>("ROLE_EN_NAME"): e.Field<string>("ROLE_CH_NAME")
                        }
                       ).Skip((page - 1) * rows).Take(page * rows).ToArray()
            };
            return data;
        }

        public int MatchRole(int role_id, List<int>emp, int emp_ref_id)
        {
            int row = 0;
            if (role_id != 0)
            {
                foreach (var data in emp)
                {
                    new Dac_RoleMenu().DeleteRoleUser(data, role_id);
                    row += new Dac_RoleMenu().InsertRoleUser(data, role_id, emp_ref_id);
                }
            }
            else
            {
                foreach (var data in emp)
                {
                    row += new Dac_RoleMenu().DeleteRoleUser(data);
                }
            }
            return row;
        }
    }
}
