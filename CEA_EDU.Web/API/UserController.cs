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
    public class UserController : ApiController
    {
        public string GetUserByID(int id)
        {
            UserInfoManager manager = new UserInfoManager();
            return new JavaScriptSerializer().Serialize(manager.GetUserByID(id));
        }

        public string GetUserByCode(string code)
        {
            UserInfoManager manager = new UserInfoManager();
            return new JavaScriptSerializer().Serialize(manager.GetUserByCode(code));
        }

        public string GetUserByName(string name)
        {
            UserInfoManager manager = new UserInfoManager();
            return new JavaScriptSerializer().Serialize(manager.GetUserByName(name));
        }

        public string GetAll(string searchKey, string userType, string order, string sort, int offset, int pageSize)
        {
            int total = 0;
            UserInfoManager manager = new UserInfoManager();
            List<UserInfoEntity> list = manager.GetSearch(searchKey, userType, sort, order, offset, pageSize, out total);

            //给分页实体赋值  
            PageModels<UserInfoEntity> model = new PageModels<UserInfoEntity>();
            model.total = total;
            if (total % pageSize == 0)
                model.page = total / pageSize;
            else
                model.page = (total / pageSize) + 1;

            model.rows = list;

            //将查询结果返回  
            return new JavaScriptSerializer().Serialize(model);
        }

        public string PostUser(UserInfoEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return "error";
                }

                UserInfoManager manager = new UserInfoManager();

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

        public string PutUser(UserInfoEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return "error";
                }

                UserInfoManager manager = new UserInfoManager();

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

        public string DeleteUser(int id)
        {
            try
            {
                UserInfoManager manager = new UserInfoManager();

                UserInfoEntity entity = manager.GetUserByID(id);
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
