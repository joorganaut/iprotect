using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AXAMansard.Framework.DTO;

namespace AXAMansard.Framework.ISO8583.DTO
{
    public class TransAmount : DataObject
    {
        public string Amount { get; set; }
        public string Sign { get; set; }
        public string CurrencyCode { get; set; }
    }
}
