using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Savory.Web.Mvc.Security
{
    public class SavoryAuthorizeAttribute: AuthorizeAttribute
    {
        /// <summary>
        /// <see cref="AuthorizeAttribute.AuthorizeCore(HttpContextBase)"/>
        /// </summary>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            //验证主体
            var principal = SavoryAuthentication.TryParsePrincipal(context);
            if (principal == null)
            {
                return false;
            }

            ////验证成员身份
            //if (this.RoleList != null && this.RoleList.Length > 0)
            //{
            //    var provider = UserRoleProviderFactory.GetUserRoleProvider();
            //    if (provider == null)
            //    {
            //        throw new UserRoleProviderNotRegistException();
            //    }
            //    if (provider.IsInRole(this.RoleList))
            //    {
            //        return true;
            //    }
            //}

            context.User = principal;

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            string url = string.Empty;
            switch (SavorySecurityOptions.RedirectTo)
            {
                case RedirectTo.AbsolutePath:
                    url = filterContext.HttpContext.Request.Url.AbsolutePath;

                    break;
                case RedirectTo.AbsoluteUri:
                default:
                    url = filterContext.HttpContext.Request.Url.AbsoluteUri;

                    break;
            }

            string redirectUrl = string.Concat(SavorySecurityOptions.LoginUrl, "?ReturnUrl=", HttpUtility.UrlEncode(url));

            //验证不通过,直接跳转到相应页面，注意：如果不使用以下跳转，则会继续执行Action方法
            filterContext.Result = new RedirectResult(redirectUrl);
        }
    }
}
