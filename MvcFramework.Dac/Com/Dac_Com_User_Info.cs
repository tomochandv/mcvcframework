using System.Collections.Generic;
using System.Data;
using TomochanLib;

namespace MvcFramework.Dac
{
    public class Dac_Com_User_Info : DbHelper
    {
        public DataTable GetUserInfo()
        {
            string qry = @"SELECT
	                        A.EMP_REF_ID, 
	                        A.LOGINID, 
	                        A.LOGINPWD, 
	                        A.EMP_NAME, 
	                        A.EMP_EMAIL, 
	                        A.DEPT_REF_ID, 
	                        B.DEPT_NAME,
	                        A.POSITION_CLASS_CODE, 
	                        PCC.CODE_NAME_KR AS POSITION_CLASS_NAME_KR, 
	                        PCC.CODE_NAME_EN AS POSITION_CLASS_NAME_EN, 
	                        PCC.CODE_NAME_JP AS POSITION_CLASS_NAME_JP, 
	                        PCC.CODE_NAME_CH AS POSITION_CLASS_NAME_CH,
	                        A.POSITION_GRP_CODE, 
	                        PGC.CODE_NAME_KR AS POSITION_GRP_NAME_KR, 
	                        PGC.CODE_NAME_EN AS POSITION_GRP_NAME_EN, 
	                        PGC.CODE_NAME_JP AS POSITION_GRP_NAME_JP, 
	                        PGC.CODE_NAME_CH AS POSITION_GRP_NAME_CH,
	                        A.POSITION_RANK_CODE, 
	                        PRC.CODE_NAME_KR AS POSITION_RANK_NAME_KR, 
	                        PRC.CODE_NAME_EN AS POSITION_RANK_NAME_EN, 
	                        PRC.CODE_NAME_JP AS POSITION_RANK_NAME_JP, 
	                        PRC.CODE_NAME_CH AS POSITION_RANK_NAME_CH,
	                        A.POSITION_DUTY_CODE, 
	                        PDC.CODE_NAME_KR AS POSITION_DUTY_NAME_KR, 
	                        PDC.CODE_NAME_EN AS POSITION_DUTY_NAME_EN, 
	                        PDC.CODE_NAME_JP AS POSITION_DUTY_NAME_JP, 
	                        PDC.CODE_NAME_CH AS POSITION_DUTY_NAME_CH,
	                        A.EMP_TYPE, 
	                        EMT.CODE_NAME_KR AS EMP_TYPE_NAME_KR, 
	                        EMT.CODE_NAME_EN AS EMP_TYPE_NAME_EN, 
	                        EMT.CODE_NAME_JP AS EMP_TYPE_NAME_JP, 
	                        EMT.CODE_NAME_CH AS EMP_TYPE_NAME_CH,
	                        A.EMP_TELL, 
	                        A.EMP_LAN_TYPE
                        FROM COM_EMP_INFO A
	                        INNER JOIN COM_DEPT_INFO B ON A.DEPT_REF_ID = B.DEPT_REF_ID
	                        LEFT OUTER JOIN (SELECT * FROM COM_CODE_INFO WHERE CATEGORY_ID = 'POS001') PCC ON A.POSITION_CLASS_CODE = PCC.ETC_CODE
	                        LEFT OUTER JOIN (SELECT * FROM COM_CODE_INFO WHERE CATEGORY_ID = 'POS002') PGC ON A.POSITION_GRP_CODE = PGC.ETC_CODE
	                        LEFT OUTER JOIN (SELECT * FROM COM_CODE_INFO WHERE CATEGORY_ID = 'POS003') PRC ON A.POSITION_RANK_CODE = PRC.ETC_CODE
	                        LEFT OUTER JOIN (SELECT * FROM COM_CODE_INFO WHERE CATEGORY_ID = 'POS004') PDC ON A.POSITION_DUTY_CODE = PDC.ETC_CODE
	                        LEFT OUTER JOIN (SELECT * FROM COM_CODE_INFO WHERE CATEGORY_ID = 'EMP001') EMT ON A.EMP_TYPE = EMT.ETC_CODE";
            return ExcuteToDataSet(qry, null).Tables[0];
        }

