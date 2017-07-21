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
    public class StudentInfoVModel : NotificationObject
    {
        public CourseScheduleItem info { get; set; }
    }

    //页面对象
    public class StudentInfoPageModel : ObservableCollection<StudentInfoVModel>
    {
        private static object _threadLock = new Object();

        private static StudentInfoPageModel _studentInfoPageModelInstance = null;
        public static StudentInfoPageModel studentInfoPageModelInstance
        {
            get
            {
                lock (_threadLock)
                    if (_studentInfoPageModelInstance == null)
                        _studentInfoPageModelInstance = new StudentInfoPageModel();
                return _studentInfoPageModelInstance;
            }
        }

        public StudentInfoPageModel()
        {
            List<StudentInfoVModel> list = new List<StudentInfoVModel>();
            //for (int i = 1; i < 101; i++)
            //{
            //    list.Add(new StudentInfoVModel { ClassRoom = "教室" + i.ToString(), StartTime = "2016-10-10 09:00", EndTime = "2016-10-10 11:00", CourseName="课程1",  TeacherName = "老师" + i.ToString(), tbStudentInfo = new StudentInfo { Name = "学生" + i.ToString() } });

            //}
            ResetData(list);
        }

        //这里注入所有数据
        public void ResetData(List<StudentInfoVModel> list)
        {
            ClearItems();
            foreach (var item in list)
            {
                Add(item);
            }
        }
    }
}
