using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AXAMansard.Framework.Functions.DAO;
using AXAMansard.Framework.Functions.DTO;

namespace AXAMansard.Framework.Functions
{
    public class FunctionsEngine
    {
        public static List<UserRole> GetRoles(string mfbCode)
        {
            return UserRoleDAO.RetrieveAll(mfbCode);
        }
    }
}
