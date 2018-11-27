using CoreBusiness.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusinessLogic
{
    public class MailSystem
    {
        private string Username { get; set; }
        private string Password { get; set; }
        private string Host { get; set; } //"smtp-mail.outlook.com"
        private string SendingName = System.Configuration.ConfigurationManager.AppSettings["SendingName"];
        private string LogoFile = System.Configuration.ConfigurationManager.AppSettings["Logo"];
        public MailSystem()
        {
        }
        public MailSystem(string username, string password, string host)
        {
            Username = username;
            Password = password;
            Host = host;
        }

        public async Task SendGridAsync(MailModel model)
        {
            SmtpClient client = new SmtpClient(Host);
            Stream stream = null;
            MemoryStream ms = null;
            client.Port = 587;
            client.ServicePoint.MaxIdleTime = 1;
            client.ServicePoint.ConnectionLimit = 1;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.ServicePoint.UseNagleAlgorithm = true;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(Username, Password);
            client.EnableSsl = true;
            client.Credentials = credentials;

            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(model.From, SendingName);
                mailMessage.To.Add(new MailAddress(model.To));
                mailMessage.Bcc.Add(new MailAddress(model.From));
                mailMessage.Subject = model.Subject;
                mailMessage.IsBodyHtml = true;
                //mailMessage.Body = model.Body;
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(model.Body, null, MediaTypeNames.Text.Html);
                LinkedResource logo = new LinkedResource(new MemoryStream(FileLoader.GetImageBytes(LogoFile)), new ContentType($"image/jpeg"));
                logo.ContentId = "logo";// Guid.NewGuid().ToString();
                avHtml.LinkedResources.Add(logo);
                mailMessage.AlternateViews.Add(avHtml);

                //Attachment att = new Attachment(ms, new ContentType($"image/{img.Extension.ToString()}"));
                //att.ContentDisposition.Inline = true;
                //mailMessage.Body = mailMessage.Body.Replace("{qrcode}", $"cid:\"{inline.ContentId}@\"");
                //mailMessage.Attachments.Add(att);

                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                if (ms != null) ms.Dispose();
                if (stream != null) stream.Dispose();
            }
        }

        public void SendMail(MailModel model, out string errMsg, ImageModel img = null)
        {
            SmtpClient client = new SmtpClient(Host);
            Stream stream = null;
            MemoryStream ms = null;
            errMsg = string.Empty;
            client.Port = 587;
            client.ServicePoint.MaxIdleTime = 1;
            client.ServicePoint.ConnectionLimit = 1;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.ServicePoint.UseNagleAlgorithm = true;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(Username, Password);
            client.EnableSsl = true;
            client.Credentials = credentials;

            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(model.From, SendingName);
                mailMessage.To.Add(new MailAddress(model.To));
                mailMessage.Bcc.Add(new MailAddress(model.From));
                mailMessage.Subject = model.Subject;
                mailMessage.IsBodyHtml = true;
                //mailMessage.Body = model.Body;
                if (img != null)
                {
                    ms = new MemoryStream();
                    img.Image.Save(ms, ImageFormat.Jpeg);
                    var bytearra = ms.ToArray();
                    AlternateView avHtml = AlternateView.CreateAlternateViewFromString(model.Body, null, MediaTypeNames.Text.Html);
                    LinkedResource inline = new LinkedResource(new MemoryStream(bytearra), new ContentType($"image/{img.Extension.ToString()}"));
                    inline.ContentId = "qrcode";// Guid.NewGuid().ToString();
                    //mailMessage.Body.Replace("{qrcode}", $"cid:\"{inline.ContentId}@\"");
                    avHtml.LinkedResources.Add(inline);
                    LinkedResource logo = new LinkedResource(new MemoryStream(FileLoader.GetImageBytes("Simply_Meds_logo.jpg")), new ContentType($"image/jpeg"));
                    logo.ContentId = "logo";// Guid.NewGuid().ToString();
                    avHtml.LinkedResources.Add(logo);
                    mailMessage.AlternateViews.Add(avHtml);

                    //Attachment att = new Attachment(ms, new ContentType($"image/{img.Extension.ToString()}"));
                    //att.ContentDisposition.Inline = true;
                    //mailMessage.Body = mailMessage.Body.Replace("{qrcode}", $"cid:\"{inline.ContentId}@\"");
                    //mailMessage.Attachments.Add(att);
                }
                foreach (MailAttachment attache in model.Attachments)
                {
                    stream = new MemoryStream(attache.File);
                    Attachment attachment = new Attachment(stream, $"image/{attache.FileType.Replace(".", "").Replace("jpg", "jpeg")}");
                    attachment.ContentDisposition.Inline = true;
                    mailMessage.Attachments.Add(attachment);
                }
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                if (ms != null) ms.Dispose();
                if (stream != null) stream.Dispose();
            }
        }
    }
}
