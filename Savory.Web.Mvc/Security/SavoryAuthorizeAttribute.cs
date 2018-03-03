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
        /// 跳转方式
        /// <see cref="Canos.Web.Security.RedirectTo"/>
        /// </summary>
        public RedirectTo RedirectTo { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SavoryAuthorizeAttribute()
        {
            this.RedirectTo = RedirectTo.AbsoluteUri;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            string url = string.Empty;
            switch (RedirectTo)
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
