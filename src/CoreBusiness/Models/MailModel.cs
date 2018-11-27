using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusiness.Models
{
    [Serializable]
    public class MailModel : IExternalObjectModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string error { get; set; }
        public virtual List<MailAttachment> Attachments { get; set; }
        public MailModel()
        {
            Attachments = new List<MailAttachment>();
        }
    }
    [Serializable]
    public class MailAttachment
    {
        public virtual byte[] File { get; set; }
        public virtual string FileType { get; set; }
    }
}
