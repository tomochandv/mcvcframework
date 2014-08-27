using MvcFramework.Dac;
using MvcFramework.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace MvcFramework.Logic
{
    public class Loc_LanguageInfo
    {
        public List<Model_Language_Info> GetLanguage()
        {
            DataTable dt = new Dac_Com_Language_Info().GetMultiLanguage();
            List<Model_Language_Info> info = new Utill().ConvertToList<Model_Language_Info>(dt);
            return info;
        }

        public List<Model_Language_Info> GetGlobalMultiLanguageAll()
        {
            List<Model_Language_Info> list = (List<Model_Language_Info>)HttpContext.Current.Application["MultiLan"];
            return list;
        }

        public Model_Language_Info GetLanguageModel(int id)
        {
            return GetGlobalMultiLanguageAll().Find(item => item.LANG_ID == id);
        }

        public string GetLanguageType()
        {
            string langType = new Loc_SystemSetup().GetSetupValue("LANGUAGE_TYPE");
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsIdentity fid = (FormsIdentity)HttpContext.Current.User.Identity;
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Model_User_Info user = serializer.Deserialize<Model_User_Info>(fid.Ticket.UserData);
                langType = user.EMP_LAN_TYPE;
            }
            return langType;
        }

        public string GetLanguage(int id)
        {
            string returnTxt = string.Empty;
            string langType = new Loc_SystemSetup().GetSetupValue("LANGUAGE_TYPE");
            Model_Language_Info info = GetGlobalMultiLanguageAll().Find(item => item.LANG_ID == id);
            if(HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsIdentity fid = (FormsIdentity)HttpContext.Current.User.Identity;
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Model_User_Info user = serializer.Deserialize<Model_User_Info>(fid.Ticket.UserData);
                langType = user.EMP_LAN_TYPE;
            }

            if (info != null)
            {
                if(langType == "KO")
                {
                    returnTxt = info.LANG_KO;
                }
                else if(langType == "JP")
                {
                    returnTxt = info.LANG_JP;
                }
                else if (langType == "EN")
                {
                    returnTxt = info.LANG_EN;
                }
                else if (langType == "CH")
                {
                    returnTxt = info.LANG_CH;
                }
                else
                {
                    returnTxt = info.LANG_KO;
                }
            }
            return returnTxt;
        }

        public string GetLanguage(int id, string ko, string jp, string en, string ch)
        {
            string returnTxt = string.Empty;
            string langType = new Loc_SystemSetup().GetSetupValue("LANGUAGE_TYPE");
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsIdentity fid = (FormsIdentity)HttpContext.Current.User.Identity;
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Model_User_Info user = serializer.Deserialize<Model_User_Info>(fid.Ticket.UserData);
                langType = user.EMP_LAN_TYPE;
            }
            if (langType == "KO")
            {
                returnTxt = ko;
            }
            else if (langType == "JP")
            {
                returnTxt = jp;
            }
            else if (langType == "EN")
            {
                returnTxt = en;
            }
            else if (langType == "CH")
            {
                returnTxt = ch;
            }
            else
            {
                returnTxt = ko;
            }
            return returnTxt;
        }
    }
}
