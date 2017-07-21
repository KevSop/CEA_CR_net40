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
    public class ClassRoomInfoController : ApiController
    {
        public string GetClassRoomInfoByID(int id)
        {
            ClassRoomInfoManager manager = new ClassRoomInfoManager();
            return new JavaScriptSerializer().Serialize(manager.GetClassRoomInfoByID(id));
        }

        public string GetClassRoomInfoByCode(string code)
        {
            ClassRoomInfoManager manager = new ClassRoomInfoManager();
            return new JavaScriptSerializer().Serialize(manager.GetClassRoomInfoByCode(code));
        }

        public string GetClassRoomInfoByName(string name)
        {
            ClassRoomInfoManager manager = new ClassRoomInfoManager();
            return new JavaScriptSerializer().Serialize(manager.GetClassRoomInfoByName(name));
        }

        public string GetAll(string order, string sort, string searchKey, int offset, int pageSize)
        {
            int total = 0;
            ClassRoomInfoManager manager = new ClassRoomInfoManager();
            List<ClassRoomInfoEntity> list = manager.GetSearch(searchKey, sort, order, offset, pageSize, out total);

            //给分页实体赋值  
            PageModels<ClassRoomInfoEntity> model = new PageModels<ClassRoomInfoEntity>();
            model.total = total;
            if (total % pageSize == 0)
                model.page = total / pageSize;
            else
                model.page = (total / pageSize) + 1;

            model.rows = list;

            //将查询结果返回  
            return new JavaScriptSerializer().Serialize(model);
        }

        public string PostClassRoomInfo(ClassRoomInfoEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return "error";
                }

                ClassRoomInfoManager manager = new ClassRoomInfoManager();

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

        public string PutClassRoomInfo(ClassRoomInfoEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return "error";
                }

                ClassRoomInfoManager manager = new ClassRoomInfoManager();

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

        public string DeleteClassRoomInfo(int id)
        {
            try
            {
                ClassRoomInfoManager manager = new ClassRoomInfoManager();

                ClassRoomInfoEntity entity = manager.GetClassRoomInfoByID(id);
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
