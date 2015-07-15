using DriverSolutions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Services
{
    public class EmailService
    {
        public string Host { get; private set; }
        public int Port { get; private set; }
        public bool EnableSsl { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        private SmtpClient Smtp;

        public EmailService()
            : this(
                GLOB.Settings.Get<string>(3),//host
                GLOB.Settings.Get<int>(4),//port
                GLOB.Settings.Get<bool>(5),//use SSL
                GLOB.Settings.Get<string>(6),//username
                GLOB.Settings.Get<string>(7))//password
        {
        }

        public EmailService(string host, int port, bool enableSsl, string username, string password)
        {
            this.Smtp = new SmtpClient(this.Host, this.Port);
            this.Smtp.EnableSsl = this.EnableSsl;
            this.Smtp.Credentials = new NetworkCredential(this.Username, this.Password);
        }

        public async Task<CheckResult> SendEmail(string sender, string recipient, string subject, string body)
        {
            return await this.SendEmail(new MailMessage(sender, recipient, subject, body));
        }

        public async Task<CheckResult> SendEmail(MailMessage message)
        {
            try
            {
                await this.Smtp.SendMailAsync(message);
                return new CheckResult();
            }
            catch (Exception ex)
            {
                return new CheckResult(ex);
            }
        }
    }
}
