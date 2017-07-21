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
    public class ClassInfoController : ApiController
    {
        public string GetClassInfoByID(int id)
        {
            ClassInfoManager manager = new ClassInfoManager();
            return new JavaScriptSerializer().Serialize(manager.GetClassInfoByID(id));
        }

        public string GetClassInfoByCode(string code)
        {
            ClassInfoManager manager = new ClassInfoManager();
            return new JavaScriptSerializer().Serialize(manager.GetClassInfoByCode(code));
        }

        public string GetClassInfoByName(string name)
        {
            ClassInfoManager manager = new ClassInfoManager();
            return new JavaScriptSerializer().Serialize(manager.GetClassInfoByName(name));
        }

        public string GetAll(string order, string sort, string searchKey, int offset, int pageSize)
        {
            int total = 0;
            ClassInfoManager manager = new ClassInfoManager();
            List<ClassInfoEntity> list = manager.GetSearch(searchKey, sort, order, offset, pageSize, out total);

            //给分页实体赋值  
            PageModels<ClassInfoEntity> model = new PageModels<ClassInfoEntity>();
            model.total = total;
            if (total % pageSize == 0)
                model.page = total / pageSize;
            else
                model.page = (total / pageSize) + 1;

            model.rows = list;

            //将查询结果返回  
            return new JavaScriptSerializer().Serialize(model);
        }

        public string PostClassInfo(ClassInfoEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return "error";
                }

                ClassInfoManager manager = new ClassInfoManager();

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

        public string PutClassInfo(ClassInfoEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return "error";
                }

                ClassInfoManager manager = new ClassInfoManager();

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

        public string DeleteClassInfo(int id)
        {
            try
            {
                ClassInfoManager manager = new ClassInfoManager();

                ClassInfoEntity entity = manager.GetClassInfoByID(id);
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
