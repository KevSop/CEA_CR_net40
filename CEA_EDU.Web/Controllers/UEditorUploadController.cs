using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;

namespace CEA_EDU.Web.Controllers
{
    public class UEditorUploadController : BaseController
    {
        public static string WebHost = ConfigurationManager.AppSettings["WebHost"];
        public static string ArticlePictureRootPath = ConfigurationManager.AppSettings["ArticlePictureRootPath"];

        [AllowAnonymous]
        public void Index()
        {
            string result = string.Empty;
         
            try
            {
                switch (HttpContext.Request["action"])
                {
                    case "config":
                        WriteJson(HttpContext, UEditorConfig.Items);
                        break;
                    case "uploadimage":
                        UploadFile(HttpContext, new UploadConfig()
                        {
                            AllowExtensions = UEditorConfig.GetStringList("imageAllowFiles"),
                            PathFormat = UEditorConfig.GetString("imagePathFormat"),
                            SizeLimit = UEditorConfig.GetInt("imageMaxSize"),
                            UploadFieldName = UEditorConfig.GetString("imageFieldName")
                        });
                        break;
                    case "uploadscrawl":
                        UploadFile(HttpContext, new UploadConfig()
                        {
                            AllowExtensions = new string[] { ".png" },
                            PathFormat = UEditorConfig.GetString("scrawlPathFormat"),
                            SizeLimit = UEditorConfig.GetInt("scrawlMaxSize"),
                            UploadFieldName = UEditorConfig.GetString("scrawlFieldName"),
                            Base64 = true,
                            Base64Filename = "scrawl.png"
                        });
                        break;
                    case "uploadvideo":
                        UploadFile(HttpContext, new UploadConfig()
                        {
                            AllowExtensions = UEditorConfig.GetStringList("videoAllowFiles"),
                            PathFormat = UEditorConfig.GetString("videoPathFormat"),
                            SizeLimit = UEditorConfig.GetInt("videoMaxSize"),
                            UploadFieldName = UEditorConfig.GetString("videoFieldName")
                        });
                        break;
                    case "uploadfile":
                        UploadFile(HttpContext, new UploadConfig()
                        {
                            AllowExtensions = UEditorConfig.GetStringList("fileAllowFiles"),
                            PathFormat = UEditorConfig.GetString("filePathFormat"),
                            SizeLimit = UEditorConfig.GetInt("fileMaxSize"),
                            UploadFieldName = UEditorConfig.GetString("fileFieldName")
                        });
                        break;
                    case "listimage":
                        //action = new ListFileManager(HttpContext, Config.GetString("imageManagerListPath"), Config.GetStringList("imageManagerAllowFiles"));
                        break;
                    case "listfile":
                        //action = new ListFileManager(HttpContext, Config.GetString("fileManagerListPath"), Config.GetStringList("fileManagerAllowFiles"));
                        break;
                    case "catchimage":
                        //action = new CrawlerHandler(HttpContext);
                        break;
                    default:
                        result = "action 参数为空或者 action 不被支持。";
                        break;
                }
            }
            catch (Exception ex)
            {
                
            }

            HttpContext.Response.Write(result);
            HttpContext.Response.End();
            
        }

