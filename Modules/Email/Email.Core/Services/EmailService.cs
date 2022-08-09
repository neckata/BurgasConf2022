using Email.Core.Interfaces;
using Email.Core.Models;
using System.Threading.Tasks;

namespace Email.Core.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmail(EmailModel email)
        {
            return true;
        }
    }
}
