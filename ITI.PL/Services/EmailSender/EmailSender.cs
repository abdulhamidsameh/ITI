
using System.Net;
using System.Net.Mail;

namespace ITI.PL.Services.EmailSender
{
	public class EmailSender : IEmailSender
	{
		private readonly IConfiguration _configuration;

		public EmailSender(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public async Task SendAsync(string from,string recipients, string subject, string body)
		{
			var senderEmail = _configuration["EmailSettings:SenderEmail"];
			var senderPassword = _configuration["EmailSettings:SenderPassword"];
			var emailMessage = new MailMessage();
			emailMessage.From = new MailAddress(from);
			emailMessage.To.Add(recipients);
			emailMessage.Subject = subject;
			emailMessage.Body = $"<html><body>{body}</body></html>";
			emailMessage.IsBodyHtml = true;
			var smtpClient = new SmtpClient(_configuration["EmailSettings:SMTPClientServer"], int.Parse(_configuration["EmailSettings:SMTPClientPort"]))
			{
				Credentials = new NetworkCredential(senderEmail, senderPassword),
				EnableSsl = true
			};
			await smtpClient.SendMailAsync(emailMessage);


		}
	}
}
