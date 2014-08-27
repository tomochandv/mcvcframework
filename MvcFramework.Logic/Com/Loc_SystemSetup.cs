using MvcFramework.Dac;
using MvcFramework.Model;
using System.Collections.Generic;
using System.Web;

namespace MvcFramework.Logic
{
    public class Loc_SystemSetup
    {
        public  List<Model_System_Setup> GetSystemSetup()
        {
            return new Utill().ConvertToList<Model_System_Setup>(new Dac_Com_System_Setup().GetSystemSetup());
        }
        public string GetSetupValue(string key)
        {
            List<Model_System_Setup> list = (List<Model_System_Setup>)HttpContext.Current.Application["System"];
            Model_System_Setup info = list.Find(item => item.SYS_KEY == key);
            if(info != null)
            {
                return info.SYS_VALUE;
            }
            else
            {
                return "";
            }
        }
    }
}
