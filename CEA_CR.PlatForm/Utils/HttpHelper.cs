using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace CEA_CR.PlatForm.Utils
{
    public static class HttpHelper
    {
        /// <summary>  
        /// 创建GET方式的HTTP请求  
        /// </summary>  
        public static string GetHttpResponse(string url, int timeOut = 3000)  //毫秒
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Timeout = timeOut;
            httpRequest.Method = "GET";
            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            StreamReader sr = new StreamReader(httpResponse.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8"));
            string result = sr.ReadToEnd();
            result = result.Replace("\r", "").Replace("\n", "").Replace("\t", "");
            int status = (int)httpResponse.StatusCode;
            sr.Close();
            return result;
        }
    }
}
