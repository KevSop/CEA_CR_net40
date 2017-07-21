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
    public class StaticInfoController : BaseController
    {
        //公司信息
        public ActionResult CompanyIndex()
        {
            return View();
        }

        public void GetCompanys()
        {
            //用于序列化实体类的对象  
            JavaScriptSerializer jss = new JavaScriptSerializer();

            //请求中携带的条件  
            string order = HttpContext.Request.Params["order"];
            string sort = HttpContext.Request.Params["sort"];
            string searchKey = HttpContext.Request.Params["search"];
            int offset = Convert.ToInt32(HttpContext.Request.Params["offset"]);
            int pageSize = Convert.ToInt32(HttpContext.Request.Params["limit"]);

            int total = 0;
            CompanyInfoManager manager = new CompanyInfoManager();
            List<CompanyInfoEntity> list = manager.GetSearch(searchKey, sort, order, offset, pageSize, out total);

            //给分页实体赋值  
            PageModels<CompanyInfoEntity> model = new PageModels<CompanyInfoEntity>();
            model.total = total;
            if (total % pageSize == 0)
                model.page = total / pageSize;
            else
                model.page = (total / pageSize) + 1;

            model.rows = list;

            //将查询结果返回  
            HttpContext.Response.Write(jss.Serialize(model));
        }

        public string SaveCompany(string jsonString, string action)
        {
            try
            {
                CompanyInfoEntity entity = JsonConvert.DeserializeObject<CompanyInfoEntity>(jsonString);
                CompanyInfoManager manager = new CompanyInfoManager();
                if (action == "add")
                {
                    manager.Insert(entity);
                }
                else
                {
                    CompanyInfoEntity oldEntity = manager.GetCompanyInfoByCode(entity.Code);
                    oldEntity.Name = entity.Name;
                    oldEntity.Address = entity.Address;
                    oldEntity.Description = entity.Description;

                    oldEntity.UpdateBy = SessionHelper.CurrentUser.Code;

                    manager.Update(oldEntity);
                }
                return "success";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        //校园信息
        public ActionResult SchoolIndex()
        {
            return View();
        }

        public void GetSchools()
        {
            //用于序列化实体类的对象  
            JavaScriptSerializer jss = new JavaScriptSerializer();

            //请求中携带的条件  
            string order = HttpContext.Request.Params["order"];
            string sort = HttpContext.Request.Params["sort"];
            string searchKey = HttpContext.Request.Params["search"];
            int offset = Convert.ToInt32(HttpContext.Request.Params["offset"]);
            int pageSize = Convert.ToInt32(HttpContext.Request.Params["limit"]);

            int total = 0;
            SchoolInfoManager manager = new SchoolInfoManager();
            List<SchoolInfoEntity> list = manager.GetSearch(searchKey, sort, order, offset, pageSize, out total);

            //给分页实体赋值  
            PageModels<SchoolInfoEntity> model = new PageModels<SchoolInfoEntity>();
            model.total = total;
            if (total % pageSize == 0)
                model.page = total / pageSize;
            else
                model.page = (total / pageSize) + 1;

            model.rows = list;

            //将查询结果返回  
            HttpContext.Response.Write(jss.Serialize(model));
        }

        public string SaveSchool(string jsonString, string action)
        {
            try
            {
                SchoolInfoEntity entity = JsonConvert.DeserializeObject<SchoolInfoEntity>(jsonString);
                SchoolInfoManager manager = new SchoolInfoManager();
                if (action == "add")
                {
                    manager.Insert(entity);
                }
                else
                {
                    SchoolInfoEntity oldEntity = manager.GetSchoolInfoByCode(entity.Code);
                    oldEntity.Name = entity.Name;
                    oldEntity.Address = entity.Address;
                    oldEntity.Description = entity.Description;

                    oldEntity.UpdateBy = SessionHelper.CurrentUser.Code;

                    manager.Update(oldEntity);
                }
                return "success";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}
