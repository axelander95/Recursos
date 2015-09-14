using System;
using System.Net.Mail;
using System.Net;
namespace Recursos
{
    class Mail
    {
        public int Port { set; get; }
        public string Hostname { get; set; }
        public string From { get; set; }
        public string User { get; set; }
        public string Token{ get; set; }
        public bool Ssl { get; set; }
        public Mail(string Hostname, int Port, string User, string Token, string From, bool Ssl)
        {
            this.Hostname = Hostname;
            this.Port = Port;
            this.User = User;
            this.Token = Token;
            this.From = From;
            this.Ssl = Ssl;
        }
        public string SendMail(string To, string Asunto, string Message)
        {
            string info = String.Empty;
            MailMessage Email = new MailMessage();
            Email.From = new MailAddress(From);
            Email.To.Add(new MailAddress(To));
            Email.Subject = Asunto;
            Email.Body = Message;
            SmtpClient MailClient = new SmtpClient();
            MailClient.Host = Hostname;
            MailClient.Port = Port;
            MailClient.EnableSsl = Ssl;
            MailClient.UseDefaultCredentials = false;
            MailClient.Credentials = new NetworkCredential(User, Token);
            try
            {
                MailClient.Send(Email);
                info = "Correo enviado.";
            }
            catch (Exception ex)
            {
                info = "Error en el envío: " + ex;
            }
            return info;
        }
    }
}
