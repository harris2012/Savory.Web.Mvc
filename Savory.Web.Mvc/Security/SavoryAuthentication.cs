using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Savory.Web.Mvc.Security
{
    public static class SavoryAuthentication
    {
        public static void SetAuthCookie(string userName)
        {
            SetAuthCookie(userName, false, string.Empty);
        }

        public static void SetAuthCookie(string userName, string userData)
        {
            SetAuthCookie(userName, false, userData);
        }

        public static void SetAuthCookie(string userName, bool rememberMe)
        {
            SetAuthCookie(userName, rememberMe, string.Empty);
        }

        public static void SetAuthCookie(string userName, bool rememberMe, string userData)
        {
            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }

            //创建ticket
            var ticket = new FormsAuthenticationTicket(2, userName, DateTime.Now, DateTime.Now.AddDays(14), rememberMe, userData);

            //加密ticket
            var cookieValue = FormsAuthentication.Encrypt(ticket);

            //创建Cookie
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue);
            cookie.HttpOnly = true;
            //cookie.Secure = FormsAuthentication.RequireSSL;
            //cookie.Domain = FormsAuthentication.CookieDomain;
            //cookie.Path = FormsAuthentication.FormsCookiePath;

            //写入Cookie
            HttpContext.Current.Response.Cookies.Remove(cookie.Name);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static CanosPrincipal TryParsePrincipal(HttpContextBase context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // 1. 读登录Cookie
            var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {
                return null;
            }

            try
            {
                // 2. 解密Cookie值，获取FormsAuthenticationTicket对象
                var ticket = FormsAuthentication.Decrypt(cookie.Value);

                return new CanosPrincipal(ticket);
            }
            catch
            {
                /* 有异常也不要抛出，防止攻击者试探。 */
            }

            return null;
        }

        /// <summary>
        /// 登出
        /// </summary>
        public static void SignOut()
        {
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
        }
    }
}

