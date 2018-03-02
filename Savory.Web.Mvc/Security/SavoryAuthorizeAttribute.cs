﻿using Savory.Web.Mvc.Exceptions;
using Savory.Web.Mvc.Provider;
using Savory.Web.Security;
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
        /// 获取或设置有权访问控制器或操作方法的用户角色。
        /// </summary>
        [Obsolete("Roles属性在代码中不被使用")]
        public new string Roles
        {
            get { return base.Roles; }
            set { base.Roles = value; }
        }

        /// <summary>
        /// 获取或设置有权访问控制器或操作方法的用户。
        /// </summary>
        [Obsolete("Users属性在代码中不被使用")]
        public new string Users
        {
            get { return base.Users; }
            set { base.Users = value; }
        }

        /// <summary>
        /// 跳转方式
        /// <see cref="Canos.Web.Security.RedirectTo"/>
        /// </summary>
        public RedirectTo RedirectTo { get; set; }

        /// <summary>
        /// 有权访问控制器或操作方法的用户角色
        /// </summary>
        public string[] RoleList { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SavoryAuthorizeAttribute()
        {
            this.RedirectTo = RedirectTo.AbsoluteUri;
        }

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

            //验证成员身份
            if (this.RoleList != null && this.RoleList.Length > 0)
            {
                var provider = UserRoleProviderFactory.GetUserRoleProvider();
                if (provider == null)
                {
                    throw new UserRoleProviderNotRegistException();
                }
                if (provider.IsInRole(this.RoleList))
                {
                    return true;
                }
            }

            context.User = principal;

            return true;
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