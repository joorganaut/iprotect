using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AXAMansard.Framework.DTO;

namespace AXAMansard.Framework.Functions.DTO
{
    public interface IBranch : IDataObject
    {
        System.String Name { get; set; }

        int Code { get; set; }

        string Address { get; set; }

        Status Status { get; set; }

        
        long RegionID { get; set; }

       

    }
}