        public void UploadFile(HttpContextBase context, UploadConfig uploadConfig)
        {
            UploadResult Result = new UploadResult();
            var Request = context.Request;

            //验证文件类型
            var file = Request.Files[uploadConfig.UploadFieldName];
            if (!CheckFileType(file.FileName, uploadConfig))
            {
                Result.State = UploadState.TypeNotAllow;
                WriteResult(context, Result);
                return;
            }

            //验证文件大小
            if (!CheckFileSize(file.ContentLength, uploadConfig))
            {
                Result.State = UploadState.SizeLimitExceed;
                WriteResult(context, Result);
                return;
            }
            try
            {
                //根据上传类型，走相应的上传服务
                string fileName = string.Empty;
                string url = string.Empty;
                switch (Request["action"])
                {
                    case "uploadvideo":
                        url = UploadVideoNewVersion(context, uploadConfig, out fileName);

                        //if (ConfigHelper.GetConfigBean("UseNewVersionUploadVideo") == "T")
                        //{
                        //    url = UploadVideoNewVersion(context, out fileName);
                        //}
                        //else
                        //{
                        //    url = UploadVidio(context, out fileName);
                        //}
                        break;
                    case "uploadimage":
                        Result.UrlList = UploadImage(context, uploadConfig, out fileName);
                        break;
                    case "uploadscrawl":
                        Result.UrlList = UploadImage(context, uploadConfig, out fileName);
                        break;
                }
                Result.OriginFileName = fileName;
                Result.Url = url;
                Result.State = UploadState.Success;
            }
            catch (Exception e)
            {
                Result.State = UploadState.FileAccessError;
                Result.ErrorMessage = e.Message;
            }
            finally
            {
                WriteResult(context, Result);
            }
        }

        #region 上传图片，视频相关逻辑
        /// <summary>
        /// 上传图片
        /// </summary>
        public List<string> UploadImage(HttpContextBase context, UploadConfig uploadConfig, out string fileName)
        {
            var paths = new List<string>();
            fileName = string.Empty;

            try
            {
                HttpFileCollectionBase files = context.Request.Files;
                if (files == null || files.Count == 0)
                {
                    return paths;
                }

                var file = files[0];

                if (file == null || file.ContentLength == 0) return paths;

                byte[] bytes = new byte[file.ContentLength];
                file.InputStream.Read(bytes, 0, file.ContentLength);
                file.InputStream.Close();

   
                //存在服务器文件夹下
                fileName = Path.GetFileNameWithoutExtension(file.FileName) + new Random().Next(100) + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(ArticlePictureRootPath, fileName);
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();

                paths.Add("http://" + WebHost + "/CEA_EDU/DigitalContent/GetArticlePictureByPath?path=" + Server.UrlEncode(fileName));
            }
            catch (Exception ex)
            {
              
            }

            return paths;
        }

        /// <summary>
        /// 上传视频
        /// </summary>
        /// <param name="fileName">视频的文件名称</param>
        /// <returns>视频的完整路径</returns>
        public string UploadVidio(HttpContextBase context, UploadConfig uploadConfig, out string fileName)
        {
            fileName = string.Empty;
            string returnPath = string.Empty;

            try
            {
                HttpFileCollectionBase files = context.Request.Files;
                if (files == null || files.Count == 0)
                {
                    return string.Empty;
                }

                HttpPostedFileBase file = files[0];
                if (file == null)
                {
                    return string.Empty;
                }

                byte[] bytes = new byte[file.ContentLength];
                file.InputStream.Read(bytes, 0, file.ContentLength);
                file.InputStream.Close();

                string channel = "hoteldomestic";
                string content = GetFileContent(channel, "mp4");

                string result = UploadPost(content, bytes);
                string[] infoArray = result.Split(',');
                if (infoArray.Length <= 1)
                {
                    return string.Empty;
                }

                var path = infoArray[1];
                if (string.IsNullOrWhiteSpace(path))
                {
                    return string.Empty;
                }

                string[] pathArray = path.Replace("\"", "").Split(':');
                if (path.Length > 1)
                {
                    fileName = pathArray[1];
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        //returnPath = ConfigHelper.GetConfigBean("VisitVideoWS") + fileName;
                        returnPath = fileName;
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return returnPath;
        }

        /// <summary>
        /// 新版本的上传视频
        /// </summary>
        /// <param name="fileName">视频的文件名称</param>
        /// <returns>视频的完整路径</returns>
        public string UploadVideoNewVersion(HttpContextBase context, UploadConfig uploadConfig, out string fileName)
        {
            fileName = string.Empty;
            string returnPath = string.Empty;

            try
            {
                HttpFileCollectionBase files = context.Request.Files;
                if (files == null || files.Count == 0) return string.Empty;

                HttpPostedFileBase file = files[0];
                if (file == null) return string.Empty;

                var bytes = new byte[file.ContentLength];
                file.InputStream.Read(bytes, 0, file.ContentLength);
                file.InputStream.Close();

                //var domain = ConfigHelper.GetConfigBean("UploadVideoDomain");
                string domain = "";

                var token = GetToken(domain);
                if (string.IsNullOrEmpty(token)) return string.Empty;

                var channel = "hoteldomestic";
                var url = string.Format("http://{0}/video/v1/api/upload?channel={1}&token={2}", domain, channel, token);
                var request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "video/mpeg4";
                request.Headers.Add("Crc", GetMd5(bytes));
                request.ContentLength = bytes.Length;

                using (Stream newStream = request.GetRequestStream())
                {
                    newStream.Write(bytes, 0, bytes.Length);
                }
                var content = string.Empty;
                var response = request.GetResponse() as HttpWebResponse;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        content = sr.ReadToEnd();
                    }
                }

                if (string.IsNullOrEmpty(content)) return string.Empty;

                var jsonContent = JObject.Parse(content);
                if (jsonContent != null)
                {
                    fileName = jsonContent["file_name"].ToString();
                    returnPath = jsonContent["url"].ToString();
                }
            }
            catch (Exception ex)
            {
            }
            return returnPath;
        }

