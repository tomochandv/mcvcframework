using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TomochanLib;

namespace MvcFramework.Dac
{
    public class Dac_Com_System_Setup : DbHelper
    {
        public DataTable GetSystemSetup()
        {
            string qry = @"SELECT * FROM COM_SYSTEM_SETUP ORDER BY SYS_ID";
            return ExcuteToDataSet(qry, null).Tables[0];
        }

        public int DeleteSystemSetup(int sys_id)
        {
            string qry = @"DELETE FROM COM_SYSTEM_SETUP WHERE SYS_ID = @SYS_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(sys_id, "SYS_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }

        public int UpdateSystemSetup(int sys_id, string sys_value, string sys_desc)
        {
            string qry = "UPDATE COM_SYSTEM_SETUP SET SYS_VALUE = @SYS_VALUE, SYS_DESC = @SYS_DESC WHERE SYS_ID = @SYS_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(sys_id, "SYS_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(sys_value, "SYS_VALUE", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(sys_desc, "SYS_DESC", SqlDbType.NVarChar));
            return ExcuteNonQuery(qry, paramArray);
        }

        public int InsertSystemSetup(string sys_key, string sys_value, string sys_desc, int emp_ref_id)
        {
            string qry = "INSERT INTO COM_SYSTEM_SETUP VALUES(@SYS_KEY, @SYS_VALUE, @SYS_DESC , GETDATE(), @EMP_REF_ID, null, null)";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(sys_key, "SYS_KEY", SqlDbType.VarChar));
            paramArray.Add(new Parameter(sys_value, "SYS_VALUE", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(sys_desc, "SYS_DESC", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(emp_ref_id, "EMP_REF_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }
    }
}
