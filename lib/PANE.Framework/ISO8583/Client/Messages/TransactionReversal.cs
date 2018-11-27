using System;
using System.Collections.Generic;
using System.Text;
using AXAMansard.Framework.ISO8583.DTO;
using AXAMansard.Framework.ISO8583.Utility;
using AXAMansard.Framework.ISO8583.Client.Configuration;
using Trx.Messaging.Iso8583;
using Trx.Messaging;
using AXAMansard.Framework.Utility;

namespace AXAMansard.Framework.ISO8583.Client.Messages
{
    internal class TransactionReversal : Message
    {
        public TransactionReversal(Iso8583Message msg, string transactionID, bool isRepeat)
            : base(420, transactionID, isRepeat)
        {
           
            //msg.Fields.Remove(new int[] {FieldNos.F7_TransDateTime, FieldNos.F12_TransLocalTime, FieldNos.F13_TransLocalDate});
            foreach (Field fld in msg.Fields)
            {
                if (this.Fields.Contains(fld.FieldNumber))
                {
                    this.Fields.Remove(fld.FieldNumber);
                }
                this.Fields.Add(fld);
            }
            this.Fields.Add(FieldNos.F90_OriginalDataElements, transactionID);
        }
    }
}
