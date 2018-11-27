using CoreBusiness.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusiness.Data
{
    [Serializable]
    public class Package : BusinessObject
    {
        public decimal Cost { get; set; }
        public long Duration { get; set; }
        public string Description { get; set; }
        public bool IsPremium { get; set; }
        public PackageType Type { get; set; }
        public long DataLimit { get; set; }
    }

    [Serializable]
    public enum PackageType
    {
    }
}
