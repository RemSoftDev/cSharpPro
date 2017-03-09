using System;
using System.Configuration;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CSharpPro.App_Start
{
	public class BasicAuthorizeAttribute : AuthorizeAttribute
	{
		private const string Realm = "cSharp Pro";

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			var request = HttpContext.Current.Request;
			var authHeader = request.Headers["Authorization"];
			if (authHeader != null)
			{
				var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

				if (authHeaderVal.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) && authHeaderVal.Parameter != null)
				{
					AuthenticateUser(authHeaderVal.Parameter);
				}
				else
				{
					base.OnAuthorization(filterContext);
				}
			}
			else
			{
				NonAuthorized();
			}
		}

		private static void AuthenticateUser(string credentials)
		{
			try
			{
				var encoding = Encoding.GetEncoding("iso-8859-1");
				credentials = encoding.GetString(Convert.FromBase64String(credentials));

				int separator = credentials.IndexOf(':');
				string name = credentials.Substring(0, separator);
				string password = credentials.Substring(separator + 1);

				if (CheckPassword(name, password))
				{
					var identity = new GenericIdentity(name);
					SetPrincipal(new GenericPrincipal(identity, null));
				}
				else
				{
					NonAuthorized();
				}
			}
			catch (FormatException)
			{
				NonAuthorized();
			}
		}

		private static void SetPrincipal(IPrincipal principal)
		{
			Thread.CurrentPrincipal = principal;
			if (HttpContext.Current != null)
			{
				HttpContext.Current.User = principal;
			}
		}

		private static void NonAuthorized()
		{
			HttpContext.Current.Response.StatusCode = 401;
			HttpContext.Current.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", Realm));
		}

		private static bool CheckPassword(string username, string password)
		{
			var section = (AuthenticationSection)ConfigurationManager.GetSection("system.web/authentication");

			foreach (FormsAuthenticationUser user in section.Forms.Credentials.Users)
			{
				if (user.Name == username && user.Password == password) return true;
			}

			return false;
		}
	}
}