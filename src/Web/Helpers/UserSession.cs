namespace Web.Helpers
{
    using System;
    using System.Web;
    using System.Web.Security;
    using Core.Models;
    using Felice.Core;

    public class UserSession
    {
        public void SetAuthenticationToken(string name, bool isPersistant, Usuario usuario)
        {
            var ticket = new FormsAuthenticationTicket(
                1, name, DateTime.Now, DateTime.Now.AddYears(1), isPersistant, usuario.Id.ToString());

            var cookieData = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieData)
            {
                HttpOnly = true,
                Expires = ticket.Expiration
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public long GetCurrentUserId()
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (cookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

                    return ticket.UserData.ToLong();
                }
            }
            catch (Exception ex)
            {
            }

            return 0;
        }
    }
}