        public DataTable GetUserInfo(int emp_ref_id)
        {
            string qry = @"SELECT
	                        A.EMP_REF_ID, 
	                        A.LOGINID, 
	                        A.LOGINPWD, 
	                        A.EMP_NAME, 
	                        A.EMP_EMAIL, 
	                        A.DEPT_REF_ID, 
	                        B.DEPT_NAME,
	                        A.POSITION_CLASS_CODE, 
	                        PCC.CODE_NAME_KR AS POSITION_CLASS_NAME_KR, 
	                        PCC.CODE_NAME_EN AS POSITION_CLASS_NAME_EN, 
	                        PCC.CODE_NAME_JP AS POSITION_CLASS_NAME_JP, 
	                        PCC.CODE_NAME_CH AS POSITION_CLASS_NAME_CH,
	                        A.POSITION_GRP_CODE, 
	                        PGC.CODE_NAME_KR AS POSITION_GRP_NAME_KR, 
	                        PGC.CODE_NAME_EN AS POSITION_GRP_NAME_EN, 
	                        PGC.CODE_NAME_JP AS POSITION_GRP_NAME_JP, 
	                        PGC.CODE_NAME_CH AS POSITION_GRP_NAME_CH,
	                        A.POSITION_RANK_CODE, 
	                        PRC.CODE_NAME_KR AS POSITION_RANK_NAME_KR, 
	                        PRC.CODE_NAME_EN AS POSITION_RANK_NAME_EN, 
	                        PRC.CODE_NAME_JP AS POSITION_RANK_NAME_JP, 
	                        PRC.CODE_NAME_CH AS POSITION_RANK_NAME_CH,
	                        A.POSITION_DUTY_CODE, 
	                        PDC.CODE_NAME_KR AS POSITION_DUTY_NAME_KR, 
	                        PDC.CODE_NAME_EN AS POSITION_DUTY_NAME_EN, 
	                        PDC.CODE_NAME_JP AS POSITION_DUTY_NAME_JP, 
	                        PDC.CODE_NAME_CH AS POSITION_DUTY_NAME_CH,
	                        A.EMP_TYPE, 
	                        EMT.CODE_NAME_KR AS EMP_TYPE_NAME_KR, 
	                        EMT.CODE_NAME_EN AS EMP_TYPE_NAME_EN, 
	                        EMT.CODE_NAME_JP AS EMP_TYPE_NAME_JP, 
	                        EMT.CODE_NAME_CH AS EMP_TYPE_NAME_CH,
	                        A.EMP_TELL, 
	                        A.EMP_LAN_TYPE
                        FROM COM_EMP_INFO A
	                        INNER JOIN COM_DEPT_INFO B ON A.DEPT_REF_ID = B.DEPT_REF_ID
	                        LEFT OUTER JOIN (SELECT * FROM COM_CODE_INFO WHERE CATEGORY_ID = 'POS001') PCC ON A.POSITION_CLASS_CODE = PCC.ETC_CODE
	                        LEFT OUTER JOIN (SELECT * FROM COM_CODE_INFO WHERE CATEGORY_ID = 'POS002') PGC ON A.POSITION_GRP_CODE = PGC.ETC_CODE
	                        LEFT OUTER JOIN (SELECT * FROM COM_CODE_INFO WHERE CATEGORY_ID = 'POS003') PRC ON A.POSITION_RANK_CODE = PRC.ETC_CODE
	                        LEFT OUTER JOIN (SELECT * FROM COM_CODE_INFO WHERE CATEGORY_ID = 'POS004') PDC ON A.POSITION_DUTY_CODE = PDC.ETC_CODE
	                        LEFT OUTER JOIN (SELECT * FROM COM_CODE_INFO WHERE CATEGORY_ID = 'EMP001') EMT ON A.EMP_TYPE = EMT.ETC_CODE
                        WHERE A.EMP_REF_ID = @EMP_REF_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(emp_ref_id, "EMP_REF_ID", SqlDbType.Int));
            return ExcuteToDataSet(qry, paramArray).Tables[0];
        }

