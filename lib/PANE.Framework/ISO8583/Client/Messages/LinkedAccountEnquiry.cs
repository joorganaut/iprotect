using System;
using System.Collections.Generic;
using System.Text;
using AXAMansard.Framework.ISO8583.DTO;
using AXAMansard.Framework.ISO8583.Utility;
using AXAMansard.Framework.ISO8583.Client.Configuration;
using AXAMansard.Framework.Utility;

namespace AXAMansard.Framework.ISO8583.Client.Messages
{
    public class LinkedAccountEnquiry : AuthorizationMessage
    {
        public LinkedAccountEnquiry(CardAcceptor cardAcceptor, CardDetails theCard, string transactionID, bool isRepeat)
            : base(cardAcceptor, transactionID, TransactionType.LinkedAccountInquiry, theCard, AccountType.Default , "00", new Amount(0, "566", AmountType.AvailableBalance), isRepeat)
        {
            this.Fields.Remove(FieldNos.F52_PinData);
        }
    }
}
