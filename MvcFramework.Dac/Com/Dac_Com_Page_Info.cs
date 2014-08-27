using System.Collections.Generic;
using System.Data;
using TomochanLib;

namespace MvcFramework.Dac
{
    public class Dac_Com_Page_Info : DbHelper
    {
        public DataTable GetPageList()
        {
            string qry = @"SELECT * FROM COM_PAGE_INFO";
            return ExcuteToDataSet(qry, null).Tables[0];
        }

        public int InsertPageInfo(string page_name, string url, string img, int emp_ref_id)
        {
            string qry = @"INSERT INTO COM_PAGE_INFO VALUES(@PAGE_NAME_KO, @PAGE_URL, @PAGE_IMG, GETDATE(), @CREATE_ID, NULL, NULL)";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(page_name, "PAGE_NAME_KO", SqlDbType.VarChar));
            paramArray.Add(new Parameter(url, "PAGE_URL", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(img, "PAGE_IMG", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(emp_ref_id, "CREATE_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }

        public int UpdatePageInfo(int page_id, string page_name, string url, string img, int emp_ref_id)
        {
            string qry = @"UPDATE COM_PAGE_INFO SET PAGE_NAME_KO = @PAGE_NAME_KO, PAGE_URL = @PAGE_URL, PAGE_IMG = @PAGE_IMG, UPDATE_DATE = GETDATE(), UPDATE_ID = @CREATE_ID
                           WHERE PAGE_ID = @PAGE_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(page_name, "PAGE_NAME_KO", SqlDbType.VarChar));
            paramArray.Add(new Parameter(url, "PAGE_URL", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(img, "PAGE_IMG", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(emp_ref_id, "CREATE_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(page_id, "PAGE_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }

        public int DeletePageInfo(int page_id)
        {
            string qry = @"DELETE FROM COM_PAGE_INFO WHERE PAGE_ID = @PAGE_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(page_id, "PAGE_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }
    }
}
