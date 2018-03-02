using System;
using System.Collections.Generic;
using System.Text;

namespace Savory.Web.Mvc.Provider
{
    public interface UserRoleProvider
    {
        /// <summary>
        /// 确定成员是否在指定角色中
        /// </summary>
        /// <param name="roleList"></param>
        /// <returns></returns>
        bool IsInRole(string[] roleList);
    }
}
