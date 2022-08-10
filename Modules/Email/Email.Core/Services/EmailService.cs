using Email.Core.Interfaces;
using Email.Core.Models;
using System.Threading.Tasks;

namespace Email.Core.Services
{
    public class EmailService : IEmailService
    {
        public async Task<string> SendEmail(EmailModel email)
        {
            return "Email sent successfully";
        }
    }
}
