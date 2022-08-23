using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.DTOs.Actions;

namespace Email.Core.Commands
{
    public class EmailHandler : INotificationHandler<SendEmailCommand>
    {
        public async Task Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("***************************");
            Console.WriteLine($"Sending Email with body {request.Text}");
            Console.WriteLine("***************************");
        }
    }
}
