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
    public class StudentClassHistoryController : ApiController
    {
        public string GetStudentClassHistoryByID(int id)
        {
            StudentClassHistoryManager manager = new StudentClassHistoryManager();
            return new JavaScriptSerializer().Serialize(manager.GetStudentClassHistoryByID(id));
        }

        public string GetStudentClassHistoryByStudentID(int studentID)
        {
            StudentClassHistoryManager manager = new StudentClassHistoryManager();
            return new JavaScriptSerializer().Serialize(manager.GetStudentClassHistoryByStudentID(studentID));
        }

        public string GetAll(string order, string sort, string searchKey, int offset, int pageSize)
        {
            int total = 0;
            StudentClassHistoryManager manager = new StudentClassHistoryManager();
            List<StudentClassHistoryEntity> list = manager.GetSearch(sort, order, offset, pageSize, out total);

            //给分页实体赋值  
            PageModels<StudentClassHistoryEntity> model = new PageModels<StudentClassHistoryEntity>();
            model.total = total;
            if (total % pageSize == 0)
                model.page = total / pageSize;
            else
                model.page = (total / pageSize) + 1;

            model.rows = list;

            //将查询结果返回  
            return new JavaScriptSerializer().Serialize(model);
        }

        public string PostStudentClassHistory(StudentClassHistoryEntity entity)
        {
            try
            {
                if (entity == null || entity.ArrangeClassID <= 0 || entity.StudentID <= 0)
                {
                    return "error";
                }

                StudentClassHistoryManager manager = new StudentClassHistoryManager();

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

        public string PutStudentClassHistory(StudentClassHistoryEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return "error";
                }

                StudentClassHistoryManager manager = new StudentClassHistoryManager();

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

        public string DeleteStudentClassHistory(int id)
        {
            try
            {
                StudentClassHistoryManager manager = new StudentClassHistoryManager();

                StudentClassHistoryEntity entity = manager.GetStudentClassHistoryByID(id);
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
