using System.Collections.Generic;
using System.Data;
using TomochanLib;

namespace MvcFramework.Dac
{
    public class Dac_Com_Language_Info : DbHelper
    {
        public DataTable GetMultiLanguage()
        {
            string qry = @"SELECT * FROM COM_LANGUAGE_INFO";
            return ExcuteToDataSet(qry, null).Tables[0];
        }

        public int InsertLanguage(string lang_type, string lang_ko, string lang_en, string lang_jp, string lang_ch, int emp_ref_id)
        {
            string qry = @"INSERT INTO COM_LANGUAGE_INFO VALUES(@LANG_TYPE, @LANG_KO, @LANG_EN, @LANG_JP, @LANG_CH, GETDATE(), @EMP_REF_ID, NULL, NULL)";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(lang_type, "LANG_TYPE", SqlDbType.VarChar));
            paramArray.Add(new Parameter(lang_ko, "LANG_KO", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(lang_en, "LANG_EN", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(lang_jp, "LANG_JP", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(lang_ch, "LANG_CH", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(emp_ref_id, "EMP_REF_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }

        public int UpdateLanguage(int lang_id, string lang_type, string lang_ko, string lang_en, string lang_jp, string lang_ch, int emp_ref_id)
        {
            string qry = @"UPDATE COM_LANGUAGE_INFO SET
                            LANG_TYPE = @LANG_TYPE, LANG_KO = @LANG_KO, LANG_EN = @LANG_EN, LANG_JP = @LANG_JP, LANG_CH = @LANG_CH, UPDATE_DATE = GETDATE(), UPDATE_ID = @EMP_REF_ID
                            WHERE LANG_ID = @LANG_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(lang_type, "LANG_TYPE", SqlDbType.VarChar));
            paramArray.Add(new Parameter(lang_ko, "LANG_KO", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(lang_en, "LANG_EN", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(lang_jp, "LANG_JP", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(lang_ch, "LANG_CH", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(emp_ref_id, "EMP_REF_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(lang_id, "LANG_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }

        public int DeleteLanguage(int lang_id)
        {
            string qry = @"DELETE FROM COM_LANGUAGE_INFO WHERE LANG_ID = @LANG_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(lang_id, "LANG_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }
    }
}
