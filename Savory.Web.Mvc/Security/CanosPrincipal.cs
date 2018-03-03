using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Web.Security;

namespace Savory.Web.Mvc.Security
{
    public class CanosPrincipal : IPrincipal
    {
        /// <summary>
        /// 标识对象
        /// <see cref="IPrincipal.Identity"/>
        /// </summary>
        public IIdentity Identity { get; private set; }

        /// <summary>
        /// 判断当前角色是否属于指定的角色
        /// <see cref="IPrincipal.IsInRole(string)"/>
        /// </summary>
        [Obsolete("在代码中不被使用")]
        public bool IsInRole(string role)
        {
            return false;
        }

        public CanosPrincipal(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }

            this.Identity = new FormsIdentity(ticket);
        }
    }
}
