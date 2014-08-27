using System.Collections.Generic;
using System.Data;
using TomochanLib;

namespace MvcFramework.Dac
{
    public class Dac_Com_Dept_Info :DbHelper
    {
        public DataTable GetDeptAllList()
        {
            string qry = @"SELECT 
	                            A.DEPT_REF_ID, A.UP_DEPT_ID, A.DEPT_LEVEL, A.DEPT_CODE, A.DEPT_NAME, A.USE_YN, B.DEPT_NAME AS UP_DEPT_NAME
                            FROM COM_DEPT_INFO A
	                            LEFT JOIN COM_DEPT_INFO B ON A.UP_DEPT_ID = B.DEPT_REF_ID
	                            ORDER BY DEPT_LEVEL, UP_DEPT_ID";
            return ExcuteToDataSet(qry, null).Tables[0];
        }

        public int InsertDeptInfo(int up_dept_id, string dept_code, string dept_name, int emp_ref_id, string use_yn)
        {
            string qry = @"INSERT INTO COM_DEPT_INFO VALUES(@UP_DEPT_ID, 0, @DEPT_CODE, @DEPT_NAME, @USE_YN, GETDATE(), @EMP_REF_ID, NULL, NULL)";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(up_dept_id, "UP_DEPT_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(dept_code, "DEPT_CODE", SqlDbType.VarChar));
            paramArray.Add(new Parameter(dept_name, "DEPT_NAME", SqlDbType.VarChar));
            paramArray.Add(new Parameter(use_yn, "USE_YN", SqlDbType.VarChar));
            paramArray.Add(new Parameter(emp_ref_id, "EMP_REF_ID", SqlDbType.Int));
            
            return ExcuteNonQuery(qry, paramArray);
        }

        public int UpdateDeptInfo(int up_dept_id, int dept_ref_id, int dept_level, int emp_ref_id)
        {
            string qry = @"UPDATE COM_DEPT_INFO SET 
	                        UP_DEPT_ID = @UP_DEPT_ID , 
	                        DEPT_LEVEL = @DEPT_LEVEL,
	                        UPDATE_DATE = GETDATE(),
	                        UPDATE_ID = @UPDATE_ID
                        WHERE DEPT_REF_ID = @DEPT_REF_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(up_dept_id, "UP_DEPT_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(dept_level, "DEPT_LEVEL", SqlDbType.Int));
            paramArray.Add(new Parameter(emp_ref_id, "UPDATE_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(dept_ref_id, "DEPT_REF_ID", SqlDbType.Int));

            return ExcuteNonQuery(qry, paramArray);
        }

        public int UpdateDeptInfo(int dept_ref_id, string dept_name, int emp_ref_id)
        {
            string qry = @"UPDATE COM_DEPT_INFO SET 
	                        DEPT_NAME = @DEPT_NAME,
	                        UPDATE_ID = @UPDATE_ID, UPDATE_DATE = GETDATE()
                        WHERE DEPT_REF_ID = @DEPT_REF_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(dept_name, "DEPT_NAME", SqlDbType.VarChar));
            paramArray.Add(new Parameter(emp_ref_id, "UPDATE_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(dept_ref_id, "DEPT_REF_ID", SqlDbType.Int));

            return ExcuteNonQuery(qry, paramArray);
        }

        public int UpdateDeptInfoUse(int dept_ref_id, string use_yn, int emp_ref_id)
        {
            string qry = @"UPDATE COM_DEPT_INFO SET 
                             UPDATE_ID = @UPDATE_ID, UPDATE_DATE = GETDATE(),
                             USE_YN = @USE_YN
                        WHERE DEPT_REF_ID = @DEPT_REF_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(use_yn, "USE_YN", SqlDbType.VarChar));
            paramArray.Add(new Parameter(dept_ref_id, "DEPT_REF_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(emp_ref_id, "UPDATE_ID", SqlDbType.Int));

            return ExcuteNonQuery(qry, paramArray);
        }

        public object MaxDeptLevel(int up_dept_id)
        {
            string qry = "SELECT MAX(DEPT_LEVEL) + 1 FROM COM_DEPT_INFO WHERE UP_DEPT_ID = @UP_DEPT_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(up_dept_id, "UP_DEPT_ID", SqlDbType.Int));
            return ExcuteScalar(qry, paramArray);
        }
    }
}
