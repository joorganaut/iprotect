using System;
using System.Collections.Generic;
using System.Text;
using Trx.Messaging.Iso8583;
using Trx.Messaging;
using AXAMansard.Framework.ISO8583.DTO;
using AXAMansard.Framework.ISO8583.Utility;
using AXAMansard.Framework.ISO8583.Client.Configuration;
using AXAMansard.Framework.Utility;

namespace AXAMansard.Framework.ISO8583.Client.Messages
{
    public abstract class CardBasedMessage : Message
    {

        public CardBasedMessage(int messageTypeIdentifier, CardAcceptor cardAcceptor, CardDetails theCard, string transactionID, bool isRepeat)
            : base(messageTypeIdentifier, transactionID, isRepeat)
        {
            this.Fields.Add(FieldNos.F2_PAN, theCard.PAN);
            
            this.Fields.Add(FieldNos.F14_CardExpiryDate, string.Format("{0:yyMM}", theCard.ExpiryDate));

            this.Fields.Add(FieldNos.F22_PosEntryMode, "011");
            this.Fields.Add(FieldNos.F25_PosConditionCode, "00");
            this.Fields.Add(FieldNos.F26_PinCaptureCode, "12");

            //this.Fields.Add(123, "100040165110119");
            this.Fields.Add(123, "000000000000000");


            if (theCard.UsePIN)
            {
                this.Fields.Add(FieldNos.F52_PinData, theCard.PIN);
            }
            
            this.Fields.Add(FieldNos.F41_CardAcceptorTerminalCode, cardAcceptor.TerminalID);
            this.Fields.Add(FieldNos.F42_CardAcceptorIDCode, cardAcceptor.ID);
            this.Fields.Add(FieldNos.F43_CardAcceptorNameLocation, string.Format("{0}{1}{2}{3}", cardAcceptor.Location, cardAcceptor.City, cardAcceptor.State, cardAcceptor.Country));

        }


    }
}
