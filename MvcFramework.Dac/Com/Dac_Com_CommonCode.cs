using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomochanLib;

namespace MvcFramework.Dac
{
    public class Dac_Com_CommonCode : DbHelper
    {
        public DataTable GetCategoryList()
        {
            string qry = @"SELECT * FROM COM_CATEGORY_INFO";
            return ExcuteToDataSet(qry, null).Tables[0];
        }

        public DataTable GetCodeList()
        {
            string qry = @"SELECT A.CODE_ID, A.CATEGORY_ID, A.ETC_CODE, A.CODE_NAME_KR, A.CODE_NAME_EN, A.CODE_NAME_JP, A.CODE_NAME_CH, A.CODE_DESC, A.SORT_ORDER, B.CATEGORY_NAME
                            FROM COM_CODE_INFO A INNER JOIN COM_CATEGORY_INFO B ON A.CATEGORY_ID = B.CATEGORY_ID ORDER BY A.CATEGORY_ID, A.SORT_ORDER";
            return ExcuteToDataSet(qry, null).Tables[0];
        }

        public int InsertCategory(string category_id, string category_name, string category_desc, int create_id)
        {
            string qry = @"INSERT INTO COM_CATEGORY_INFO VALUES(@CATEGORY_ID, @CATEGORY_NAME, @CATEGORY_DESC, GETDATE(), @CREATE_ID, NULL, NULL)";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(category_id, "CATEGORY_ID", SqlDbType.VarChar));
            paramArray.Add(new Parameter(category_name, "CATEGORY_NAME", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(category_desc, "CATEGORY_DESC", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(create_id, "CREATE_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }

        public int UpdateCategory(List<string> ids, List<string> names, List<string> desc, int create_id)
        {
            List<string> arrQry = new List<string>();
            List<List<Parameter>> arrParamArray = new List<List<Parameter>>();

            for (var i = 0; i < ids.Count; i++ )
            {
                string qry = @"UPDATE COM_CATEGORY_INFO SET CATEGORY_NAME = @CATEGORY_NAME, CATEGORY_DESC = @CATEGORY_DESC, UPDATE_DATE = GETDATE(), UPDATE_ID = @CREATE_ID  WHERE CATEGORY_ID = @CATEGORY_ID";
                arrQry.Add(qry);
                List<Parameter> paramArray = new List<Parameter>();
                paramArray.Add(new Parameter(ids[i], "CATEGORY_ID", SqlDbType.VarChar));
                paramArray.Add(new Parameter(names[i], "CATEGORY_NAME", SqlDbType.NVarChar));
                paramArray.Add(new Parameter(desc[i], "CATEGORY_DESC", SqlDbType.NVarChar));
                paramArray.Add(new Parameter(create_id, "CREATE_ID", SqlDbType.Int));
                arrParamArray.Add(paramArray);
            }
            return ExcuteNonQueryTran(arrQry, arrParamArray);
        }

        public int DeleteCategory(List<string> ids)
        {
            List<string> arrQry = new List<string>();
            List<List<Parameter>> arrParamArray = new List<List<Parameter>>();

            for (var i = 0; i < ids.Count; i++)
            {
                string qry = @"DELETE FROM COM_CATEGORY_INFO WHERE CATEGORY_ID = @CATEGORY_ID";
                arrQry.Add(qry);
                List<Parameter> paramArray = new List<Parameter>();
                paramArray.Add(new Parameter(ids[i], "CATEGORY_ID", SqlDbType.VarChar));
                arrParamArray.Add(paramArray);
            }
            return ExcuteNonQueryTran(arrQry, arrParamArray);
        }

        public object CheckCategory(string id)
        {
            string qry = "SELECT COUNT(*) FROM COM_CATEGORY_INFO WHERE CATEGORY_ID = @CATEGORY_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(id, "CATEGORY_ID", SqlDbType.VarChar));
            return ExcuteScalar(qry, paramArray);
        }

        public int DeleteComCode(string id)
        {
            string qry = @"DELETE FROM COM_CODE_INFO WHERE CATEGORY_ID = @CATEGORY_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(id, "CATEGORY_ID", SqlDbType.VarChar));
            return ExcuteNonQuery(qry, paramArray);
        }

        public int InsertComCode(string category_id, List<string> etc_code, List<string> ko, List<string> jp, List<string> en, List<string> ch, int emp_ref_id)
        {
            List<string> arrQry = new List<string>();
            List<List<Parameter>> arrParamArray = new List<List<Parameter>>();
            DateTime now = DateTime.Now;
            for (var i = 0; i < etc_code.Count; i++)
            {
                string qry = @"INSERT INTO COM_CODE_INFO
                               (CATEGORY_ID
                               ,ETC_CODE
                               ,CODE_NAME_KR
                               ,CODE_NAME_EN
                               ,CODE_NAME_JP
                               ,CODE_NAME_CH
                               ,SORT_ORDER
                               ,CREATE_DATE
                               ,CREATE_ID)
                         VALUES
                               (@CATEGORY_ID
                               ,@ETC_CODE 
                               ,@CODE_NAME_KR
                               ,@CODE_NAME_EN 
                               ,@CODE_NAME_JP 
                               ,@CODE_NAME_CH 
                               ,@SORT_ORDER 
                               ,@CREATE_DATE
                               ,@CREATE_ID)";
                arrQry.Add(qry);
                List<Parameter> paramArray = new List<Parameter>();
                paramArray.Add(new Parameter(category_id, "CATEGORY_ID", SqlDbType.VarChar));
                paramArray.Add(new Parameter(etc_code[i], "ETC_CODE", SqlDbType.VarChar));
                paramArray.Add(new Parameter(ko[i], "CODE_NAME_KR", SqlDbType.NVarChar));
                paramArray.Add(new Parameter(jp[i], "CODE_NAME_JP", SqlDbType.NVarChar));
                paramArray.Add(new Parameter(en[i], "CODE_NAME_EN", SqlDbType.NVarChar));
                paramArray.Add(new Parameter(ch[i], "CODE_NAME_CH", SqlDbType.NVarChar));
                paramArray.Add(new Parameter(i, "SORT_ORDER", SqlDbType.Int));
                paramArray.Add(new Parameter(now, "CREATE_DATE", SqlDbType.DateTime));
                paramArray.Add(new Parameter(emp_ref_id, "CREATE_ID", SqlDbType.Int));
                arrParamArray.Add(paramArray);
            }
            return ExcuteNonQueryTran(arrQry, arrParamArray);
        }

         

    }
}
