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
using CEA_EDU.Domain;
using CEA_EDU.Domain.Entity;
using CEA_EDU.Domain.Manager;
using CEA_EDU.Web.Models;
using CEA_EDU.Web.Utils;
using Newtonsoft.Json;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace CEA_EDU.Web.API
{
    public class CompanyInfoController : ApiController
    {
        public string GetCompanyInfoByID(int id)
        {
            CompanyInfoManager manager = new CompanyInfoManager();
            return new JavaScriptSerializer().Serialize(manager.GetCompanyInfoByID(id));
        }

        public string GetCompanyInfoByCode(string code)
        {
            CompanyInfoManager manager = new CompanyInfoManager();
            return new JavaScriptSerializer().Serialize(manager.GetCompanyInfoByCode(code));
        }

        public string GetCompanyInfoByName(string name)
        {
            CompanyInfoManager manager = new CompanyInfoManager();
            return new JavaScriptSerializer().Serialize(manager.GetCompanyInfoByName(name));
        }

        public string GetAll(string order, string sort, string searchKey, int offset, int pageSize)
        {
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
            return new JavaScriptSerializer().Serialize(model);
        }

        public string PostCompanyInfo(CompanyInfoEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return "error";
                }

                CompanyInfoManager manager = new CompanyInfoManager();

                entity.CreateTime = DateTime.Now;
                entity.CreateTime = DateTime.Now;

                manager.Insert(entity);
              
                return "success";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string PutCompanyInfo(CompanyInfoEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return "error";
                }

                CompanyInfoManager manager = new CompanyInfoManager();

                entity.CreateTime = DateTime.Now;
                entity.CreateTime = DateTime.Now;

                manager.Update(entity);

                return "success";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string DeleteCompanyInfo(int id)
        {
            try
            {
                CompanyInfoManager manager = new CompanyInfoManager();

                CompanyInfoEntity entity = manager.GetCompanyInfoByID(id);
                if (entity != null)
                {
                    entity.Valid = "F";
                    entity.CreateTime = DateTime.Now;
                    entity.CreateTime = DateTime.Now;

                    manager.Update(entity);
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
