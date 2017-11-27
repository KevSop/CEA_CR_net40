using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel;
using IBatisNet.DataMapper.Configuration;
using IBatisNet.DataMapper;
using System.Collections.ObjectModel;
using System.Collections;
using System.Xml.Linq;
using CEA_CR.PlatForm.Utils;

namespace CEA_CR.PlatForm.Models
{
    //原始库里的对象转换成前台要看的对象
    public class ClassInfoVModel : NotificationObject
    {
        public CourseScheduleByBJItem info { get; set; }
    }

    //页面对象
    public class ClassInfoPageModel : ObservableCollection<ClassInfoVModel>
    {
        private static object _threadLock = new Object();

        private static ClassInfoPageModel _classInfoPageModelInstance = null;
        public static ClassInfoPageModel classInfoPageModelInstance
        {
            get
            {
                lock (_threadLock)
                    if (_classInfoPageModelInstance == null)
                        _classInfoPageModelInstance = new ClassInfoPageModel();
                return _classInfoPageModelInstance;
            }
        }

        public ClassInfoPageModel()
        {
            List<ClassInfoVModel> list = new List<ClassInfoVModel>();
            //for (int i = 1; i < 101; i++)
            //{
            //    list.Add(new ClassRoomInfoVModel { info = new CurrentCourseResponse { classId = "班级编号" + i.ToString(), className = "班级名称" + i.ToString(), courseId = "课程编号" + i.ToString(), courseName = "课程名称" + i.ToString(), time = "开课时间" } });

            //}
            ResetData(list);
        }

        //这里注入所有数据
        public void ResetData(List<ClassInfoVModel> list)
        {
            ClearItems();
            foreach (var item in list)
            {
                Add(item);
            }
        }
    }
}
