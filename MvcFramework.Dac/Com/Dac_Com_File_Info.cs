using System;
using System.Collections.Generic;
using System.Data;
using TomochanLib;

namespace MvcFramework.Dac
{
    public class Dac_Com_File_Info : DbHelper
    {
        public DataTable GetFileList(string id)
        {
            string qry = @"SELECT * FROM COM_FILE_INFO WHERE FILE_ID = @FILE_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(id, "FILE_ID", SqlDbType.VarChar));
            return ExcuteToDataSet(qry, paramArray).Tables[0];
        }

        public DataTable GetFileList(int idx)
        {
            string qry = @"SELECT * FROM COM_FILE_INFO WHERE FILE_IDX = @FILE_IDX";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(idx, "FILE_IDX", SqlDbType.Int));
            return ExcuteToDataSet(qry, paramArray).Tables[0];
        }

        public int InsertFile(string id, string type, string ori_name, string name, string folder, string url, int emp_ref_id)
        {
            string qry = @"INSERT INTO COM_FILE_INFO VALUES(@FILE_ID, @FILE_NAME, @FILE_ORI_NAME, @FILE_TYPE, @FILE_FORDER, @FILE_URL, @CREATEDATE, @CREATE_USER)";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(id, "FILE_ID", SqlDbType.VarChar));
            paramArray.Add(new Parameter(name, "FILE_NAME", SqlDbType.VarChar));
            paramArray.Add(new Parameter(ori_name, "FILE_ORI_NAME", SqlDbType.VarChar));
            paramArray.Add(new Parameter(type, "FILE_TYPE", SqlDbType.VarChar));
            paramArray.Add(new Parameter(folder, "FILE_FORDER", SqlDbType.VarChar));
            paramArray.Add(new Parameter(url, "FILE_URL", SqlDbType.VarChar));
            paramArray.Add(new Parameter(DateTime.Now, "CREATEDATE", SqlDbType.DateTime));
            paramArray.Add(new Parameter(emp_ref_id, "CREATE_USER", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }

        public int DeleteFile(int idx)
        {
            string qry = "DELETE FROM COM_FILE_INFO WHERE FILE_IDX = @FILE_IDX";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(idx, "FILE_IDX", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }
    }
}
