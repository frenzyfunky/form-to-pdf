using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FormToPdf.Service
{
    public class GMailSender : IMailSender
    {
        private readonly IConfiguration _configuration;
        public GMailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Send(string body, Stream attachment, string fileName, params string[] to)
        {
            try
            {
                using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential
                    {
                        UserName = _configuration.GetSection("Email:Username").Value,
                        Password = _configuration.GetSection("Email:Password").Value
                    };
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        IsBodyHtml = true,
                        Body = body,
                        From = new MailAddress(_configuration.GetSection("Email:From").Value),
                        Subject = _configuration.GetSection("Email:Subject").Value,
                    };

                    attachment.Position = 0;

                    var attachmentObj = new Attachment(attachment, new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf));
                    attachmentObj.ContentDisposition.FileName = fileName;
                    mailMessage.Attachments.Add(attachmentObj);

                    foreach (var item in to)
                    {
                        mailMessage.To.Add(item);
                    }

                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
