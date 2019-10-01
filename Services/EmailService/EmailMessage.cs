using System.Threading.Tasks;

namespace Services.EmailService
{
    public class EmailMessage
    {        
        public string To { get; set; }
        public string Subject { get; set; }
        public Task<string> Content { get; set; }
        
    }
}