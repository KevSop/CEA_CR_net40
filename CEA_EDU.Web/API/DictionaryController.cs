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
    public class DictionaryController : ApiController
    {
        public string GetDicByID(int id)
        {
            SysDicManager manager = new SysDicManager();
            return new JavaScriptSerializer().Serialize(manager.GetDicByID(id));
        }

        public string GetDicByCode(string code)
        {
            SysDicManager manager = new SysDicManager();
            return new JavaScriptSerializer().Serialize(manager.GetDicByCode(code));
        }

        public string GetDicByName(string name)
        {
            SysDicManager manager = new SysDicManager();
            return new JavaScriptSerializer().Serialize(manager.GetDicByName(name));
        }

        public string GetAllDics(string order, string sort, string searchKey, int offset, int pageSize)
        {
            int total = 0;
            SysDicManager manager = new SysDicManager();
            List<SysDicEntity> list = manager.GetSearch(searchKey, sort, order, offset, pageSize, out total);

            //给分页实体赋值  
            PageModels<SysDicEntity> model = new PageModels<SysDicEntity>();
            model.total = total;
            if (total % pageSize == 0)
                model.page = total / pageSize;
            else
                model.page = (total / pageSize) + 1;

            model.rows = list;

            //将查询结果返回  
            return new JavaScriptSerializer().Serialize(model);
        }

        public string PostDic(SysDicEntity entity)
        {
            try
            {
                //DictionaryViewModel model = JsonConvert.DeserializeObject<DictionaryViewModel>(jsonString.ToString());

                if (entity == null)
                {
                    return "error";
                }

                SysDicManager manager = new SysDicManager();

                entity.IsDisplay = "T";
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

        public string PutDic(SysDicEntity entity)
        {
            try
            {
                //DictionaryViewModel model = JsonConvert.DeserializeObject<DictionaryViewModel>(jsonString.ToString());

                if (entity == null)
                {
                    return "error";
                }

                SysDicManager manager = new SysDicManager();

                entity.IsDisplay = "T";
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

        public string DeleteDic(int id)
        {
            try
            {
                //DictionaryViewModel model = JsonConvert.DeserializeObject<DictionaryViewModel>(jsonString.ToString());

                SysDicManager manager = new SysDicManager();

                SysDicEntity entity = manager.GetDicByID(id);
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
