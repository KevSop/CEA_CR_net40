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
    public class ClassStudentMapController : ApiController
    {
        public string GetClassStudentMapByID(int id)
        {
            ClassStudentMapManager manager = new ClassStudentMapManager();
            return new JavaScriptSerializer().Serialize(manager.GetClassStudentMapByID(id));
        }

        public string GetClassStudentMapByClassID(int classID)
        {
            ClassStudentMapManager manager = new ClassStudentMapManager();
            return new JavaScriptSerializer().Serialize(manager.GetClassStudentMapByClassID(classID));
        }

        public string GetClassStudentMapByStudentID(int studentID)
        {
            ClassStudentMapManager manager = new ClassStudentMapManager();
            return new JavaScriptSerializer().Serialize(manager.GetClassStudentMapByStudentID(studentID));
        }

        public string GetClassStudentMapViewList(int? classID, int? studentID)
        {
            ClassStudentMapManager manager = new ClassStudentMapManager();
            return new JavaScriptSerializer().Serialize(manager.GetClassStudentMapViewList(classID, studentID));
        }

        public string PostClassStudentMap(ClassStudentMapEntity entity)
        {
            try
            {
                if (entity == null || entity.StudentID <= 0)
                {
                    return "error";
                }

                ClassStudentMapManager manager = new ClassStudentMapManager();

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

        public string PutClassStudentMap(ClassStudentMapEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return "error";
                }

                ClassStudentMapManager manager = new ClassStudentMapManager();

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

        public string DeleteClassStudentMap(int id)
        {
            try
            {
                ClassStudentMapManager manager = new ClassStudentMapManager();

                ClassStudentMapEntity entity = manager.GetClassStudentMapByID(id);
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
