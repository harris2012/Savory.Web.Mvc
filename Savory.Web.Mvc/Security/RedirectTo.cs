using System;
using System.Collections.Generic;
using System.Text;

namespace Savory.Web.Mvc.Security
{
    /// <summary>
    /// 跳转方式
    /// </summary>
    public enum RedirectTo
    {
        /// <summary>
        /// 跳转到绝对URI
        /// </summary>
        AbsoluteUri,

        /// <summary>
        /// 跳转到绝对URI的路径
        /// </summary>
        AbsolutePath
    }
}
