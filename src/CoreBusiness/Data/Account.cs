using CoreBusiness.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusiness.Data
{
    [Serializable]
    public class Account : BusinessObject
    {
        public string AccountNumber { get; set; }
        public long UserID { get; set; }
    }
}
