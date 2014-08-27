using System;
using System.Collections.Generic;
using System.Data;
using TomochanLib;

namespace MvcFramework.Dac
{
    public class Dac_RoleMenu : DbHelper
    {
        public DataTable GetUserMenu(int emp_ref_id)
        {
            string qry = @"SELECT A.ROLE_ID, B.MENU_ID, C.UP_MENU_ID ,C.MENU_NAME_KO,C.MENU_NAME_EN,C.MENU_NAME_JP,C.MENU_NAME_CH,D.PAGE_URL,C.MENU_TREE, SORT_ORDER
                            FROM COM_ROLE_USER A 
                            INNER JOIN COM_ROLE_MENU B ON A.ROLE_ID = B.ROLE_ID
                            INNER JOIN COM_MENU_INFO C ON B.MENU_ID = C.MENU_ID
                            INNER JOIN COM_PAGE_INFO D ON C.PAGE_ID = D.PAGE_ID
                            WHERE C.USE_YN = 'Y' AND A.EMP_REF_ID = @EMP_REF_ID
                            ORDER BY MENU_ID, UP_MENU_ID, SORT_ORDER";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(emp_ref_id, "EMP_REF_ID", SqlDbType.Int));
            return ExcuteToDataSet(qry, paramArray).Tables[0];
        }

        public DataTable GetUserMenu()
        {
            string qry = @"SELECT C.MENU_ID ,D.PAGE_ID, C.UP_MENU_ID ,C.MENU_NAME_KO,C.MENU_NAME_EN,C.MENU_NAME_JP,C.MENU_NAME_CH,D.PAGE_URL,C.MENU_TREE
                            FROM COM_MENU_INFO C 
                            INNER JOIN COM_PAGE_INFO D ON C.PAGE_ID = D.PAGE_ID
                            ORDER BY MENU_ID, UP_MENU_ID, SORT_ORDER";
            return ExcuteToDataSet(qry, null).Tables[0];
        }


        public DataTable GetRoleInfo(int emp_ref_id)
        {
            string qry = @"SELECT A.ROLE_ID, A.EMP_REF_ID, 
                            B.ROLE_SELECT, B.ROLE_DELETE, B.ROLE_UPDATE, B.ROLE_INSERT,
                            B.ROLE_KO_NAME, B.ROLE_EN_NAME, B.ROLE_JP_NAME, B.ROLE_CH_NAME
                            FROM COM_ROLE_USER A INNER JOIN COM_ROLE_INFO B ON A.ROLE_ID = B.ROLE_ID
                            WHERE EMP_REF_ID = @EMP_REF_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(emp_ref_id, "EMP_REF_ID", SqlDbType.Int));
            return ExcuteToDataSet(qry, paramArray).Tables[0];
        }

        public DataTable GetUsedOrder(int id)
        {
            string qry = @"SELECT * FROM COM_MENU_INFO WHERE UP_MENU_ID = @UP_MENU_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(id, "UP_MENU_ID", SqlDbType.Int));
            return ExcuteToDataSet(qry, paramArray).Tables[0];
        }

