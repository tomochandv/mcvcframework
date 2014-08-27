using MvcFramework.Dac;
using MvcFramework.Model;

namespace MvcFramework.Logic
{
    public class Loc_ButtonRole
    {
        public Model_Role_Info AuthRole(int emp_ref_id)
        {
            return new Utill().ConvertToRow<Model_Role_Info>(new Dac_RoleMenu().GetRoleInfo(emp_ref_id));
        }
    }
}
