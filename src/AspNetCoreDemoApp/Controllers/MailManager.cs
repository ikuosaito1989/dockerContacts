using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace src.Manager {
    public class MailManager {

        public MailManager () {
            this.Host = "smtp.gmail.com";
            this.Port = 587;
            this.UserName = "itunestool.jp@gmail.com";
            this.PassWord = "irxgbtffgglufwot";
            this.From = "itunestool.jp@gmail.com";
            this.To = "itunestool.jp@gmail.com";
        }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        public async Task SendEmailAsync () {

            var emailMessage = new MimeMessage ();

            emailMessage.From.Add (new MailboxAddress (this.UserName, this.From));

            emailMessage.To.Add (new MailboxAddress (this.To));

            emailMessage.Subject = this.Subject;

            emailMessage.Body = new TextPart ("plain") { Text = this.Body };

            using (var client = new SmtpClient ()) {

                await client.ConnectAsync (this.Host, this.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync (this.UserName, this.PassWord);
                await client.SendAsync (emailMessage);
                await client.DisconnectAsync (true);
            }
        }
    }

}