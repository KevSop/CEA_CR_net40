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
using CEA_EDU.Web.Models;
using CEA_EDU.Domain.Manager;
using CEA_EDU.Domain.Entity;
using CEA_EDU.Web.Utils;

namespace CEA_EDU.Web.Controllers
{
    public class DigitalContentController : BaseController
    {
        public static List<string> PictureExtName = new List<string>(){".bmp", ".gif", ".jpg", ".jpeg", ".png"};
        public static List<string> VideoExtName = new List<string>() { ".avi", ".rmvb", ".rm", ".mpg", ".mpeg", ".wmv", ".mp4", ".mkv" };

        //公司校园数字资源信息
        public ActionResult DigitalContentIndex()
        {
            return View();
        }

        public void GetDigitalContents()
        {
            //用于序列化实体类的对象  
            JavaScriptSerializer jss = new JavaScriptSerializer();

            List<DigitalContentEntity> list = new List<DigitalContentEntity>();

            //请求中携带的条件  
            string order = HttpContext.Request.Params["order"];
            string sort = HttpContext.Request.Params["sort"];
            string searchKey = HttpContext.Request.Params["search"];
            int offset = Convert.ToInt32(HttpContext.Request.Params["offset"]);
            int pageSize = Convert.ToInt32(HttpContext.Request.Params["limit"]);
            string category = HttpContext.Request.Params["category"];   //资源类别-共享文件夹2级目录（公司介绍、校园介绍）
            string type = HttpContext.Request.Params["type"];           //图片：1， 视频：2

            string rootPath = "I:\\东航数字资源文件";
            if (!Directory.Exists(rootPath))
            {
                HttpContext.Response.Write("");
                return;
            }

            //"http://10.63.15.136"

            string[] digitalContentList = Directory.GetFiles(Path.Combine(rootPath, category));

            foreach (string fileName in digitalContentList)
            {
                DigitalContentEntity digitalContentEntity = new DigitalContentEntity()
                {
                    Category = category,
                    Type = GetFileType(Path.GetExtension(fileName)),
                    Name = Path.GetFileNameWithoutExtension(fileName),
                    Path = "http://localhost/CEA_EDU/DigitalContent/GetByPath?path=" + Server.UrlEncode(fileName.Replace(Path.Combine(rootPath), ""))   
                };

                list.Add(digitalContentEntity);
            }

            int total = list.Count;

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                list = list.Where(r => r.Name.Contains(searchKey)).ToList();
            }
            
            if (sort == "Type")
            {
                list = list.OrderBy(r => r.Type).Skip(offset).Take(pageSize).ToList();
                if (order == "desc")
                {
                    list = list.OrderByDescending(r => r.Type).Skip(offset).Take(pageSize).ToList();
                }
            }
            else
            {
                list = list.OrderBy(r => r.Name).Skip(offset).Take(pageSize).ToList();
                if (order == "desc")
                {
                    list = list.OrderByDescending(r => r.Name).Skip(offset).Take(pageSize).ToList();
                }
            }

            //给分页实体赋值  
            PageModels<DigitalContentEntity> model = new PageModels<DigitalContentEntity>();
            model.total = total;
            if (total % pageSize == 0)
                model.page = total / pageSize;
            else
                model.page = (total / pageSize) + 1;

            model.rows = list;

            //将查询结果返回  
            HttpContext.Response.Write(jss.Serialize(model));
        }

        public void GetByPath(string path)
        {
            string rootPath = "I:\\东航数字资源文件";
            path = Path.Combine(rootPath, Server.UrlDecode(path.Trim('\\')));
            if (System.IO.File.Exists(path))
            {
                FileInfo fi = new FileInfo(path);
                string fileName = Path.GetFileName(path);

                Response.Clear();
                Response.AddHeader("Content-Length", fi.Length.ToString());
                Response.ContentType = GetFileContentType(fileName);

                if (Request.UserAgent.ToLower().IndexOf("msie") > -1)
                {
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlPathEncode(fileName));
                }
                else if (Request.UserAgent.ToLower().IndexOf("firefox") > -1)
                {
                    Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                }
                else
                {
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                }


                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] buffer = new byte[10240];
                int n = fs.Read(buffer, 0, buffer.Length);
                while (n > 0)
                {
                    Response.OutputStream.Write(buffer, 0, n);
                    n = fs.Read(buffer, 0, buffer.Length);
                }

                fs.Close();
                Response.End();
            }
        }

        private static int GetFileType(string fileExtName)
        {
            if(PictureExtName.Contains(fileExtName.ToLower()))
            {
                return 1;
            }

            if(VideoExtName.Contains(fileExtName.ToLower()))
            {
                return 2;
            }

            return 0;
        }

        private string GetFileContentType(string fileName)
        {
            string DEFAULT_CONTENT_TYPE = "application/unknown";
            Microsoft.Win32.RegistryKey regkey, fileextkey;
            string FileContentType;
            try
            {
                string fileExtName = fileName.Substring(fileName.LastIndexOf('.'));
                regkey = Microsoft.Win32.Registry.ClassesRoot;
                fileextkey = regkey.OpenSubKey(fileExtName);
                FileContentType = fileextkey.GetValue("Content Type", DEFAULT_CONTENT_TYPE).ToString();
            }
            catch
            {
                FileContentType = DEFAULT_CONTENT_TYPE;
            }
            return FileContentType;
        }
    }
}
