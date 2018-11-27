using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace AXAMansard.Framework.ISO8583.Client.DTO
{
    [DataContract]
    public class SignOffResponse : MessageResponse
    {
        public SignOffResponse(Trx.Messaging.Message responseMessage)
            : base(responseMessage)
        {
           
        }
    }
}
