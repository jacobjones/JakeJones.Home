using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using JakeJones.Home.Core.Implementation.Configuration;
using JakeJones.Home.Core.Managers;

namespace JakeJones.Home.Core.Implementation.Managers
{
	public class EmailNotificationManager : INotificationManager
	{
		private readonly INotificationOptions _notificationOptions;

		public EmailNotificationManager(INotificationOptions notificationOptions)
		{
			_notificationOptions = notificationOptions;
		}

		public async Task SendNotificationAsync(string subject, string message)
		{
			var smtpClient = new SmtpClient
			{
				Host = _notificationOptions.Host,
				Port = _notificationOptions.Port,
				EnableSsl = true,
				Credentials = new NetworkCredential(_notificationOptions.Username, _notificationOptions.Password)
			};

			await smtpClient.SendMailAsync(new MailMessage(_notificationOptions.From, _notificationOptions.To, subject, message));
		}
	}
}