        public DataTable GetUserInfo(string id)
        {
            string qry = @"SELECT
	                        A.EMP_REF_ID, 
	                        A.LOGINID, 
	                        A.LOGINPWD, 
	                        A.EMP_NAME, 
	                        A.EMP_EMAIL, 
	                        A.DEPT_REF_ID, 
	                        B.DEPT_NAME,
	                        A.POSITION_CLASS_CODE, 
	                        PCC.CODE_NAME_KR AS POSITION_CLASS_NAME_KR, 
	                        PCC.CODE_NAME_EN AS POSITION_CLASS_NAME_EN, 
	                        PCC.CODE_NAME_JP AS POSITION_CLASS_NAME_JP, 
	                        PCC.CODE_NAME_CH AS POSITION_CLASS_NAME_CH,
	                        A.POSITION_GRP_CODE, 
	                        PGC.CODE_NAME_KR AS POSITION_GRP_NAME_KR, 
	                        PGC.CODE_NAME_EN AS POSITION_GRP_NAME_EN, 
	                        PGC.CODE_NAME_JP AS POSITION_GRP_NAME_JP, 
	                        PGC.CODE_NAME_CH AS POSITION_GRP_NAME_CH,
	                        A.POSITION_RANK_CODE, 
	                        PRC.CODE_NAME_KR AS POSITION_RANK_NAME_KR, 
	                        PRC.CODE_NAME_EN AS POSITION_RANK_NAME_EN, 
	                        PRC.CODE_NAME_JP AS POSITION_RANK_NAME_JP, 
	                        PRC.CODE_NAME_CH AS POSITION_RANK_NAME_CH,
	                        A.POSITION_DUTY_CODE, 
	                        PDC.CODE_NAME_KR AS POSITION_DUTY_NAME_KR, 
	                        PDC.CODE_NAME_EN AS POSITION_DUTY_NAME_EN, 
	                        PDC.CODE_NAME_JP AS POSITION_DUTY_NAME_JP, 
	                        PDC.CODE_NAME_CH AS POSITION_DUTY_NAME_CH,
	                        A.EMP_TYPE, 
	                        EMT.CODE_NAME_KR AS EMP_TYPE_NAME_KR, 
	                        EMT.CODE_NAME_EN AS EMP_TYPE_NAME_EN, 
	                        EMT.CODE_NAME_JP AS EMP_TYPE_NAME_JP, 
	                        EMT.CODE_NAME_CH AS EMP_TYPE_NAME_CH,
	                        A.EMP_TELL, 
	                        A.EMP_LAN_TYPE
                        FROM COM_EMP_INFO A
	                        INNER JOIN COM_DEPT_INFO B ON A.DEPT_REF_ID = B.DEPT_REF_ID
	                        LEFT OUTER JOIN (SELECT * FROM COM_CODE_INFO WHERE CATEGORY_ID = 'POS001') PCC ON A.POSITION_CLASS_CODE = PCC.ETC_CODE
	                        LEFT OUTER JOIN (SELECT * FROM COM_CODE_INFO WHERE CATEGORY_ID = 'POS002') PGC ON A.POSITION_GRP_CODE = PGC.ETC_CODE
	                        LEFT OUTER JOIN (SELECT * FROM COM_CODE_INFO WHERE CATEGORY_ID = 'POS003') PRC ON A.POSITION_RANK_CODE = PRC.ETC_CODE
	                        LEFT OUTER JOIN (SELECT * FROM COM_CODE_INFO WHERE CATEGORY_ID = 'POS004') PDC ON A.POSITION_DUTY_CODE = PDC.ETC_CODE
	                        LEFT OUTER JOIN (SELECT * FROM COM_CODE_INFO WHERE CATEGORY_ID = 'EMP001') EMT ON A.EMP_TYPE = EMT.ETC_CODE
                        WHERE A.LOGINID = @LOGINID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(id, "LOGINID", SqlDbType.VarChar));
            return ExcuteToDataSet(qry, paramArray).Tables[0];
        }
    }
}
