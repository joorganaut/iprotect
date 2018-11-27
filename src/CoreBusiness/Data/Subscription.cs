using CoreBusiness.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusiness.Data
{
    [Serializable]
    public class Subscription : BusinessObject
    {
        public DateTime ExpiryDate { get; set; }
        public DateTime StartDate { get; set; }
        public string LoginID { get; set; }
        public string Passkey { get; set; }
        public long AccountID { get; set; }
        public SubscriptionStatus Status { get; set; }
        public long PackageID { get; set; }
    }

    [Serializable]
    public enum SubscriptionStatus
    {
        Started = 0,
        Stopped = 1,
        Paused = 2
    }
}
