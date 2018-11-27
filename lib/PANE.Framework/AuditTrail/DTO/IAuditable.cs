using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AXAMansard.Framework.Functions.DTO;

namespace AXAMansard.Framework.AuditTrail.DTO
{
    public interface IAuditable
    {
        IUser AuditableUser { get; set; }
    }
}
