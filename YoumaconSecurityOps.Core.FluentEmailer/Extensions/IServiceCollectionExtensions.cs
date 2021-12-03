using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YoumaconSecurityOps.Core.FluentEmailer.Services;

namespace YoumaconSecurityOps.Core.FluentEmailer.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddFluentEmailServices(this IServiceCollection services, IConfigurationRoot configuration)
        {
            //---Gmail
            var from = configuration.GetSection("Email")["From"];

            var gmailSender = configuration.GetSection("Gmail")["Sender"];
            var gmailPassword = configuration.GetSection("Gmail")["Password"];
            var gmailPort = Convert.ToInt32(configuration.GetSection("Gmail")["Port"]);

            //---Sendgrid
            var sendGridSender = configuration.GetSection("Sendgrid")["Sender"];
            var sendGridKey = configuration.GetSection("Sendgrid")["SendgridKey"];

            //--Uncomment to use Gmail
            //services
            //    .AddFluentEmail(gmailSender, from)
            //    .AddRazorRenderer()
            //    .AddSmtpSender(new SmtpClient("smtp.gmail.com")
            //    {
            //        UseDefaultCredentials = false,
            //        Port = gmailPort,
            //        Credentials = new NetworkCredential(gmailSender, gmailPassword),
            //        EnableSsl = true,
            //    });

            services
                .AddFluentEmail(sendGridSender, from)
                .AddRazorRenderer()
                .AddSendGridSender(sendGridKey);

            services.AddScoped<IEmailSendingService, EmailSendingService>();

            return services;
        }
    }
}