        public DataTable GetMenuInfo(int id)
        {
            string qry = @"SELECT	C.MENU_NAME_KO AS UP_MENU_NAME_KO, 
		                    C.MENU_NAME_EN AS UP_MENU_NAME_EN, 
		                    C.MENU_NAME_JP AS UP_MENU_NAME_JP, 
		                    C.MENU_NAME_CH AS UP_MENU_NAME_CH, 
		                    A.*, B.* FROM COM_MENU_INFO A 
	                    INNER JOIN COM_PAGE_INFO B ON A.PAGE_ID = B.PAGE_ID 
	                    LEFT JOIN COM_MENU_INFO C ON A.UP_MENU_ID = C.MENU_ID
	                    WHERE A.MENU_ID = @MENU_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(id, "MENU_ID", SqlDbType.Int));
            return ExcuteToDataSet(qry, paramArray).Tables[0];
        }

        public int InsertMenu(int up_menu_id, int page_id, string menu_tree, string menu_name_ko,string menu_name_en,string menu_name_jp,string menu_name_ch,int sort_order,string use_yn, int emp_ref_id)
        {
            string qry = @"INSERT INTO COM_MENU_INFO VALUES(@UP_MENU_ID
                           ,@PAGE_ID
                           ,@MENU_TREE
                           ,@MENU_NAME_KO
                           ,@MENU_NAME_JP
                           ,@MENU_NAME_EN
                           ,@MENU_NAME_CH
                           ,@SORT_ORDER
                           ,@USE_YN
                           ,GETDATE()
                           ,@CREATE_ID
                           ,NULL
                           ,NULL)";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(up_menu_id, "UP_MENU_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(page_id, "PAGE_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(menu_tree, "MENU_TREE", SqlDbType.VarChar));
            paramArray.Add(new Parameter(menu_name_ko, "MENU_NAME_KO", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(menu_name_jp, "MENU_NAME_JP", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(menu_name_en, "MENU_NAME_EN", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(menu_name_ch, "MENU_NAME_CH", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(sort_order, "SORT_ORDER", SqlDbType.Int));
            paramArray.Add(new Parameter(use_yn, "USE_YN", SqlDbType.VarChar));
            paramArray.Add(new Parameter(emp_ref_id, "CREATE_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }

        public int UpdateMenu(int menu_id, int up_menu_id, int page_id, string menu_tree, string menu_name_ko, string menu_name_en, string menu_name_jp, string menu_name_ch, int sort_order, string use_yn, int emp_ref_id)
        {
            string qry = @"UPDATE COM_MENU_INFO SET
	                            PAGE_ID = @PAGE_ID, UP_MENU_ID = @UP_MENU_ID,
	                            MENU_NAME_KO = @MENU_NAME_KO, MENU_NAME_EN = @MENU_NAME_EN, MENU_NAME_JP = @MENU_NAME_JP, MENU_NAME_CH = @MENU_NAME_CH, 
	                            MENU_TREE = @MENU_TREE, SORT_ORDER = @SORT_ORDER, USE_YN = @USE_YN,
	                            UPDATE_DATE = GETDATE(), UPDATE_ID = @UPDATE_ID
                            WHERE 	MENU_ID = @MENU_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(menu_id, "MENU_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(up_menu_id, "UP_MENU_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(page_id, "PAGE_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(menu_tree, "MENU_TREE", SqlDbType.VarChar));
            paramArray.Add(new Parameter(menu_name_ko, "MENU_NAME_KO", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(menu_name_jp, "MENU_NAME_JP", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(menu_name_en, "MENU_NAME_EN", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(menu_name_ch, "MENU_NAME_CH", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(sort_order, "SORT_ORDER", SqlDbType.Int));
            paramArray.Add(new Parameter(use_yn, "USE_YN", SqlDbType.VarChar));
            paramArray.Add(new Parameter(emp_ref_id, "UPDATE_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);

        }

        public int DeleteMenu(int menu_id)
        {
            string qry = @"DELETE FROM COM_MENU_INFO WHERE 	MENU_ID = @MENU_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(menu_id, "MENU_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }

        public DataTable GetRoleInfoList()
        {
            string qry = "SELECT * FROM COM_ROLE_INFO";
            return ExcuteToDataSet(qry, null).Tables[0];
        }

        public object InsertRoleInfo(string role_ko_name, string role_jp_name, string role_en_name, string role_ch_name,
            string role_select, string role_delete, string role_update, string role_insert, int emp_ref_id)
        {
            string qry = @"INSERT INTO COM_ROLE_INFO
                           (ROLE_KO_NAME
                           ,ROLE_JP_NAME
                           ,ROLE_EN_NAME
                           ,ROLE_CH_NAME
                           ,ROLE_SELECT
                           ,ROLE_DELETE
                           ,ROLE_UPDATE
                           ,ROLE_INSERT
                           ,CREATE_DATE
                           ,CREATE_ID)
                     VALUES
                           (@ROLE_KO_NAME
                           ,@ROLE_JP_NAME
                           ,@ROLE_EN_NAME
                           ,@ROLE_CH_NAME
                           ,@ROLE_SELECT
                           ,@ROLE_DELETE
                           ,@ROLE_UPDATE
                           ,@ROLE_INSERT
                           ,@CREATE_DATE
                           ,@CREATE_ID) SELECT @@IDENTITY";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(role_ko_name, "ROLE_KO_NAME", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(role_jp_name, "ROLE_JP_NAME", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(role_en_name, "ROLE_EN_NAME", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(role_ch_name, "ROLE_CH_NAME", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(role_select, "ROLE_SELECT", SqlDbType.VarChar));
            paramArray.Add(new Parameter(role_delete, "ROLE_DELETE", SqlDbType.VarChar));
            paramArray.Add(new Parameter(role_update, "ROLE_UPDATE", SqlDbType.VarChar));
            paramArray.Add(new Parameter(role_insert, "ROLE_INSERT", SqlDbType.VarChar));
            paramArray.Add(new Parameter(DateTime.Now, "CREATE_DATE", SqlDbType.DateTime));
            paramArray.Add(new Parameter(emp_ref_id, "CREATE_ID", SqlDbType.Int));
            return ExcuteScalar(qry, paramArray);
        }

        public int InsertRoleMenu(int role_id, int menu_id, int emp_ref_id)
        {
            string qry = "INSERT INTO COM_ROLE_MENU(ROLE_ID, MENU_ID, CREATE_DATE, CREATE_ID) VALUES(@ROLE_ID, @MENU_ID, @CREATE_DATE, @CREATE_ID)";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(role_id, "ROLE_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(menu_id, "MENU_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(DateTime.Now, "CREATE_DATE", SqlDbType.DateTime));
            paramArray.Add(new Parameter(emp_ref_id, "CREATE_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }

        public int UpdateRoleInfo(int role_id, string role_ko_name, string role_jp_name, string role_en_name, string role_ch_name,
            string role_select, string role_delete, string role_update, string role_insert, int emp_ref_id)
        {
            string qry = @"UPDATE  COM_ROLE_INFO SET
                            ROLE_KO_NAME = @ROLE_KO_NAME
                           ,ROLE_JP_NAME = @ROLE_JP_NAME
                           ,ROLE_EN_NAME= @ROLE_EN_NAME
                           ,ROLE_CH_NAME= @ROLE_CH_NAME
                           ,ROLE_SELECT= @ROLE_SELECT
                           ,ROLE_DELETE= @ROLE_DELETE
                           ,ROLE_UPDATE= @ROLE_UPDATE
                           ,ROLE_INSERT= @ROLE_INSERT
                           ,UPDATE_DATE= @UPDATE_DATE
                           ,UPDATE_ID = @UPDATE_ID
                     WHERE ROLE_ID = @ROLE_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(role_ko_name, "ROLE_KO_NAME", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(role_jp_name, "ROLE_JP_NAME", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(role_en_name, "ROLE_EN_NAME", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(role_ch_name, "ROLE_CH_NAME", SqlDbType.NVarChar));
            paramArray.Add(new Parameter(role_select, "ROLE_SELECT", SqlDbType.VarChar));
            paramArray.Add(new Parameter(role_delete, "ROLE_DELETE", SqlDbType.VarChar));
            paramArray.Add(new Parameter(role_update, "ROLE_UPDATE", SqlDbType.VarChar));
            paramArray.Add(new Parameter(role_insert, "ROLE_INSERT", SqlDbType.VarChar));
            paramArray.Add(new Parameter(DateTime.Now, "UPDATE_DATE", SqlDbType.DateTime));
            paramArray.Add(new Parameter(emp_ref_id, "UPDATE_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(role_id, "ROLE_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }

        public int DeleteRoleMenu(int role_id, int menu_id)
        {
            string qry = "DELETE FROM COM_ROLE_MENU WHERE ROLE_ID = @ROLE_ID AND MENU_ID = @MENU_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(role_id, "ROLE_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(menu_id, "MENU_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }

        public int DeleteRoleInfo(int role_id)
        {
            string qry = "DELETE FROM COM_ROLE_INFO WHERE ROLE_ID = @ROLE_ID DELETE FROM COM_ROLE_MENU WHERE ROLE_ID = @ROLE_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(role_id, "ROLE_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }

        public DataTable GetRoleToid(int role_id)
        {
            string qry = "SELECT * FROM COM_ROLE_INFO WHERE ROLE_ID = @ROLE_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(role_id, "ROLE_ID", SqlDbType.Int));
            return ExcuteToDataSet(qry, paramArray).Tables[0];
        }
        public DataTable GetRoleMenu(int role_id)
        {
            string qry = "SELECT MENU_ID FROM COM_ROLE_MENU WHERE ROLE_ID = @ROLE_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(role_id, "ROLE_ID", SqlDbType.Int));
            return ExcuteToDataSet(qry, paramArray).Tables[0];
        }

        public object CheckCountRoleUser(int role_id)
        {
            string qry = "SELECT COUNT(*) FROM COM_ROLE_USER WHERE ROLE_ID = @ROLE_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(role_id, "ROLE_ID", SqlDbType.Int));
            return ExcuteScalar(qry, paramArray);
        }

        public DataTable GetRoleUserList()
        {
            string qry = @"SELECT 
	                            C.EMP_NAME, C.EMP_REF_ID, D.DEPT_REF_ID, D.DEPT_NAME
	                            , ISNULL(B.ROLE_ID,0) AS ROLE_ID, A.ROLE_KO_NAME, A.ROLE_EN_NAME, A.ROLE_JP_NAME, A.ROLE_CH_NAME 
                            FROM COM_EMP_INFO C
	                            INNER JOIN COM_DEPT_INFO D ON  C.DEPT_REF_ID = D.DEPT_REF_ID
	                            LEFT JOIN COM_ROLE_USER B ON B.EMP_REF_ID = C.EMP_REF_ID
	                            LEFT JOIN COM_ROLE_INFO A ON A.ROLE_ID = B.ROLE_ID ORDER BY C.EMP_NAME";
            return ExcuteToDataSet(qry, null).Tables[0];
        }

        public int DeleteRoleUser(int emp_ref_id, int role_id)
        {
            string qry = "DELETE FROM COM_ROLE_USER WHERE ROLE_ID = @ROLE_ID AND EMP_REF_ID= @EMP_REF_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(role_id, "ROLE_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(emp_ref_id, "EMP_REF_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }
        public int DeleteRoleUser(int emp_ref_id)
        {
            string qry = "DELETE FROM COM_ROLE_USER WHERE  EMP_REF_ID= @EMP_REF_ID";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(emp_ref_id, "EMP_REF_ID", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }

        public int InsertRoleUser(int emp_ref_id, int role_id, int inUser)
        {
            string qry = "INSERT INTO COM_ROLE_USER VALUES(@ROLE_ID, @EMP_REF_ID, GETDATE(), @INUSER, NULL, NULL)";
            List<Parameter> paramArray = new List<Parameter>();
            paramArray.Add(new Parameter(role_id, "ROLE_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(emp_ref_id, "EMP_REF_ID", SqlDbType.Int));
            paramArray.Add(new Parameter(emp_ref_id, "INUSER", SqlDbType.Int));
            return ExcuteNonQuery(qry, paramArray);
        }
    }
}
