using CoreBusiness.Common;
using CoreBusiness.Contracts;
using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBusiness.Data
{
    [Serializable]
    [PrimaryKey("ID", AutoIncrement =true)]
    public class User : BusinessObject
    {
        public virtual string Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string Pin { get; set; }
        public virtual long MerchantID { get; set; }
        public virtual string MerchantName { get; set; }
        public virtual DateTime LastAuthenticated { get; set; }
        public virtual bool IsLockedOut { get; set; }
        public virtual long MasterID { get; set; }
        public virtual string Password { get; set; }
        public virtual string PassPhrase { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Address { get; set; }
        public virtual string Avatar { get; set; }
        public virtual DateTime? LastLoginDate { get; set; }
        public virtual int NumberOfFailedAttempts { get; set; }
        public virtual string EncryptionKey { get; set; }
        public virtual string DecryptionKey { get; set; }
        public virtual DateTime? DOB { get; set; }
        public virtual string[,] SecurityGrid { get; set; }
    }
    [Serializable]
    public class UserMap : BusinessObjectMap<User>
    {
    }
}
