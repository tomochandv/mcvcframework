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
    public class Loc_Mng005 : Utill
    {
        public dynamic RoleInfoList(int rows, int page, string sidx, string sord)
        {
            var list = ConvertToList<Model_Role_Info>( new Dac_RoleMenu().GetRoleInfoList());
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
                            ROLE_ID = e.ROLE_ID,
                            ROLE_KO_NAME = e.ROLE_KO_NAME,
                            ROLE_EN_NAME = e.ROLE_EN_NAME,
                            ROLE_JP_NAME = e.ROLE_JP_NAME,
                            ROLE_CH_NAME = e.ROLE_CH_NAME,
                            ROLE_SELECT = e.ROLE_SELECT,
                            ROLE_INSERT = e.ROLE_INSERT,
                            ROLE_UPDATE = e.ROLE_UPDATE,
                            ROLE_DELETE = e.ROLE_DELETE
                        }
                       ).Skip((page - 1) * rows).Take(page * rows).ToArray()
            };
            return data;
        }

        public int CreateRole(string role_ko_name, string role_jp_name, string role_en_name, string role_ch_name,
            string role_select, string role_delete, string role_update, string role_insert, int emp_ref_id, List<int> menus)
        {
            int row = 0;
            int role_id = InsertRoleInfo(role_ko_name, role_jp_name, role_en_name, role_ch_name, role_select, role_delete, role_update, role_insert, emp_ref_id);
            foreach(var menu_id in menus)
            {
                row += InsertRoleMenu(role_id, menu_id, emp_ref_id);
            }
            return row;
        }
        private int InsertRoleInfo(string role_ko_name, string role_jp_name, string role_en_name, string role_ch_name,
            string role_select, string role_delete, string role_update, string role_insert, int emp_ref_id)
        {
            return int.Parse(new Dac_RoleMenu().InsertRoleInfo(role_ko_name, role_jp_name, role_en_name, role_ch_name,
                role_select, role_delete, role_update, role_insert, emp_ref_id).ToString());
        }

        private int InsertRoleMenu(int role_id, int menu_id, int emp_ref_id)
        {
            return new Dac_RoleMenu().InsertRoleMenu(role_id, menu_id, emp_ref_id);
        }

        public dynamic GetRoleMenu(int role_id)
        {
            Dac_RoleMenu dac = new Dac_RoleMenu();
            DataTable role = dac.GetRoleToid(role_id);
            DataTable menues = dac.GetRoleMenu(role_id);

            Model_Role_Info role_info = ConvertToRow<Model_Role_Info>(role);
            var result = new
            {
                ROLE_KO_NAME = role_info.ROLE_KO_NAME,
                ROLE_EN_NAME = role_info.ROLE_EN_NAME,
                ROLE_JP_NAME = role_info.ROLE_JP_NAME,
                ROLE_CH_NAME = role_info.ROLE_CH_NAME,
                ROLE_SELECT = role_info.ROLE_SELECT,
                ROLE_INSERT = role_info.ROLE_INSERT,
                ROLE_DELETE = role_info.ROLE_DELETE,
                ROLE_UPDATE = role_info.ROLE_UPDATE,
                ROLE_ID = role_info.ROLE_ID,
                MENUES = menues.Rows.Count > 0 ?
                        (from e in menues.AsEnumerable()
                          select new
                          {
                              MENU_ID = e.Field<int>("MENU_ID")
                          }).ToArray()
                          : null
            };
            return result;
        }

        public int UpdateRoleInfo(int role_id, string role_ko_name, string role_jp_name, string role_en_name, string role_ch_name,
            string role_select, string role_delete, string role_update, string role_insert, int emp_ref_id, List<int> menus)
        {
            int row = 0;
            Dac_RoleMenu dac = new Dac_RoleMenu();
            row = dac.UpdateRoleInfo(role_id, role_ko_name, role_jp_name, role_en_name, role_ch_name, role_select, role_delete, role_update, role_insert, emp_ref_id);
            if(menus.Count > 0)
            {
                foreach(var menu_id in menus)
                {
                    dac.DeleteRoleMenu(role_id, menu_id);
                    dac.InsertRoleMenu(role_id, menu_id, emp_ref_id);
                }
            }
            return row;
        }

        public int CheckCountRoleUser(int role_id)
        {
            return ConvertObjToInt(new Dac_RoleMenu().CheckCountRoleUser(role_id));
        }

        public int DeleteRole(List<int> role_id)
        {
            int row = 0;
            foreach(var data in role_id)
            {
                row += new Dac_RoleMenu().DeleteRoleInfo(data);
            }
            return row;
        }
    }
}
