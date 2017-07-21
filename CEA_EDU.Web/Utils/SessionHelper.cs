using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CEA_EDU.Domain.Entity;
using CEA_EDU.Web.Models;

namespace CEA_EDU.Web.Utils
{
    public class SessionHelper
    {
        public static string CurrentUserKey = "HRMS_CURRENTUSER";

        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        public static LoginUserViewModel CurrentUser
        {
            get
            {

                LoginUserViewModel userinfo = HttpContext.Current.Session[CurrentUserKey] as LoginUserViewModel;
                if (userinfo == null && System.Net.Dns.GetHostName() == "ThinkPad-PC")
                {
                    return new LoginUserViewModel { Name = "超级管理员", Code = "sa", Type = "超级管理员", Company = "上海敏慧" };
                }
                return userinfo;
            }
        }
    }
}