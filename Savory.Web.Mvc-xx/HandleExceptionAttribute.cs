using Savory.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Savory.Web.Mvc
{
    /// <summary>
    /// 记录异常的过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class HandleExceptionAttribute : HandleErrorAttribute, IExceptionFilter
    {
        /// <summary>
        /// <see cref="System.Web.Mvc.HandleErrorAttribute.OnException"/>
        /// </summary>
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext != null && !filterContext.ExceptionHandled)
            {
                string controllerName = filterContext.RouteData.Values["controller"].ToString();
                string actionName = filterContext.RouteData.Values["action"].ToString();
                string title = string.Format(Thread.CurrentThread.CurrentCulture, "{0}.{1}", controllerName, actionName);

                ILog log = LogManager.GetLogger(title);

                log.Error(filterContext.Exception.Message, filterContext.Exception.ToString());
            }

            base.OnException(filterContext);
        }
    }
}
