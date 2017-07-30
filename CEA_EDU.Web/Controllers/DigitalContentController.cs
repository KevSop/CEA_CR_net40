﻿using System;
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
    [AllowAnonymous]
    public class DigitalContentController : BaseController
    {
        //用于序列化实体类的对象  
        JavaScriptSerializer jss = new JavaScriptSerializer();

        public static string WebHost = ConfigurationManager.AppSettings["WebHost"];
        public static string DigitalContentRootPath = ConfigurationManager.AppSettings["DigitalContentRootPath"];
        public static string ArticlePictureRootPath = ConfigurationManager.AppSettings["ArticlePictureRootPath"];

        public static List<string> PictureExtName = new List<string>(){".bmp", ".gif", ".jpg", ".jpeg", ".png"};
        public static List<string> VideoExtName = new List<string>() { ".avi", ".rmvb", ".rm", ".mpg", ".mpeg", ".wmv", ".mp4", ".mkv" };


        //公司介绍首页
        [AllowAnonymous]
        public ActionResult CompanyIndex()
        {
            return View();
        }

        //校园介绍首页
        [AllowAnonymous]
        public ActionResult SchoolIndex()
        {
            return View();
        }

        //公司校园数字资源信息
        [AllowAnonymous]
        public ActionResult DigitalContentIndex()
        {
            return View();
        }

        //公司介绍和公司愿景
        [AllowAnonymous]
        public void GetCompanyContent()
        {
            List<DigitalContentEntity> list = new List<DigitalContentEntity>();

            //请求中携带的条件  
            string type = HttpContext.Request.Params["type"];       //公司介绍：1， 公司愿景：2

            CompanyInfoEntity companyInfoEntity = null;
            if (type == "1")
            {
                companyInfoEntity = new CompanyInfoManager().GetCompanyInfoByCode("CEA");
            }
            else
            {
                companyInfoEntity = new CompanyInfoManager().GetCompanyInfoByCode("CEAFuture");
            }

            //将查询结果返回  
            HttpContext.Response.Write(jss.Serialize(companyInfoEntity.Description));
        }

        //校园介绍和校园规划
        [AllowAnonymous]
        public void GetSchoolContent()
        {
            List<DigitalContentEntity> list = new List<DigitalContentEntity>();

            //请求中携带的条件  
            string type = HttpContext.Request.Params["type"];       //校园介绍：1， 校园规划：2

            SchoolInfoEntity schoolInfoEntity = null;
            if (type == "1")
            {
                schoolInfoEntity = new SchoolInfoManager().GetSchoolInfoByCode("CEA");
            }
            else
            {
                schoolInfoEntity = new SchoolInfoManager().GetSchoolInfoByCode("CEAFuture");
            }

            //将查询结果返回  
            HttpContext.Response.Write(jss.Serialize(schoolInfoEntity.Description));
        }

        [AllowAnonymous]
        public void GetCompanyDigitalContent()
        {
            //用于序列化实体类的对象  
            JavaScriptSerializer jss = new JavaScriptSerializer();

            string type = HttpContext.Request.Params["type"];           //图片：1， 视频：2

            List<DigitalContentEntity> list = GetDigitalContentList("公司介绍", type);

            list = list.OrderBy(r => r.Name).ToList();

            //将查询结果返回  
            HttpContext.Response.Write(jss.Serialize(list));
        }


        [AllowAnonymous]
        public void GetSchoolDigitalContent()
        {
            //用于序列化实体类的对象  
            JavaScriptSerializer jss = new JavaScriptSerializer();

            string type = HttpContext.Request.Params["type"];           //图片：1， 视频：2

            List<DigitalContentEntity> list = GetDigitalContentList("校园介绍", type);

            list = list.OrderBy(r => r.Name).ToList();

            //将查询结果返回  
            HttpContext.Response.Write(jss.Serialize(list));
        }
        

        [AllowAnonymous]
        public void GetDigitalContents()
        {
            //用于序列化实体类的对象  
            JavaScriptSerializer jss = new JavaScriptSerializer();

            List<DigitalContentEntity> list = new List<DigitalContentEntity>();

            //请求中携带的条件  
            string order = HttpContext.Request.Params["order"];
            string sort = HttpContext.Request.Params["sort"];
            string searchName = HttpContext.Request.Params["search"];
            int offset = Convert.ToInt32(HttpContext.Request.Params["offset"]);
            int pageSize = Convert.ToInt32(HttpContext.Request.Params["limit"]);
            string category = HttpContext.Request.Params["category"];   //资源类别-共享文件夹2级目录（公司介绍、校园介绍）
            string type = HttpContext.Request.Params["type"];           //图片：1， 视频：2

            int total = 0;

            list = GetDigitalContentList(category, type, searchName, sort, order, offset, pageSize, out total);

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

        /// <summary>
        /// 获取数字资源文件
        /// </summary>
        /// <param name="category">资源类别-共享文件夹2级目录（公司介绍、校园介绍）</param>
        /// <param name="type">//图片：1， 视频：2</param>
        /// <returns></returns>
        [AllowAnonymous]
        public List<DigitalContentEntity> GetDigitalContentList(string category, string type)
        {
            List<DigitalContentEntity> list = new List<DigitalContentEntity>();

            string rootPath = DigitalContentRootPath;
            if (Directory.Exists(rootPath))
            {
                string[] digitalContentList = Directory.GetFiles(Path.Combine(rootPath, category));

                foreach (string fileName in digitalContentList)
                {
                    DigitalContentEntity digitalContentEntity = new DigitalContentEntity()
                    {
                        Category = category,
                        Type = GetFileType(Path.GetExtension(fileName)),
                        Name = Path.GetFileNameWithoutExtension(fileName),
                        Path = "http://" + WebHost + "/CEA_EDU/DigitalContent/GetByPath?path=" + Server.UrlEncode(fileName.Replace(Path.Combine(rootPath), ""))
                    };

                    if (type == digitalContentEntity.Type.ToString())
                    {
                        list.Add(digitalContentEntity);
                    }
                }
            }

            return list;

        }

        /// <summary>
        /// 获取数字资源文件
        /// </summary>
        /// <param name="category">资源类别-共享文件夹2级目录（公司介绍、校园介绍、图文信息）</param>
        /// <param name="type">//图片：1， 视频：2</param>
        /// <param name="searchName">名称搜索</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">正倒序</param>
        /// <param name="offset">起始点</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        [AllowAnonymous]
        public List<DigitalContentEntity> GetDigitalContentList(string category, string type, string searchName, string sort, string order, int offset, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<DigitalContentEntity> list = new List<DigitalContentEntity>();

            string rootPath = DigitalContentRootPath;
            if (Directory.Exists(rootPath))
            {
                string[] digitalContentList = Directory.GetFiles(Path.Combine(rootPath, category));

                foreach (string fileName in digitalContentList)
                {
                    DigitalContentEntity digitalContentEntity = new DigitalContentEntity()
                    {
                        Category = category,
                        Type = GetFileType(Path.GetExtension(fileName)),
                        Name = Path.GetFileNameWithoutExtension(fileName),
                        Path = "http://" + WebHost + "/CEA_EDU/DigitalContent/GetByPath?path=" + Server.UrlEncode(fileName.Replace(Path.Combine(rootPath), ""))
                    };

                    list.Add(digitalContentEntity);
                }

                if (!string.IsNullOrWhiteSpace(searchName))
                {
                    list = list.Where(r => r.Name.Contains(searchName)).ToList();
                }

                totalCount = list.Count;

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
            }

            return list;

        }

        /// <summary>
        /// 获取图片和视频资源
        /// </summary>
        /// <param name="path"></param>
        [AllowAnonymous]
        public void GetByPath(string path)
        {
            string rootPath = DigitalContentRootPath;
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


                string cacheKey = path.Replace(":", "").Replace("\\", "_");
                byte[] fileBytes = CacheUtility.GetCache(cacheKey) as byte[];

                int bufferLength = 10240;
                if (fileBytes == null || fileBytes.Length == 0)
                {
                    MemoryStream ms = new MemoryStream();

                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    byte[] buffer = new byte[10240];
                    int n = fs.Read(buffer, 0, buffer.Length);
                    while (n > 0)
                    {
                        Response.OutputStream.Write(buffer, 0, n);
                        ms.Write(buffer, 0, n);

                        n = fs.Read(buffer, 0, buffer.Length);
                    }
                    fs.Close();

                    //写入缓存
                    fileBytes = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(fileBytes, 0, (int)ms.Length);
                    ms.Close();
                    CacheUtility.SetCache(cacheKey, fileBytes, DateTime.Now.AddHours(10), TimeSpan.Zero);
                }
                else
                {
                    int bufferCount = fileBytes.Length / bufferLength + 1;
                    for (int i = 0; i < bufferCount; i++)
                    {
                        if (i == bufferCount - 1)
                        {
                            if (fileBytes.Length > i * bufferLength)
                            {
                                Response.OutputStream.Write(fileBytes, i * bufferLength, fileBytes.Length - i * bufferLength);
                            }
                        }
                        else
                        {
                            Response.OutputStream.Write(fileBytes, i * bufferLength, bufferLength);
                        }
                    }
                }

                Response.End();
            }
        }


        /// <summary>
        /// 获取图文信息图片资源
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public void GetArticlePictureByPath(string path)
        {
            string rootPath = ArticlePictureRootPath;
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

                //读取缓存
                string cacheKey = path.Replace(":", "").Replace("\\", "_");
                byte[] fileBytes = CacheUtility.GetCache(cacheKey) as byte[];

                int bufferLength = 10240;
                if (fileBytes == null || fileBytes.Length == 0)
                {
                    MemoryStream ms = new MemoryStream();

                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    byte[] buffer = new byte[bufferLength];
                    int n = fs.Read(buffer, 0, buffer.Length);
                    while (n > 0)
                    {
                        Response.OutputStream.Write(buffer, 0, n);
                        ms.Write(buffer, 0, n);

                        n = fs.Read(buffer, 0, buffer.Length);
                    }
                    fs.Close();

                    //写入缓存
                    fileBytes = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(fileBytes, 0, (int)ms.Length);
                    ms.Close();
                    CacheUtility.SetCache(cacheKey, fileBytes, DateTime.Now.AddHours(10), TimeSpan.Zero);
                }
                else
                {
                    int bufferCount = fileBytes.Length / bufferLength + 1;
                    for (int i = 0; i < bufferCount; i++)
                    {
                        if (i == bufferCount - 1)
                        {
                            if (fileBytes.Length > i * bufferLength)
                            {
                                Response.OutputStream.Write(fileBytes, i * bufferLength, fileBytes.Length - i * bufferLength);
                            }
                        }
                        else
                        {
                            Response.OutputStream.Write(fileBytes, i * bufferLength, bufferLength);
                        }
                    }
                }
               
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
