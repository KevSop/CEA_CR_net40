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
using CEA_EDU.Domain.Entity.ViewEntity;

namespace CEA_EDU.Web.API
{
    public class ArrangeClassController : ApiController
    {
        public string GetArrangeClassViewList(int? curriculumID, int? classID, int? classRoomID, int? teacherID, DateTime? startTime, DateTime? endTime)
        {
            ArrangeClassManager manager = new ArrangeClassManager();
            return new JavaScriptSerializer().Serialize(manager.GetArrangeClassViewList(curriculumID, classID, classRoomID, teacherID, startTime, endTime));
        }

        public string GetArrangeClassByID(int id)
        {
            ArrangeClassManager manager = new ArrangeClassManager();
            return new JavaScriptSerializer().Serialize(manager.GetArrangeClassByID(id));
        }

        public string GetArrangeClassByCurriculumID(int curriculumID)
        {
            ArrangeClassManager manager = new ArrangeClassManager();
            return new JavaScriptSerializer().Serialize(manager.GetArrangeClassByCurriculumID(curriculumID));
        }

        public string GetArrangeClassByClassID(int classID)
        {
            ArrangeClassManager manager = new ArrangeClassManager();
            return new JavaScriptSerializer().Serialize(manager.GetArrangeClassByClassID(classID));
        }

        public string GetArrangeClassByClassRoomID(int classRoomID)
        {
            ArrangeClassManager manager = new ArrangeClassManager();
            return new JavaScriptSerializer().Serialize(manager.GetArrangeClassByClassRoomID(classRoomID));
        }

        public string GetAll(int? curriculumID, int? classID, int? classRoomID, int? teacherID, DateTime? startTime, DateTime? endTime, int offset, int pageSize)
        {
            int total = 0;
            ArrangeClassManager manager = new ArrangeClassManager();
            List<ArrangeClassViewEntity> list = manager.GetArrangeClassViewList(curriculumID, classID, classRoomID, teacherID, startTime, endTime);

            total = list.Count;
            List<ArrangeClassViewEntity> listView = list.Skip(offset).Take(pageSize).ToList();

            //给分页实体赋值  
            PageModels<ArrangeClassViewEntity> model = new PageModels<ArrangeClassViewEntity>();
            model.total = total;
            if (total % pageSize == 0)
                model.page = total / pageSize;
            else
                model.page = (total / pageSize) + 1;

            model.rows = listView;

            //将查询结果返回  
            return new JavaScriptSerializer().Serialize(model);
        }

        public string PostArrangeClass(ArrangeClassEntity entity)
        {
            try
            {
                if (entity == null || entity.ClassID <= 0 || entity.ClassRoomID <= 0 || entity.CurriculumID <= 0 || entity.TeacherID <= 0)
                {
                    return "error";
                }

                ArrangeClassManager manager = new ArrangeClassManager();

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

        public string PutArrangeClass(ArrangeClassEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return "error";
                }

                ArrangeClassManager manager = new ArrangeClassManager();

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

        public string DeleteArrangeClass(int id)
        {
            try
            {
                ArrangeClassManager manager = new ArrangeClassManager();

                ArrangeClassEntity entity = manager.GetArrangeClassByID(id);
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
