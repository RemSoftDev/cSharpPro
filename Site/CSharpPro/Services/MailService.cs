using System;
using System.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CSharpPro.Services
{
	public class MailService
	{
		public string NotificationMail { get { return ConfigurationManager.AppSettings["NotificationMail"]; } }
		public string MessageDisplayNmae { get { return ConfigurationManager.AppSettings["MessageDisplayNmae"]; } }

		public void SendRequestMail(string name, string emailTo, string message)
		{
			var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
			var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
			var mail = new MailMessage();

			mail.From = new MailAddress(smtpSection.From, MessageDisplayNmae);
			mail.Sender = new MailAddress(smtpSection.From, MessageDisplayNmae);
			mail.To.Add(NotificationMail);
			mail.Subject = "CSharp Company Request";
			mail.IsBodyHtml = true;
			var messageBulder = new StringBuilder();
			messageBulder.AppendFormat("<h3>name: {0}</h3>", name);
			messageBulder.AppendFormat("<h3>email: {0}</h3>", emailTo);
			messageBulder.AppendFormat("<p>{0}</p>", message);
			mail.Body = messageBulder.ToString();
			Send(mail);
		}

		public void SendConfirmMail(string emailTo)
		{
			var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
			var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
			var mail = new MailMessage();

			mail.From = new MailAddress(smtpSection.From, MessageDisplayNmae);
			mail.Sender = new MailAddress(smtpSection.From, MessageDisplayNmae);
			mail.To.Add(emailTo);
			mail.Subject = "CSharp Company Thanks";
			mail.IsBodyHtml = true;
			mail.Body = "Thank you for you request!";

			Send(mail);
		}

		public bool Send(MailMessage mail)
		{
			if (mail == null)
				throw new ArgumentNullException("mail");

			var client = new SmtpClient();
			var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

			client.Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
			client.SendCompleted += (s, e) =>
			{
				client.Dispose();
				mail.Dispose();
			};

			try
			{
				ThreadPool.QueueUserWorkItem(x =>
				{
					ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
					client.SendAsync(mail, mail);
				});
			}
			catch (Exception ex)
			{
				return false;
			}

			return true;
		}
	}
}