using MvcFramework.Dac;
using MvcFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcFramework.Logic
{
    public class Loc_Mng008 : Utill
    {
        public dynamic CategoryList()
        {
            List<Model_Category_Info> list = ConvertToList<Model_Category_Info>(new Dac_Com_CommonCode().GetCategoryList());
            return list;
        }

        public dynamic CodeList(int rows, int page, string category, int emp_ref_id)
        {
            List<Model_Code_Info> list = ConvertToList<Model_Code_Info>(new Dac_Com_CommonCode().GetCodeList()).Where(x => x.CATEGORY_ID.Contains(category)).ToList();

            int totalCount = list.Count;
            decimal total = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(totalCount) / rows));
            var data = new
            {
                total = total,
                records = totalCount,
                page = page,
                rows = (from e in list
                        select new
                        {
                            CATEGORY_ID = e.CATEGORY_ID,
                            CODE_ID = e.CODE_ID,
                            ETC_CODE = e.ETC_CODE,
                            CODE_NAME = new Loc_LanguageInfo().GetLanguage(emp_ref_id, e.CODE_NAME_KR, e.CODE_NAME_JP, e.CODE_NAME_EN, e.CODE_NAME_CH),
                            CATEGORY_NAME = e.CATEGORY_NAME,
                            SORT_ORDER = e.SORT_ORDER
                        }
                       ).Skip((page - 1) * rows).Take(page * rows).ToArray()

            };
            return data;
        }

        public dynamic CodeList(string category)
        {
            return ConvertToList<Model_Code_Info>(new Dac_Com_CommonCode().GetCodeList()).Where(x => x.CATEGORY_ID.Contains(category)).ToList();
        }

        public int InsertComCode(string category_id, List<string> etc_code, List<string> ko, List<string> jp, List<string> en, List<string> ch, int emp_ref_id)
        {
            new Dac_Com_CommonCode().DeleteComCode(category_id);
            return new Dac_Com_CommonCode().InsertComCode(category_id, etc_code, ko, jp, en, ch, emp_ref_id);
        }
    }
}
