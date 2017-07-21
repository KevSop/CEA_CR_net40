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
    public class ClassRoomInfoVModel : NotificationObject
    {
        public CurrentCourseItem info { get; set; }
    }

    //页面对象
    public class ClassRoomInfoPageModel : ObservableCollection<ClassRoomInfoVModel>
    {
        private static object _threadLock = new Object();

        private static ClassRoomInfoPageModel _classRoomInfoPageModelInstance = null;
        public static ClassRoomInfoPageModel classRoomInfoPageModelInstance
        {
            get
            {
                lock (_threadLock)
                    if (_classRoomInfoPageModelInstance == null)
                        _classRoomInfoPageModelInstance = new ClassRoomInfoPageModel();
                return _classRoomInfoPageModelInstance;
            }
        }

        public ClassRoomInfoPageModel()
        {
            List<ClassRoomInfoVModel> list = new List<ClassRoomInfoVModel>();
            //此处查询所有 Mark todo
            //for (int i = 1; i < 101; i++)
            //{
            //    list.Add(new ClassRoomInfoVModel { info = new CurrentCourseResponse { classId = "班级编号" + i.ToString(), className = "班级名称" + i.ToString(), courseId = "课程编号" + i.ToString(), courseName = "课程名称" + i.ToString(), time = "开课时间" } });

            //}
            ResetData(list);
        }

        //这里注入所有数据
        public void ResetData(List<ClassRoomInfoVModel> list)
        {
            ClearItems();
            foreach (var item in list)
            {
                Add(item);
            }
        }
    }
}
