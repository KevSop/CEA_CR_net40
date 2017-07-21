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
    public class SchoolInfoController : ApiController
    {
        public string GetSchoolInfoByID(int id)
        {
            SchoolInfoManager manager = new SchoolInfoManager();
            return new JavaScriptSerializer().Serialize(manager.GetSchoolInfoByID(id));
        }

        public string GetSchoolInfoByCode(string code)
        {
            SchoolInfoManager manager = new SchoolInfoManager();
            return new JavaScriptSerializer().Serialize(manager.GetSchoolInfoByCode(code));
        }

        public string GetSchoolInfoByName(string name)
        {
            SchoolInfoManager manager = new SchoolInfoManager();
            return new JavaScriptSerializer().Serialize(manager.GetSchoolInfoByName(name));
        }

        public string GetAll(string order, string sort, string searchKey, int offset, int pageSize)
        {
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
            return new JavaScriptSerializer().Serialize(model);
        }

        public string PostSchoolInfo(SchoolInfoEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return "error";
                }

                SchoolInfoManager manager = new SchoolInfoManager();

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

        public string PutSchoolInfo(SchoolInfoEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return "error";
                }

                SchoolInfoManager manager = new SchoolInfoManager();

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

        public string DeleteSchoolInfo(int id)
        {
            try
            {
                SchoolInfoManager manager = new SchoolInfoManager();

                SchoolInfoEntity entity = manager.GetSchoolInfoByID(id);
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
