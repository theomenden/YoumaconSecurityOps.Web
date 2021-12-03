using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FluentEmail.Core;
using Microsoft.Extensions.DependencyInjection;

namespace YoumaconSecurityOps.Core.FluentEmailer.Services
{
    public class EmailSendingService : IEmailSendingService
    {
        private readonly IServiceProvider _serviceProvider;

        public EmailSendingService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task SendPlaintextGmail(string recipientEmail, string recipientName)
        {
            using var scope = _serviceProvider.CreateScope();
            var mailer = scope.ServiceProvider.GetRequiredService<IFluentEmail>();
            var email = mailer
                .To(recipientEmail, recipientName)
                .Subject("Hello there from DI in Class!")
                .Body("This is a plain text message using Gmail");

            await email.SendAsync();
        }

        public async Task SendHtmlGmail(string recipientEmail, string recipientName)
        {
            using var scope = _serviceProvider.CreateScope();
            var mailer = scope.ServiceProvider.GetRequiredService<IFluentEmail>();
            var email = mailer
                .To(recipientEmail, recipientName)
                .Subject("Hello there HTML")
                .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/wwwroot/emails/sample.cshtml",
                    new
                    {
                        Name = recipientName,
                    });

            await email.SendAsync();
        }

        public async Task SendHtmlWithAttachmentGmail(string recipientEmail, string recipientName)
        {
            using var scope = _serviceProvider.CreateScope();
            var mailer = scope.ServiceProvider.GetRequiredService<IFluentEmail>();
            var email = mailer
                .To(recipientEmail, recipientName)
                .Subject("Hello there With Attachment")
                .AttachFromFilename($"{Directory.GetCurrentDirectory()}/wwwroot/files/sample.pdf", "application/pdf", "Application Form")
                .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/wwwroot/emails/sample.cshtml",
                    new
                    {
                        Name = recipientName,
                    });

            await email.SendAsync();
        }

        public async Task SendPlaintextSendgrid(string recipientEmail, string recipientName)
        {
            using var scope = _serviceProvider.CreateScope();
            var mailer = scope.ServiceProvider.GetRequiredService<IFluentEmail>();
            var email = mailer
                .To(recipientEmail, recipientName)
                .Subject("Hello Plaintext from Sendrid")
                .Body("Hello there " + recipientName + ", How are you doing today");

            await email.SendAsync();
        }

        public async Task SendHtmlSendgrid(string recipientEmail, string recipientName)
        {
            using var scope = _serviceProvider.CreateScope();
            var mailer = scope.ServiceProvider.GetRequiredService<IFluentEmail>();
            var email = mailer
                .To(recipientEmail, recipientName)
                .Subject("Hello there Sendgrid HTML")
                .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/wwwroot/emails/sample.cshtml",
                    new
                    {
                        Name = recipientName,
                    });

            await email.SendAsync();
        }

        public async Task SendHtmlWithAttachmentSendgrid(string recipientEmail, string recipientName)
        {
            using var scope = _serviceProvider.CreateScope();
            var mailer = scope.ServiceProvider.GetRequiredService<IFluentEmail>();
            var email = mailer
                .To(recipientEmail, recipientName)
                .Subject("Hello there Sendgrid With Attachment")
                .AttachFromFilename($"{Directory.GetCurrentDirectory()}/wwwroot/files/sample.pdf", "application/pdf", "Application Form")
                .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/wwwroot/emails/sample.cshtml",
                    new
                    {
                        Name = recipientName,
                    });

            await email.SendAsync();
        }

        public async Task SendSendgridBulk(IEnumerable<string> recipientEmails)
        {
            using var scope = _serviceProvider.CreateScope();
            var factory = scope.ServiceProvider.GetRequiredService<IFluentEmailFactory>();

            foreach (var recipient in recipientEmails)
            {
                var email = factory
                    .Create()
                    .To(recipient)
                    .Subject("Bulk Emails from Sendgrid")
                    .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/wwwroot/emails/sample.cshtml",
                        new
                        {
                            Name = "Stranger",
                        });
                await email.SendAsync();
            }
        }
    }
}
