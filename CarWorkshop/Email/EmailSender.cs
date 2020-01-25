using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWorkshop.Email
{
    public class EmailSender : IEmailSender
    {
        public EmailOptions options { get; set; }
        public EmailSender(IOptions<EmailOptions> emailoptions)
        {
            options = emailoptions.Value;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(options.SendGridKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("Admin@Carworkshop.com", "CarWorkshop"),
                Subject = subject,
                PlainTextContent = htmlMessage,
                HtmlContent = htmlMessage
            };
            msg.AddTo(new EmailAddress(email));
            try
            {
                return client.SendEmailAsync(msg);
            }
            catch(Exception ex)
            {

            }
            return null;
        }
    }
}
