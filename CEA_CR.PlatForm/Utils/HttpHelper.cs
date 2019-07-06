using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace CEA_CR.PlatForm.Utils
{
    public static class HttpHelper
    {
        /// <summary>  
        /// 创建GET方式的HTTP请求  
        /// </summary>  
        public static string GetHttpResponse(string url, int timeOut = 4000, bool gzip = false, string encoding = "utf-8")  //毫秒
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Timeout = timeOut;
            httpRequest.Method = "GET";

            if (gzip)
            {
                httpRequest.Headers.Add("Accept-Encoding", "gzip,deflate,sdch");
            }

            HttpWebResponse httpResponse = null;
            Stream responseStream = null;
            int retryCount = 1;

            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    responseStream = httpResponse.GetResponseStream();

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
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.Timeout && i < retryCount - 1)
                    {
                        continue;
                    }
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (responseStream != null)
                    {
                        responseStream.Close();
                    }
                    if (httpResponse != null)
                    {
                        httpResponse.Close();
                    }
                }
            }
            return "";
        }
    }
}
