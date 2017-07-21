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
using CEA_EDU.Common.Extend;
using CEA_EDU.Domain.Entity.ViewEntity;

namespace CEA_EDU.Web.Controllers
{
    public class MappingInfoController : BaseController
    {
        //班级学生信息
        public ActionResult ClassStudentMapIndex()
        {
            return View();
        }

        public void GetClassStudentMaps()
        {
            //用于序列化实体类的对象  
            JavaScriptSerializer jss = new JavaScriptSerializer();

            //请求中携带的条件  
            string order = HttpContext.Request.Params["order"];
            string searchKey = HttpContext.Request.Params["search"];
            int offset = Convert.ToInt32(HttpContext.Request.Params["offset"]);
            int pageSize = Convert.ToInt32(HttpContext.Request.Params["limit"]);

            string classID = HttpContext.Request.Params["classID"];

            int total = 0;
            ClassStudentMapManager manager = new ClassStudentMapManager();
            List<ClassStudentMapViewEntity> list = manager.GetSearch(Ext.ToIntOrNull(classID), searchKey, order, offset, pageSize, out total);

            //给分页实体赋值  
            PageModels<ClassStudentMapViewEntity> model = new PageModels<ClassStudentMapViewEntity>();
            model.total = total;
            if (total % pageSize == 0)
                model.page = total / pageSize;
            else
                model.page = (total / pageSize) + 1;

            model.rows = list;

            //将查询结果返回  
            HttpContext.Response.Write(jss.Serialize(model));
        }

        //排课信息
        public ActionResult ArrangeClassIndex()
        {
            ViewBag.Teachers = new UserInfoManager().GetUserByType("Teacher");

            ViewBag.Curriculums = new CurriculumInfoManager().GetAll();

            ViewBag.Classes = new ClassInfoManager().GetAll();

            ViewBag.ClassRooms = new ClassRoomInfoManager().GetAll();

            return View();
        }

        public void GetArrangeClasses()
        {
            //用于序列化实体类的对象  
            JavaScriptSerializer jss = new JavaScriptSerializer();

            //请求中携带的条件  
            string order = HttpContext.Request.Params["order"];
            int offset = Convert.ToInt32(HttpContext.Request.Params["offset"]);
            int pageSize = Convert.ToInt32(HttpContext.Request.Params["limit"]);

            string student = HttpContext.Request.Params["student"];
            string curriculumID = HttpContext.Request.Params["curriculumID"];
            string classID = HttpContext.Request.Params["classID"];
            string classRoomID = HttpContext.Request.Params["classRoomID"];
            string teacherID = HttpContext.Request.Params["teacherID"];
            string startTime = HttpContext.Request.Params["startTime"];

            DateTime? endTime = null;
            if (!string.IsNullOrWhiteSpace(startTime))
            {
                endTime = DateTime.Parse(startTime).AddDays(1);
            }

            List<int> classList = null;

            if (!string.IsNullOrWhiteSpace(student))
            {
                classList = new ClassStudentMapManager().GetClassStudentMapViewList(Ext.ToIntOrNull(classID), null, student).Select(r => r.ClassID).ToList();
            }

            int total = 0;
            ArrangeClassManager manager = new ArrangeClassManager();
            List<ArrangeClassViewEntity> list = manager.GetArrangeClassViewList(Ext.ToIntOrNull(curriculumID), Ext.ToIntOrNull(classID), Ext.ToIntOrNull(classRoomID), Ext.ToIntOrNull(teacherID),
                Ext.ToDateOrNull(startTime), Ext.ToDateOrNull(endTime));

            list = list.OrderByDescending(r => r.StartTime).OrderBy(r => r.ClassCode).ToList();

            total = list.Count;
            List<ArrangeClassViewEntity> listView = list.Skip(offset).Take(pageSize).ToList();
            if (!string.IsNullOrWhiteSpace(student))
            {
                listView = list.Where(r => classList.Contains(r.ClassID)).Skip(offset).Take(pageSize).ToList();
            }

            //给分页实体赋值  
            PageModels<ArrangeClassViewEntity> model = new PageModels<ArrangeClassViewEntity>();
            model.total = total;
            if (total % pageSize == 0)
                model.page = total / pageSize;
            else
                model.page = (total / pageSize) + 1;

            model.rows = listView;

            //将查询结果返回  
            HttpContext.Response.Write(jss.Serialize(model));
        }

    }
}
