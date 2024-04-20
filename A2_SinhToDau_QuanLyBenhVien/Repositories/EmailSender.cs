using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace A2_SinhToDau_QuanLyBenhVien.Repositories
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (var client = new SmtpClient(_configuration["EmailSettings:MailServer"],
                int.Parse(_configuration["EmailSettings:MailPort"])))
            {
                client.Credentials = new NetworkCredential(_configuration["EmailSettings:Sender"], _configuration["EmailSettings:Password"]);
                client.EnableSsl = true;

                var mailMessage = new MailMessage(_configuration["EmailSettings:Sender"], email, subject, htmlMessage)
                {
                    IsBodyHtml = true
                };

                try
                {
                    await client.SendMailAsync(mailMessage);
                }
                catch (SmtpException ex)
                {
                    // Xử lý ngoại lệ tại đây (ví dụ: ghi log)
                    throw; // Rethrow exception or handle it accordingly
                }
            }
        }
    }
}
