using MvcFramework.Dac;
using MvcFramework.Logic.Base;
using MvcFramework.Model;
using System.Collections.Generic;
using System.Data;


namespace MvcFramework.Logic
{
    public class Loc_UserInfo
    {
        public Model_User_Info GetUserInfo(int emp_ref_id)
        {
            DataTable dt = new Dac_Com_User_Info().GetUserInfo(emp_ref_id);
            Model_User_Info info = new Utill().ConvertToRow<Model_User_Info>(dt.Rows[0]);
            return info;
        }

        public Model_User_Info GetUserInfo(string userId, string pwd)
        {
            DataTable dt = new Dac_Com_User_Info().GetUserInfo(userId);
            Model_User_Info info = new Model_User_Info();
            List<Model_User_Info> list = new Utill().ConvertToList<Model_User_Info>(dt);
            foreach(var data in list)
            {
                if(pwd == new Security().Description(data.LOGINPWD))
                {
                    info = data;
                }
            }
            return info;
        }
    }
}
