﻿using MailKit.Net.Smtp;
using MimeKit;
using PrintManagerment_API.Application.Handle.HandleEmail;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Application.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.ImplementServices
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _configuration;
        public EmailService(EmailConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> SendEmail(EmailMessage emailMessage)
        {
            var message = CreateEmailMessage(emailMessage);
            await Send(message);
            var recipients = string.Join(", ", message.To);
            return ResponseMessage.GetEmailSuccessMessage(recipients);
        }
        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _configuration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }
        private async Task Send(MimeMessage message)
        {
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_configuration.SmtpServer, _configuration.Port, true);
                client.AuthenticationMechanisms.Remove("XAUTH2");
                await client.AuthenticateAsync(_configuration.Username, _configuration.Password);
                await client.SendAsync(message);
            }
            catch
            {
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}
