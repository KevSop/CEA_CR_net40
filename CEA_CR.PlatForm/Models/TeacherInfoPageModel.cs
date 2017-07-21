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
    public class TeacherInfoVModel : NotificationObject
    {
        public CourseScheduleItem info { get; set; }
    }

    //页面对象
    public class TeacherInfoPageModel : ObservableCollection<TeacherInfoVModel>
    {
        private static object _threadLock = new Object();

        private static TeacherInfoPageModel _teacherInfoPageModelInstance = null;
        public static TeacherInfoPageModel teacherInfoPageModelInstance
        {
            get
            {
                lock (_threadLock)
                    if (_teacherInfoPageModelInstance == null)
                        _teacherInfoPageModelInstance = new TeacherInfoPageModel();
                return _teacherInfoPageModelInstance;
            }
        }

        public TeacherInfoPageModel()
        {
            List<TeacherInfoVModel> list = new List<TeacherInfoVModel>();
            //for (int i = 1; i < 101; i++)
            //{
            //    list.Add(new TeacherInfoVModel { ClassRoom = "教室" + i.ToString(), StartTime = "2016-10-10 09:00", EndTime = "2016-10-10 11:00", CourseName = "课程" + i.ToString(), tbTeacherInfo = new TeacherInfo { Name = "老师" + i.ToString() } });

            //}
            ResetData(list);
        }

        //这里注入所有数据
        public void ResetData(List<TeacherInfoVModel> list)
        {
            ClearItems();
            foreach (var item in list)
            {
                Add(item);
            }
        }
    }
}
