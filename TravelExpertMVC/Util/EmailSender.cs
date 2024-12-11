using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace TravelExpertMVC.Util
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                //UseDefaultCredentials = false,
                Credentials = new NetworkCredential("travelexpert.group3@gmail.com", "")
            };

            return client.SendMailAsync(
                new MailMessage(from: "travelexpert.group3@gmail.com",
                                to: email,
                                subject,
                                message
                                )
                {
                    IsBodyHtml = true
                }
                );

        }

    }

}


