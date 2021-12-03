using System.Collections.Generic;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.FluentEmailer.Services
{
    public interface IEmailSendingService
    {
        #region Gmail Sending Methods
        /// <summary>
        /// Sends a plain text email from us to <paramref name="recipientName"/> via their provided <paramref name="recipientEmail"/>
        /// </summary>
        /// <param name="recipientEmail"></param>
        /// <param name="recipientName"></param>
        Task SendPlaintextGmail(string recipientEmail, string recipientName);

        /// <summary>
        /// Sends a rendered html5 email from us to <paramref name="recipientName"/> via their provided <paramref name="recipientEmail"/>
        /// </summary>
        /// <param name="recipientEmail"></param>
        /// <param name="recipientName"></param>
        Task SendHtmlGmail(string recipientEmail, string recipientName);

        /// <summary>
        /// Sends a rendered html5 email with attachments from us to <paramref name="recipientName"/> via their provided <paramref name="recipientEmail"/>
        /// </summary>
        /// <param name="recipientEmail"></param>
        /// <param name="recipientName"></param>
        Task SendHtmlWithAttachmentGmail(string recipientEmail, string recipientName);
        #endregion
        #region Sendgrid Sending Methods

        /// <summary>
        /// Sends a plaintext email from us to <paramref name="recipientName"/> via their provided <paramref name="recipientEmail"/> and sendgrid
        /// </summary>
        /// <param name="recipientEmail"></param>
        /// <param name="recipientName"></param>
        Task SendPlaintextSendgrid(string recipientEmail, string recipientName);

        /// <summary>
        /// Sends a rendered html5 email from us to <paramref name="recipientName"/> via their provided <paramref name="recipientEmail"/> and sendgrid
        /// </summary>
        /// <param name="recipientEmail"></param>
        /// <param name="recipientName"></param>
        Task SendHtmlSendgrid(string recipientEmail, string recipientName);


        /// <summary>
        /// Sends a rendered html5 email with attachments from us to <paramref name="recipientName"/> via their provided <paramref name="recipientEmail"/> and sendgrid
        /// </summary>
        /// <param name="recipientEmail"></param>
        /// <param name="recipientName"></param>

        Task SendHtmlWithAttachmentSendgrid(string recipientEmail, string recipientName);

        /// <summary>
        /// Sends an email from us to the provided <paramref name="recipientEmails"/>
        /// </summary>
        /// <param name="recipientEmails"></param>
        Task SendSendgridBulk(IEnumerable<string> recipientEmails);
        #endregion
    }
}
