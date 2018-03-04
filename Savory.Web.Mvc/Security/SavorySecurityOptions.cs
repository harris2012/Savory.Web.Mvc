using System;
using System.Collections.Generic;
using System.Text;

namespace Savory.Web.Mvc.Security
{
    public static class SavorySecurityOptions
    {
        /// <summary>
        /// 跳转方式
        /// <see cref="Canos.Web.Security.RedirectTo"/>
        /// </summary>
        public static RedirectTo RedirectTo { get; set; } = RedirectTo.AbsoluteUri;

        public static string LoginUrl { get; set; }
    }
}
