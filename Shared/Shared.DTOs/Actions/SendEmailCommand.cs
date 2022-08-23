using MediatR;

namespace OnlineShop.DTOs.Actions
{
    public class SendEmailCommand : INotification
    {
        public string Text { get; set; }

        public SendEmailCommand(string text)
        {
            Text = text;
        }
    }
}
