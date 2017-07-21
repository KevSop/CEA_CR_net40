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
    public class CurriculumInfoController : ApiController
    {
        public string GetCurriculumInfoByID(int id)
        {
            CurriculumInfoManager manager = new CurriculumInfoManager();
            return new JavaScriptSerializer().Serialize(manager.GetCurriculumInfoByID(id));
        }

        public string GetCurriculumInfoByCode(string code)
        {
            CurriculumInfoManager manager = new CurriculumInfoManager();
            return new JavaScriptSerializer().Serialize(manager.GetCurriculumInfoByCode(code));
        }

        public string GetCurriculumInfoByName(string name)
        {
            CurriculumInfoManager manager = new CurriculumInfoManager();
            return new JavaScriptSerializer().Serialize(manager.GetCurriculumInfoByName(name));
        }

        public string GetAll(string order, string sort, string searchKey, int offset, int pageSize)
        {
            int total = 0;
            CurriculumInfoManager manager = new CurriculumInfoManager();
            List<CurriculumInfoEntity> list = manager.GetSearch(searchKey, sort, order, offset, pageSize, out total);

            //给分页实体赋值  
            PageModels<CurriculumInfoEntity> model = new PageModels<CurriculumInfoEntity>();
            model.total = total;
            if (total % pageSize == 0)
                model.page = total / pageSize;
            else
                model.page = (total / pageSize) + 1;

            model.rows = list;

            //将查询结果返回  
            return new JavaScriptSerializer().Serialize(model);
        }

        public string PostCurriculumInfo(CurriculumInfoEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return "error";
                }

                CurriculumInfoManager manager = new CurriculumInfoManager();

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

        public string PutCurriculumInfo(CurriculumInfoEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return "error";
                }

                CurriculumInfoManager manager = new CurriculumInfoManager();

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

        public string DeleteCurriculumInfo(int id)
        {
            try
            {
                CurriculumInfoManager manager = new CurriculumInfoManager();

                CurriculumInfoEntity entity = manager.GetCurriculumInfoByID(id);
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
