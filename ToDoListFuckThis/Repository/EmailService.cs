using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using ToDoListFuckThis.Config;
using ToDoListFuckThis.Repository.IRepository;

namespace ToDoListFuckThis.Repository
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendMailAsync(string to, string subject, string text, string html = null)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_emailSettings.User));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            if (!string.IsNullOrEmpty(html))
            {
                message.Body = new BodyBuilder { TextBody = text, HtmlBody = html }.ToMessageBody();
            }
            else
            {
                message.Body = new TextPart("plain") { Text = text };
            }

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.User, _emailSettings.Pass);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }
}
