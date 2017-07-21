using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEA_CR.PlatForm.Utils
{
    public static class ConfigStatic
    {
        private static string _userName;
        private static string _password;
        private static string _GetCurrentCourseUrl;
        private static string _GetCourseScheduleUrl;
        public static string userName
        {
            get
            {
                if (string.IsNullOrEmpty(_userName))
                {
                    string config = System.Configuration.ConfigurationManager.AppSettings["userName"];
                    byte[] bytedata = Encoding.Default.GetBytes(config);
                    _userName = Convert.ToBase64String(bytedata);
                }
                return _userName;
            }
        }
        public static string password
        {
            get
            {
                if (string.IsNullOrEmpty(_password))
                {
                    string config = System.Configuration.ConfigurationManager.AppSettings["password"];
                    byte[] bytedata = Encoding.Default.GetBytes(config);
                    _password = Convert.ToBase64String(bytedata);
                }
                return _password;
            }
        }
        public static string GetCurrentCourseUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_GetCurrentCourseUrl))
                {
                    string config = System.Configuration.ConfigurationManager.AppSettings["GetCurrentCourseUrl"];
                    _GetCurrentCourseUrl = config;
                }
                return _GetCurrentCourseUrl;
            }
        }
        public static string GetCourseScheduleUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_GetCourseScheduleUrl))
                {
                    string config = System.Configuration.ConfigurationManager.AppSettings["GetCourseScheduleUrl"];
                    _GetCourseScheduleUrl = config;
                }
                return _GetCourseScheduleUrl;
            }
        }
    }
}
