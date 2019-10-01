using System.Threading.Tasks;
using Common;
using Services.EmailService;

namespace MailServices
{
    public class MessageBody
    {
        private async Task<string> GetMessageBody(User user)
        {
            return $"<h1>Witaj {user.FirstName}</h1> Mamy promocje. <p>Dziękujemy</p>";
        }
        
        public EmailMessage GetEmailMessage(User user)
        {
            return new EmailMessage
            {
                Subject = "ZTP - Zad1",
                To = user.Email,
                Content = GetMessageBody(user)
            };
        }
    }
}