using Email.Core.Interfaces;
using Email.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnlineShop.Modules.Email.Infrastructure.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<bool> SendEmail(EmailModel email)
        {
            return await _emailService.SendEmail(email);
        }
    }
}
