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
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using CEA_EDU.Web.Models;
using CEA_EDU.Domain.Manager;
using CEA_EDU.Domain.Entity;
using CEA_EDU.Web.Utils;

namespace CEA_EDU.Web.Controllers
{
    public class BasicInfoController : BaseController
    {
        //班级信息
        public ActionResult ClassIndex()
        {
            ViewBag.Teachers = new UserInfoManager().GetUserByType("Teacher");

            return View();
        }

        public void GetClasses()
        {
            //用于序列化实体类的对象  
            JavaScriptSerializer jss = new JavaScriptSerializer();

            //请求中携带的条件  
            string order = HttpContext.Request.Params["order"];
            string sort = HttpContext.Request.Params["sort"];
            string searchKey = HttpContext.Request.Params["search"];
            int offset = Convert.ToInt32(HttpContext.Request.Params["offset"]);
            int pageSize = Convert.ToInt32(HttpContext.Request.Params["limit"]);

            int total = 0;
            ClassInfoManager manager = new ClassInfoManager();
            List<ClassInfoEntity> list = manager.GetSearch(searchKey, sort, order, offset, pageSize, out total);

            var teacherList = new UserInfoManager().GetUserByType("Teacher");

            List<ClassViewModel> listView = new List<ClassViewModel>();

            foreach (var item in list)
            {
                ClassViewModel viewModel = new ClassViewModel();

                viewModel.ClassID = item.ClassID;
                viewModel.Code = item.Code;
                viewModel.Name = item.Name;
                viewModel.Type = item.Type;
                viewModel.StartTime = item.StartTime;
                viewModel.EndTime = item.EndTime;
                viewModel.Company = item.Company;
                viewModel.Department = item.Department;
                viewModel.Remark = item.Remark;
                viewModel.UpdateTime = item.UpdateTime;

                var teacherEntity = teacherList.Where(r => r.ID == item.TeacherID).FirstOrDefault();
                if (teacherEntity != null)
                {
                    viewModel.TeacherID = teacherEntity.ID;
                    viewModel.TeacherCode = teacherEntity.Code;
                    viewModel.TeacherName = teacherEntity.Name;
                }

                listView.Add(viewModel);
            }

            //给分页实体赋值  
            PageModels<ClassViewModel> model = new PageModels<ClassViewModel>();
            model.total = total;
            if (total % pageSize == 0)
                model.page = total / pageSize;
            else
                model.page = (total / pageSize) + 1;

            model.rows = listView;

            //将查询结果返回  
            HttpContext.Response.Write(jss.Serialize(model));
        }

        public string SaveClass(string jsonString, string action)
        {
            try
            {
                ClassInfoEntity entity = JsonConvert.DeserializeObject<ClassInfoEntity>(jsonString);
                ClassInfoManager manager = new ClassInfoManager();
                if (action == "add")
                {
                    manager.Insert(entity);
                }
                else
                {
                    ClassInfoEntity oldEntity = manager.GetClassInfoByCode(entity.Code);
                    oldEntity.Name = entity.Name;
                    oldEntity.Type = entity.Type;
                    oldEntity.StartTime = entity.StartTime;
                    oldEntity.EndTime = entity.EndTime;
                    oldEntity.TeacherID = entity.TeacherID;

                    oldEntity.UpdateBy = SessionHelper.CurrentUser.Code;

                    manager.Update(oldEntity);
                }
                return "success";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        //教室信息
        public ActionResult ClassRoomIndex()
        {
            return View();
        }

        public void GetClassRooms()
        {
            //用于序列化实体类的对象  
            JavaScriptSerializer jss = new JavaScriptSerializer();

            //请求中携带的条件  
            string order = HttpContext.Request.Params["order"];
            string sort = HttpContext.Request.Params["sort"];
            string searchKey = HttpContext.Request.Params["search"];
            int offset = Convert.ToInt32(HttpContext.Request.Params["offset"]);
            int pageSize = Convert.ToInt32(HttpContext.Request.Params["limit"]);

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
            HttpContext.Response.Write(jss.Serialize(model));
        }

        public string SaveClassRoom(string jsonString, string action)
        {
            try
            {
                ClassRoomInfoEntity entity = JsonConvert.DeserializeObject<ClassRoomInfoEntity>(jsonString);
                ClassRoomInfoManager manager = new ClassRoomInfoManager();
                if (action == "add")
                {
                    manager.Insert(entity);
                }
                else
                {
                    ClassRoomInfoEntity oldEntity = manager.GetClassRoomInfoByCode(entity.Code);
                    oldEntity.Name = entity.Name;
                    oldEntity.Address = entity.Address;
                    oldEntity.Type = entity.Type;
                    oldEntity.SeatNum = entity.SeatNum;

                    oldEntity.UpdateBy = SessionHelper.CurrentUser.Code;

                    manager.Update(oldEntity);
                }
                return "success";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        //课程信息
        public ActionResult CurriculumIndex()
        {
            return View();
        }

        public void GetCurriculums()
        {
            //用于序列化实体类的对象  
            JavaScriptSerializer jss = new JavaScriptSerializer();

            //请求中携带的条件  
            string order = HttpContext.Request.Params["order"];
            string sort = HttpContext.Request.Params["sort"];
            string searchKey = HttpContext.Request.Params["search"];
            int offset = Convert.ToInt32(HttpContext.Request.Params["offset"]);
            int pageSize = Convert.ToInt32(HttpContext.Request.Params["limit"]);

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
            HttpContext.Response.Write(jss.Serialize(model));
        }

        public string SaveCurriculum(string jsonString, string action)
        {
            try
            {
                CurriculumInfoEntity entity = JsonConvert.DeserializeObject<CurriculumInfoEntity>(jsonString);
                CurriculumInfoManager manager = new CurriculumInfoManager();
                if (action == "add")
                {
                    manager.Insert(entity);
                }
                else
                {
                    CurriculumInfoEntity oldEntity = manager.GetCurriculumInfoByCode(entity.Code);
                    oldEntity.Name = entity.Name;
                    oldEntity.Type = entity.Type;

                    oldEntity.UpdateBy = SessionHelper.CurrentUser.Code;

                    manager.Update(oldEntity);
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
