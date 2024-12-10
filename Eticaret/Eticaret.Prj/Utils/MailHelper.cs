using Eticaret.Prj.Entities;
using System.Net;
using System.Net.Mail;

namespace Eticaret.Prj.Utils
{
    public class MailHelper
    {
        public static async Task SendMailAsync(Contact contact)
        {
            SmtpClient smtpClient = new SmtpClient("mail.gnietz.com", 587);
            smtpClient.Credentials = new NetworkCredential("gurkan@gnietz.com", "mailşifre");
            smtpClient.EnableSsl = false;
            MailMessage message = new MailMessage();
            message.From = new MailAddress("gurkan@gnietz.com");
            message.To.Add("gurkan@gnietz.com");
            message.Subject = "Siteden Mesaj Geldi";
            message.Body = $"Ad Soyad: {contact.Name} {contact.Surname} <br/> E-Posta: {contact.Email} <br/> Mesaj: {contact.Message}";
            message.IsBodyHtml = true;
            await smtpClient.SendMailAsync(message);
            smtpClient.Dispose();
        }
    }
}
