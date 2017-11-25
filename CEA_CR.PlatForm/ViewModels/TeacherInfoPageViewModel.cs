using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using CEA_CR.PlatForm.Models;
using System.Collections;
using IBatisNet.DataMapper.Configuration;
using IBatisNet.DataMapper;
using Microsoft.Win32;
using System.IO;
using System.Drawing.Printing;
using System.Drawing;
using TouchScreenKeyboard.Controls;
using System.Text.RegularExpressions;
using System.ComponentModel.Composition;
using System.Data;
using System.Threading;
using CEA_CR.PlatForm.Utils;

namespace CEA_CR.PlatForm.ViewModels
{
    public class TeacherInfoPageViewModel : NotificationObject
    {
        private Window window;
        private ListView lvMain;
        private Label lbPageInfo;
        private Label lbEmptyTip;


        private TeacherInfoPageModel _teacherInfoPageModel;
        public TeacherInfoPageModel teacherInfoPageModel
        {
            get
            {
                if (_teacherInfoPageModel == null)
                {
                    _teacherInfoPageModel = TeacherInfoPageModel.teacherInfoPageModelInstance;
                }
                return _teacherInfoPageModel;
            }
        }

        private TeacherInfoVModel _currentObject;
        public TeacherInfoVModel CurrentObject
        {
            get
            {
                if (_currentObject == null)
                {
                    _currentObject = new TeacherInfoVModel();
                    _currentObject.info = new CourseScheduleItem();
                    //_currentObject.tbTeacherInfo = new TeacherInfo();
                }
                return _currentObject;
            }
        }

        private List<TeacherInfoVModel> _displayList;
        public List<TeacherInfoVModel> DisplayList
        {
            get
            {
                _displayList = teacherInfoPageModel.Take(_pagesize).ToList();
                CanPageUpDisplay = _currentPage != 1;
                CanPageDownDisplay = _currentPage != _totalPage;
                return _displayList;
            }
        }


        private ICommand gotFocusCommand;
        public ICommand GotFocusCommand
        {
            get
            {
                if (gotFocusCommand == null)
                {
                    gotFocusCommand = new DelegateCommand<FloatingTouchScreenKeyboard>(Keyboard =>
                    {
                        Keyboard.IsShow = Visibility.Visible;
                    });
                }
                return gotFocusCommand;
            }
        }

        private ICommand lostFocusCommand;
        public ICommand LostFocusCommand
        {
            get
            {
                if (lostFocusCommand == null)
                {
                    lostFocusCommand = new DelegateCommand<FloatingTouchScreenKeyboard>(Keyboard =>
                    {
                        Keyboard.IsShow = Visibility.Hidden;
                    });
                }
                return lostFocusCommand;
            }
        }

        private bool _canPageUp;
        public bool CanPageUpDisplay
        {
            get { return _canPageUp; }
            set { _canPageUp = value; RaisePropertyChanged("CanPageUpDisplay"); }
        }

        private bool _canPageDown;
        public bool CanPageDownDisplay
        {
            get { return _canPageDown; }
            set { _canPageDown = value; RaisePropertyChanged("CanPageDownDisplay"); }
        }

        private int _currentPage = 1;
        private int _pagesize = 15;
        private int _totalPage
        {
            get
            {
                return teacherInfoPageModel.Count <= _pagesize ? 1 : (teacherInfoPageModel.Count / _pagesize + ((teacherInfoPageModel.Count % _pagesize) > 0 ? 1 : 0));
            }
        }
        private ICommand _pageUp;
        public ICommand PageUpCommand
        {
            get
            {
                if (_pageUp == null)
                {
                    _pageUp = new DelegateCommand<ListView>(lv =>
                    {
                        _currentPage = _currentPage > 1 ? _currentPage - 1 : 1;
                        CanPageUpDisplay = _currentPage != 1;
                        CanPageDownDisplay = _currentPage != _totalPage;
                        _displayList = teacherInfoPageModel.Skip((_currentPage - 1) * _pagesize).Take(_pagesize).ToList();
                        lv.ItemsSource = _displayList;
                        lbPageInfo.Content = string.Format("当前第{0}页，共{1}页", _currentPage, _totalPage);
                    });
                }
                return _pageUp;
            }
        }
        private ICommand _pageDown;
        public ICommand PageDownCommand
        {
            get
            {
                if (_pageDown == null)
                {
                    _pageDown = new DelegateCommand<ListView>(lv =>
                    {
                        _currentPage = _currentPage < _totalPage ? _currentPage + 1 : _totalPage;
                        CanPageUpDisplay = _currentPage != 1;
                        CanPageDownDisplay = _currentPage != _totalPage;
                        _displayList = teacherInfoPageModel.Skip((_currentPage - 1) * _pagesize).Take(_pagesize).ToList();
                        lv.ItemsSource = _displayList;
                        lbPageInfo.Content = string.Format("当前第{0}页，共{1}页", _currentPage, _totalPage);
                    });
                }
                return _pageDown;
            }
        }


