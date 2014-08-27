using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomochanLib;

namespace MvcFramework.Dac
{
    public class Dac_Com_Emp_Key : DbHelper
    {
        public DataTable GetUserKey(int emp_ref_id)
        {
            string qry = @"SELECT A.EMP_REF_ID, A.MENU_ID, A.KEY_CODE, C.PAGE_URL , B.MENU_TREE , B.MENU_NAME_KO , B.MENU_NAME_EN, B.MENU_NAME_JP, B.MENU_NAME_CH
                            FROM COM_EMP_KEY A 
	                            INNER JOIN COM_MENU_INFO B ON A.MENU_ID = B.MENU_ID
	                            INNER JOIN COM_PAGE_INFO C ON B.PAGE_ID = C.PAGE_ID
                            WHERE A.EMP_REF_ID = @EMP_REF_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(emp_ref_id, "EMP_REF_ID", SqlDbType.Int));
            return ExcuteToDataSet(qry, paramArray).Tables[0];
        }

        public int InsertKey(int menu_id, int key_code, int emp_ref_id)
        {
            string qry = "INSERT INTO COM_EMP_KEY VALUES(@EMP_REF_ID, @MENU_ID, @KEY_CODE)";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(emp_ref_id, "EMP_REF_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(key_code, "KEY_CODE", SqlDbType.Int));
            paramArray.Add(new Parameter(menu_id, "MENU_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }

        public int DeleteKey(int key_code, int emp_ref_id)
        {
            string qry = "DELETE FROM COM_EMP_KEY WHERE KEY_CODE = @KEY_CODE AND EMP_REF_ID = @EMP_REF_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(emp_ref_id, "EMP_REF_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(key_code, "KEY_CODE", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }
    }
}
