// EmailSender.cs
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace TaskApp.PL.Services
{
    public class EmailSender : IEmailSender
    {
        // Implement the SendEmailAsync method using your preferred email sending service/library
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // Example implementation using SendGrid
            var apiKey = "YourSendGridApiKey";
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage
            {
                From = new EmailAddress("ahmedqweasz887@gmail.com", "Ahmed Khalel"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            await client.SendEmailAsync(msg);
        }
    }
}
