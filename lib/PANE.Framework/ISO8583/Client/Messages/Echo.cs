using System;
using System.Collections.Generic;
using System.Text;
using AXAMansard.Framework.ISO8583.DTO;
using AXAMansard.Framework.ISO8583.Utility;
using AXAMansard.Framework.Utility;

namespace AXAMansard.Framework.ISO8583.Client.Messages
{
    public class Echo : Message
    {
        public Echo(CardAcceptor terminal, string transactionID, bool isRepeat)
            : base(800, transactionID, isRepeat)
        {
            this.Fields.Add(FieldNos.F70_NetworkMgtInfoCode, "301");
        }
    }
}
