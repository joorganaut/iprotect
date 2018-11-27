using CoreBusiness.Common;
using CoreBusiness.Contracts;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusiness.Data
{
    [Serializable]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class Role : BusinessObject
    {
        public virtual bool IsTransactable { get; set; }
        public virtual decimal TransactionAmount { get; set; }
    }
    [Serializable]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class UserRole : BusinessObject
    {
        public virtual long UserID { get; set; }
        public virtual long RoleID { get; set; }
        public virtual string Username { get; set; }
        public virtual string RoleName { get; set; }
    }

    [Serializable]
    public class UserRoleMap : BusinessObjectMap<UserRole>
    {
    }
    [Serializable]
    public class RoleMap : BusinessObjectMap<Role>
    {
    }
}
