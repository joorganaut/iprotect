﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AXAMansard.Framework.AuditTrail.Exceptions
{
    public class AuditTrailException : Exception
    {
        public override string Message
        {
            get
            {
                return base.Message;
            }
        }
    }
}
