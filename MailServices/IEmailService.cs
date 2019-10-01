using System.Collections.Generic;
using System.Threading.Tasks;
using Common;

namespace Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmailToUser(ICollection<User> users);
    }
}