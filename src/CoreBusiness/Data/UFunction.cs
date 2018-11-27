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
    public class UFunction : BusinessObject
    {
        public virtual string Url { get; set; }
        public virtual string Group { get; set; }
    }
    [Serializable]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class RFunction : BusinessObject
    {
        public virtual long RoleID { get; set; }
        public virtual long FunctionID { get; set; }
    }

    [Serializable]
    public class UFunctionMap : BusinessObjectMap<UFunction>
    {
    }
    [Serializable]
    public class RFunctionMap : BusinessObjectMap<RFunction>
    {
    }
}