        #region 窗体载入事件
        private ICommand _onLoaded;
        public ICommand OnLoadedCommand
        {
            get
            {
                if (_onLoaded == null)
                {
                    _onLoaded = new DelegateCommand<Window>(w =>
                    {
                        this.window = w;
                        lvMain = w.FindName("lvMain") as ListView;
                        lbPageInfo = w.FindName("lbPageInfo") as Label;
                        lbEmptyTip = w.FindName("lbEmptyTip") as Label;
                        Thread th1 = new Thread(delegate() { InitView(); });
                        th1.IsBackground = true; th1.Start();

                    });
                }
                return _onLoaded;
            }
        }
        private void InitView()
        {
            lvMain.Dispatcher.Invoke(
            new Action(
                 delegate
                 {
                     try
                     {
                         lvMain.ItemsSource = DisplayList;
                         SearchCommand.Execute(null);
                     }
                     catch (Exception ex)
                     {
                         Framework.MessageBox mb = new Framework.MessageBox();
                         mb.Topmost = true;
                         mb.Title = "异常提示";
                         mb.Message = ex.Message;
                         mb.ShowDialog();
                     }
                 }
            ));
            lbPageInfo.Dispatcher.Invoke(
       new Action(
            delegate
            {
                lbPageInfo.Content = string.Format("当前第{0}页，共{1}页", _currentPage, _totalPage);
            }
       ));

        }
        #endregion



        #region 搜索事件
        private ICommand searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new DelegateCommand(delegate()
                    {
                        Framework.SearchBoxTeacher sb = new Framework.SearchBoxTeacher();
                        sb.Topmost = true;
                        if (sb.ShowDialog().Value)
                        {
                            List<TeacherInfoVModel> searchResult = new List<TeacherInfoVModel>();
                            //searchResult.Add(new TeacherInfoVModel { ClassRoom = "阶梯教室", CourseName = "查询课程", StartTime = "2016-01-01 09:30", EndTime = "2016-01-01 11:30", tbTeacherInfo = new TeacherInfo { Name = sb.TeacherSearch } });
                            //此处调用查询接口查询结果
                            HttpDataService service = new HttpDataService();
                            List<CourseScheduleItem> currentCourse = service.GetCourseSchedule(sb.TeacherSearch, sb.StartValue.ToString("yyyy-MM"), "2");
                            if (currentCourse != null)
                            {
                                foreach (var item in currentCourse)
                                {
                                    searchResult.Add(new TeacherInfoVModel { info = item });
                                }
                            }
                            teacherInfoPageModel.ResetData(searchResult);
                            _currentPage = 1;
                            lvMain.ItemsSource = DisplayList;
                            lbPageInfo.Content = string.Format("当前第{0}页，共{1}页", _currentPage, _totalPage);

                            lbEmptyTip.Visibility = searchResult.Count == 0 ? Visibility.Visible : Visibility.Hidden;
                        }

                    });
                }
                return searchCommand;
            }
        }
        #endregion

        #region 退出事件
        private ICommand closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new DelegateCommand<Window>(w =>
                    {
                        teacherInfoPageModel.ResetData(new List<TeacherInfoVModel>());
                        _currentPage = 1;
                        lvMain.ItemsSource = DisplayList;
                        lbPageInfo.Content = "当前第1页，共1页";

                        w.Close();
                    });
                }
                return closeCommand;
            }
        }
        #endregion

        #region 选中行数据
        private ICommand _onSelectedIndexChange;
        public ICommand OnSelectedIndexChange
        {
            get
            {
                if (_onSelectedIndexChange == null)
                {
                    _onSelectedIndexChange = new DelegateCommand<ListView>(lv =>
                    {
                        TeacherInfoVModel code = lv.SelectedItem as TeacherInfoVModel;
                        if (code != null)
                        {
                            var t = code.info;
                            if (t != null)
                            {
                                CurrentObject.info.courseName = t.courseName;
                                Framework.MessageBox mb = new Framework.MessageBox();
                                mb.Title = "课程明细";
                                mb.Message = "班级名称：" + t.className + ", 课程名称：" + t.courseName + ", 开课时间：" + t.time + ", 开课地点：" + t.place + ", 教室编号：" + t.roomNo;
                                mb.Topmost = true;
                                mb.ShowDialog();
                            }
                        }
                    });
                }
                return _onSelectedIndexChange;
            }
        }
        #endregion

    }
}
