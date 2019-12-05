using Culture.Contracts.IServices;
using Culture.Models;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Culture.Services.Services
{
    public class EmailService : IEmailService
    {
        public IConfiguration Configuration { get; }

        public EmailService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task SendEmail(string content, IEnumerable<AppUser> appUsers)
        {
            var x = Configuration["Values:EmailLogin"];
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Configuration["Values:EmailLogin"], Configuration["Values:EmailPass"]),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress($"{Configuration["Values:EmailLogin"]}@gmail.com"),
                Subject = "Notyfikacja generowana z systemu MyCulture",
                IsBodyHtml = true
            };

            foreach (var user in appUsers)
            {

                if (isValidEmail(user.Email))
                {
                    mailMessage.To.Add(user.Email);
                    mailMessage.Body =
                        $"Użytkowniku, {user.UserName} !" + "<br/> Otrzymałeś nowe powiadomienie! <br/>"
                        + content
                        + " <br/> Jeżeli nie chcesz już dostawać notyfikacji, skonfiguruj odpowiednio konto w ustawieniach. <br/>" +
                        " Pozdrawiamy administracja MyCulture";

                    await client.SendMailAsync(mailMessage);
                }
            }
        }
        private bool isValidEmail(string email)
        {
            try
            {
                var validEmail = new MailAddress(email);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
