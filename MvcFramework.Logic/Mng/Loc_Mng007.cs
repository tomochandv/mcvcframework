using MvcFramework.Dac;
using MvcFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace MvcFramework.Logic
{
    public class Loc_Mng007 : Utill
    {
        public dynamic CategoryList(int rows, int page, string sidx, string sord)
        {
            List<Model_Category_Info> newList = new List<Model_Category_Info>();
            List<Model_Category_Info> list = ConvertToList<Model_Category_Info>(new Dac_Com_CommonCode().GetCategoryList());
            List<Model_Code_Info> subList = ConvertToList<Model_Code_Info>(new Dac_Com_CommonCode().GetCodeList());

            foreach(var model in list)
            {
                Model_Category_Info item = new Model_Category_Info();
                item.CATEGORY_ID = model.CATEGORY_ID;
                item.CATEGORY_NAME = model.CATEGORY_NAME;
                item.CATEGORY_DESC = model.CATEGORY_DESC;
                item.CodeInfoList = subList.Where(x => x.CATEGORY_ID == model.CATEGORY_ID).ToList();
                newList.Add(item);
            }


            int totalCount = newList.Count;
            decimal total = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(totalCount) / rows));
            newList = newList.AsQueryable().OrderBy(sidx + " " + sord).ToList();
            var data = new
            {
                total = total,
                records = totalCount,
                page = page,
                rows = (from e in newList
                        select new
                        {
                            CATEGORY_ID = e.CATEGORY_ID,
                            CATEGORY_NAME = e.CATEGORY_NAME,
                            CATEGORY_DESC = e.CATEGORY_DESC,
                            CODE_COUNT = e.CodeInfoList.Count
                        }
                       ).Skip((page - 1) * rows).Take(page * rows).ToArray()

            };
            return data;
        }

        public int CheckCategoryId(string category_id)
        {
            return ConvertToList<Model_Category_Info>(new Dac_Com_CommonCode().GetCategoryList()).Where(x=>x.CATEGORY_ID == category_id).ToList().Count();
        }

        public int InsertCategory(string category_id, string category_name, string category_desc, int create_id)
        {
            return new Dac_Com_CommonCode().InsertCategory(category_id, category_name, category_desc, create_id);
        }

        public int UpdateCategory(List<string> ids, List<string> names, List<string> desc, int create_id)
        {
            return new Dac_Com_CommonCode().UpdateCategory(ids, names, desc, create_id);
        }

        public int DeleteCategory(List<string> ids)
        {
            return new Dac_Com_CommonCode().DeleteCategory(ids);
        }

        public int CheckCategory(string id)
        {
            return ConvertObjToInt(new Dac_Com_CommonCode().CheckCategory(id));
        }

      
    }
}
