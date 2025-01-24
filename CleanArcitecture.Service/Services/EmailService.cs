using CleanArcitecture.Domain.Helpers;
using CleanArcitecture.Service.Abstracts;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CleanArcitecture.Service.Services
{
    public class EmailService(IOptions<EmailSettings> emailSetting) : IEmailService
    {
        private readonly IOptions<EmailSettings> _emailSetting = emailSetting;

        public async Task<string> SendEmail(string email, string message, string subject)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSetting.Value.Host, _emailSetting.Value.Port);
                    await client.AuthenticateAsync(_emailSetting.Value.FromEmail, _emailSetting.Value.Password);
                    var bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = message,
                        TextBody = "Wellcome"
                    };
                    var MSG = new MimeMessage
                    {
                        Body = bodyBuilder.ToMessageBody()
                    };
                    MSG.From.Add(new MailboxAddress("Alaa", _emailSetting.Value.FromEmail));
                    MSG.To.Add(new MailboxAddress("Test", email));
                    MSG.Subject = subject;
                    await client.SendAsync(MSG);
                    await client.DisconnectAsync(true);
                    return "Success";
                }
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
    }
}
