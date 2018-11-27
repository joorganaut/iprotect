using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AXAMansard.Framework.Utility;
using AXAMansard.Framework.Functions.DTO;

namespace AXAMansard.Framework.DTO
{
    public interface IInstitution: IDataObject
    {
        long ID { get; set; }
        int InstitutionCode { get; set; }
        string Name { get; set; }
        //Status Status { get; set; }
        //IUser TheUser { get; set; }

        string Code { get; set; }

        string LocalConnectionString { get; set; }
        
        string RemoteConnectionString { get; set; }
    }
}
