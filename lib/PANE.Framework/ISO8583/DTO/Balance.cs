﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AXAMansard.Framework.ISO8583.DTO
{
    public class Balance : TransAmount
    {
        public string BalanceType { get; set; }
    }
}
