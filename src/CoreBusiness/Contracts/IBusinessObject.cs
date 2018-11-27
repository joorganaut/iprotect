using AXAMansard.Framework.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBusiness
{
    public interface IBusinessObject : IDataObject
    {
        DateTime? DateCreated { get; set; }
        bool IsEnabled { get; set; }
        DateTime? DateLastModified { get; set; }
        string Name { get; set; }
        string CreatedBy { get; set; }
        string LastModifiedBy { get; set; }
    }

}
