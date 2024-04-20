using System.Net.Mail;
using System.Net;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
namespace A2_SinhToDau_QuanLyBenhVien
{
    public class test
    {


        static async Task Main()
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("nhocnhat0203@gmail.com", "nhocvip02"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("nhocnhat0203@gmail.com"),
                Subject = "subject",
                Body = "<p>This is a test email</p>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add("nhocnhat0200@gmail.com");

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email. Exception: " + ex.Message);
            }
        }
}
}
