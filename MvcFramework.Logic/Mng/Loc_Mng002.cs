using MvcFramework.Dac;
using MvcFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace MvcFramework.Logic
{
    public class Loc_Mng002 : Utill
    {
        public dynamic GetLanguageList(int rows, int page, string sidx, string sord, string txt)
        {
            var list = ConvertToList<Model_Language_Info>(new Dac_Com_Language_Info().GetMultiLanguage()).Where(x=>
                NullToString(x.LANG_KO).Contains(txt) ||
                NullToString(x.LANG_EN).Contains(txt) ||
                NullToString(x.LANG_JP).Contains(txt) ||
                NullToString(x.LANG_CH).Contains(txt)).ToList();
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
                            LANG_ID = e.LANG_ID,
                            LANG_KO = e.LANG_KO,
                            LANG_EN = e.LANG_EN,
                            LANG_JP = e.LANG_JP,
                            LANG_CH = e.LANG_CH,
                            LANG_TYPE = e.LANG_TYPE
                        }
                       ).Skip((page - 1) * rows).Take(page * rows).ToArray()

            };
            return data;
        }

        public int DeleteLanguage(List<int> id)
        {
            int row = 0;
            foreach (var data in id)
            {
                row += new Dac_Com_Language_Info().DeleteLanguage(data);
            }
            return row;
        }

        public int UpdateLanguage(int lang_id, string lang_type, string lang_ko, string lang_en, string lang_jp, string lang_ch, int emp_ref_id)
        {
            return new Dac_Com_Language_Info().UpdateLanguage(lang_id, lang_type, lang_ko, lang_en, lang_jp, lang_ch, emp_ref_id);
        }

        public int InsertLanguage(string lang_type, string lang_ko, string lang_en, string lang_jp, string lang_ch, int emp_ref_id)
        {
            return new Dac_Com_Language_Info().InsertLanguage(lang_type, lang_ko, lang_en, lang_jp, lang_ch, emp_ref_id);
        }
    }
}
