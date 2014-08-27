using MvcFramework.Dac;
using MvcFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MvcFramework.Logic
{
    public class Loc_Mng009 : Utill
    {
        public dynamic DeptAllListForTree()
        {
            List<Menu_Tree_Info> list = new List<Menu_Tree_Info>();
            List<Model_Dept_Info> deptList = ConvertToList<Model_Dept_Info>(new Dac_Com_Dept_Info().GetDeptAllList()).Where(x => x.USE_YN == "Y").ToList();
            foreach (var data in deptList)
            {
                Menu_Tree_Info menu = new Menu_Tree_Info();
                menu.key = data.DEPT_REF_ID;
                menu.up_key = data.UP_DEPT_ID;
                menu.title = data.DEPT_NAME;
                var children = from p in deptList
                               where data.UP_DEPT_ID == menu.key
                               select p;
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
                isFolder = (GetTree(list, x.key).Count > 0 || x.up_key == 0) ? true : false
            }).ToList();
        }

        public int InsertDeptInfo(int up_dept_id, string dept_code, string dept_name, int emp_ref_id, string use_yn)
        {
            return new Dac_Com_Dept_Info().InsertDeptInfo(up_dept_id, dept_code, dept_name, emp_ref_id, use_yn);
        }

        public int UpdateDeptInfo(string stringJson, int emp_ref_id)
        {
            int row = 0;
            List<Menu_Tree_Info> list = new JavaScriptSerializer().Deserialize<List<Menu_Tree_Info>>(stringJson);
            foreach (var data in list)
            {
                row += NodeUpdate(data.key, data.children, row, emp_ref_id);
            }
            return row;
        }

       
        private int NodeUpdate(int up_key, IEnumerable<Menu_Tree_Info> childNode, int row, int emp_ref_id)
        {
            foreach (var data in childNode)
            {
                //update
                row += new Dac_Com_Dept_Info().UpdateDeptInfo(up_key, data.key,  ConvertObjToInt(new Dac_Com_Dept_Info().MaxDeptLevel(up_key)), emp_ref_id);
                //다시돌려
                if (data.children != null)
                {
                    NodeUpdate(data.key, data.children, row, emp_ref_id);
                }
            }
            return row;
        }

        public int UpdateDeptInfoOneNode(int dept_ref_id, string dept_name, int emp_ref_id)
        {
            return new Dac_Com_Dept_Info().UpdateDeptInfo(dept_ref_id, dept_name, emp_ref_id);
        }

        public int UpdateDeptInfoUseYn(int dept_ref_id, string useYn, int emp_ref_id)
        {
            return new Dac_Com_Dept_Info().UpdateDeptInfoUse(dept_ref_id, useYn, emp_ref_id);
        }

    }
}
