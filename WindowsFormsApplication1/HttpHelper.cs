using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace WindowsFormsApplication1
{
    public static class HttpHelper
    {
        /// <summary>  
        /// 创建GET方式的HTTP请求  
        /// </summary>  
        public static string GetHttpResponse(string url, int timeOut = 3000, bool gzip = false, string encoding = "utf-8")  //毫秒
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Timeout = timeOut;
            httpRequest.Method = "GET";

            if (gzip)
            {
                httpRequest.Headers.Add("Accept-Encoding", "gzip,deflate,sdch");
            }

            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();

            Stream responseStream = httpResponse.GetResponseStream();
            if (httpResponse.ContentEncoding.ToLower().Contains("gzip"))
            {
                responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
            }

            StreamReader sr = new StreamReader(responseStream, System.Text.Encoding.GetEncoding(encoding));
            string result = sr.ReadToEnd();
            result = result.Replace("\r", "").Replace("\n", "").Replace("\t", "");
            int status = (int)httpResponse.StatusCode;
            sr.Close();
            return result;
        }
    }
}
