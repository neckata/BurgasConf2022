using Email.Core.Models;
using System.Threading.Tasks;

namespace Email.Core.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailModel email);
    }
}
