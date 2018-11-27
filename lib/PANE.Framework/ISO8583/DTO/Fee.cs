using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AXAMansard.Framework.DTO;

namespace AXAMansard.Framework.ISO8583.DTO
{
    public class Fee : TransAmount
    {
        public string FeeDescription { get; set; }
    }
}