        private string GetFileContent(string channel, string fileExt)
        {
            string json = "{\"Channel\":\"{channel}\",\"FileExt\":\"{fileext}\"}";
            json = json.Replace("{channel}", channel);
            json = json.Replace("{fileext}", fileExt);
            return json;
        }

        private string UploadPost(string upfile, byte[] files)
        {
            string content = string.Empty;
            
            string uploadVideoWS = "";    // ConfigHelper.GetConfigBean("UploadVideoWS")
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uploadVideoWS);

            request.Headers.Add("ctrip_upfile", upfile);
            request.Method = "POST";
            request.ContentLength = files.Length;
            using (Stream newStream = request.GetRequestStream())
            {
                newStream.Write(files, 0, files.Length);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(GetContentType(response.ContentType))))
            {
                content = sr.ReadToEnd();
            }
            return content.Replace("\\", string.Empty).TrimStart('"').TrimEnd('"');
        }

        public string GetToken(string domain)
        {
            var clientId = "110123"; //向框架确认过传一个非空值即可
            var request = WebRequest.Create(string.Format("http://{0}/video/v1/api/gettoken?clientid={1}", domain, clientId)) as HttpWebRequest;
            request.Method = "GET";

            var token = string.Empty;
            var response = request.GetResponse() as HttpWebResponse;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    token = sr.ReadToEnd();
                }
            }

            return token;
        }

        private string GetContentType(string p_strContentType)
        {
            if (string.IsNullOrEmpty(p_strContentType))
            {
                return "UTF-8";
            }
            return p_strContentType.Split(';')[1].Split('=')[1];
        }

        private string GetMd5(byte[] fileBts)
        {
            byte[] tmp;
            int size5M = 5 * 1024 * 1024;
            const int size10M = 10 * 1024 * 1024;
            if (fileBts.Length > size10M)
            {
                tmp = new byte[size10M];
                Array.Copy(fileBts, 0, tmp, 0, size5M);
                Array.Copy(fileBts, fileBts.Length - size5M, tmp, size5M, size5M);
            }
            else
            {
                tmp = fileBts;
            }
            MD5 md5 = new MD5CryptoServiceProvider();
            var bts = md5.ComputeHash(tmp);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bts.Length; i++)
            {
                sb.Append(bts[i].ToString("x2"));
            }
            return sb.ToString();
        }


        #endregion

        private void WriteResult(HttpContextBase context, UploadResult Result)
        {
            var responseObj = new
            {
                state = GetStateMessage(Result.State),
                url = Result.Url,
                urllist = Result.UrlList,
                title = Result.OriginFileName,
                original = Result.OriginFileName,
                error = Result.ErrorMessage
            };

            WriteJson(context, responseObj);
        }

        private void WriteJson(HttpContextBase context, object responseObj)
        {
            string jsonpCallback = context.Request["callback"];
            string json = JsonConvert.SerializeObject(responseObj);
            if (String.IsNullOrWhiteSpace(jsonpCallback))
            {
                context.Response.AddHeader("Content-Type", "text/plain");
                context.Response.Write(json);
            }
            else
            {
                context.Response.AddHeader("Content-Type", "application/javascript");
                context.Response.Write(String.Format("{0}({1});", jsonpCallback, json));
            }

            context.Response.End();
        }

        private string GetStateMessage(UploadState state)
        {
            switch (state)
            {
                case UploadState.Success:
                    return "SUCCESS";
                case UploadState.FileAccessError:
                    return "文件访问出错，请检查写入权限";
                case UploadState.SizeLimitExceed:
                    return "文件大小超出服务器限制";
                case UploadState.TypeNotAllow:
                    return "不允许的文件格式";
                case UploadState.NetworkError:
                    return "网络错误";
            }
            return "未知错误";
        }

        private bool CheckFileType(string filename, UploadConfig uploadConfig)
        {
            var fileExtension = Path.GetExtension(filename).ToLower();
            return uploadConfig.AllowExtensions.Select(x => x.ToLower()).Contains(fileExtension);
        }

        private bool CheckFileSize(int size, UploadConfig uploadConfig)
        {
            return size < uploadConfig.SizeLimit;
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class UploadConfig
    {
        /// <summary>
        /// 文件命名规则
        /// </summary>
        public string PathFormat { get; set; }

        /// <summary>
        /// 上传表单域名称
        /// </summary>
        public string UploadFieldName { get; set; }

        /// <summary>
        /// 上传大小限制
        /// </summary>
        public int SizeLimit { get; set; }

        /// <summary>
        /// 上传允许的文件格式
        /// </summary>
        public string[] AllowExtensions { get; set; }

        /// <summary>
        /// 文件是否以 Base64 的形式上传
        /// </summary>
        public bool Base64 { get; set; }

        /// <summary>
        /// Base64 字符串所表示的文件名
        /// </summary>
        public string Base64Filename { get; set; }
    }

    public class UploadResult
    {
        public UploadState State { get; set; }
        public string Url { get; set; }
        public List<string> UrlList { get; set; }
        public string OriginFileName { get; set; }

        public string ErrorMessage { get; set; }
    }

    public enum UploadState
    {
        Success = 0,
        SizeLimitExceed = -1,
        TypeNotAllow = -2,
        FileAccessError = -3,
        NetworkError = -4,
        Unknown = 1,
    }

    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public static class UEditorConfig
    {
        private static bool noCache = true;
        private static JObject BuildItems()
        {
            var json = File.ReadAllText(HttpContext.Current.Server.MapPath("~/FlatLab/ueres/ueditorConfig.json"));
            return JObject.Parse(json);
        }

        public static JObject Items
        {
            get
            {
                if (noCache || _Items == null)
                {
                    _Items = BuildItems();
                }
                return _Items;
            }
        }
        private static JObject _Items;


        public static T GetValue<T>(string key)
        {
            return Items[key].Value<T>();
        }

        public static String[] GetStringList(string key)
        {
            return Items[key].Select(x => x.Value<String>()).ToArray();
        }

        public static String GetString(string key)
        {
            return GetValue<String>(key);
        }

        public static int GetInt(string key)
        {
            return GetValue<int>(key);
        }
    }
}
