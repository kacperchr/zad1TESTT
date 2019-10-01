using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Services.EmailService;

namespace MailServices
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;
        private MessageBody _messageBody;
        //private MessageSend _messageSend;

        public void MessageBodyValue(User user)
        {
            
        }

        public EmailService(IOptions<EmailConfiguration> emailConfiguration)
        {
            _emailConfiguration = emailConfiguration.Value;
        }
        public async Task Send(EmailMessage emailMessage, SmtpClient smtpClient)
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(emailMessage.To));
            message.From.Add(new MailboxAddress(_emailConfiguration.SmtpUsername));
            message.Subject = emailMessage.Subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = await emailMessage.Content
            };
            await smtpClient.SendAsync(message);
        }
        private SmtpClient GetSmtpClient()
        {
            var smptClient = new SmtpClient();
            smptClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);
            smptClient.AuthenticationMechanisms.Remove("XOAUTH2");
            smptClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
            return smptClient;
        }
        
        private async Task SendEmailToUser(User user, SmtpClient smtpClient)
        {
            var message = _messageBody.GetEmailMessage(user);
            Send(message, smtpClient);
        }

        public async Task SendEmailToUser(ICollection<User> users)
        {
            using (var emailClient = GetSmtpClient())
            {
                foreach (var user in users)
                {
                    SendEmailToUser(user, emailClient);
                }

                emailClient.Disconnect(true);
            }
        }
    }
}