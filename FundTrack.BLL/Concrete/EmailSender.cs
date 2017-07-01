using FundTrack.BLL.Abstract;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Class for sending Emails
    /// </summary>
    public class EmailSender : IEmailSender
    {
        // setting for smtp client
        private const string SmtpClient = "smtp.gmail.com";
        private const int Port = 465;
        private const bool Ssl = true;

        /// <summary>
        /// Send email to givend email
        /// </summary>
        /// <param name="currentHost">Current application host</param>
        /// <param name="email">Addres to send Email</param>
        /// <param name="guid">Guid of user</param>
        public void SendMail(string currentHost, string email,string guid)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(Properties.Resources.ResourceManager.GetString("From"),
                                                     Properties.Resources.ResourceManager.GetString("FromEmail")));

            emailMessage.To.Add(new MailboxAddress(string.Empty, email));
            emailMessage.Subject = Properties.Resources.ResourceManager.GetString("Subject");

            
            emailMessage.Body = new TextPart("html")
            {
                Text = Properties.Resources.ResourceManager.GetString("body").Replace("linkBody",
                                                            $"{currentHost} + /new_password/ + {guid}")
            };

            using (var client = new SmtpClient())
            {              
                client.Connect(SmtpClient, Port, Ssl);

                client.Authenticate(Properties.Resources.ResourceManager.GetString("Login"),
                                    Properties.Resources.ResourceManager.GetString("Password"));

                client.Send(emailMessage);

                client.Disconnect(true);
            }
        }
    }
}
