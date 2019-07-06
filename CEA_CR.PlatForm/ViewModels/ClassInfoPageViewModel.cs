﻿using System;
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
using Framework;

namespace CEA_CR.PlatForm.ViewModels
{
    public class ClassInfoPageViewModel : NotificationObject
    {
        private Window window;
        private ListView lvMain;
        private Label lbPageInfo;
        private Label lbEmptyTip;
        private Window SearchWindow;

        private ClassInfoPageModel _classInfoPageModel;
        public ClassInfoPageModel classInfoPageModel
        {
            get
            {
                if (_classInfoPageModel == null)
                {
                    _classInfoPageModel = ClassInfoPageModel.classInfoPageModelInstance;
                }
                return _classInfoPageModel;
            }
        }

        private ClassInfoVModel _currentObject;
        public ClassInfoVModel CurrentObject
        {
            get
            {
                if (_currentObject == null)
                {
                    _currentObject = new ClassInfoVModel();
                    _currentObject.info = new CourseScheduleByBJItem();
                }
                return _currentObject;
            }
        }

        private List<ClassInfoVModel> _displayList;
        public List<ClassInfoVModel> DisplayList
        {
            get
            {
                _displayList = classInfoPageModel.Take(_pagesize).ToList();
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
                return classInfoPageModel.Count <= _pagesize ? 1 : (classInfoPageModel.Count / _pagesize + ((classInfoPageModel.Count % _pagesize) > 0 ? 1 : 0));
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
                        _displayList = classInfoPageModel.Skip((_currentPage - 1) * _pagesize).Take(_pagesize).ToList();
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
                        _displayList = classInfoPageModel.Skip((_currentPage - 1) * _pagesize).Take(_pagesize).ToList();
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
                        if (this.SearchWindow == null)
                        {
                            //Framework.SearchBoxClassInfo sb = new Framework.SearchBoxClassInfo();
                            Framework.SearchBoxClassInfoNew sb = new Framework.SearchBoxClassInfoNew(getSearchResult);
                            sb.CloseWindowAction = new Action<Framework.SearchBoxClassInfoNew, bool>(SearchBoxCloseAction);
                            sb.Topmost = true;

                            this.SearchWindow = sb;

                            sb.Show();
                        }
                        else
                        {
                            this.SearchWindow.Topmost = true;
                            this.SearchWindow.Activate();
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
                        classInfoPageModel.ResetData(new List<ClassInfoVModel>());
                        _currentPage = 1;
                        lvMain.ItemsSource = DisplayList;
                        lbPageInfo.Content = "当前第1页，共1页";

                        if (this.SearchWindow != null)
                        {
                            this.SearchWindow.Close();
                        }

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
                        ClassInfoVModel code = lv.SelectedItem as ClassInfoVModel;
                        if (code != null)
                        {
                            //弹出课程介绍
                            var t = code.info;
                            if (t != null)
                            {
                                CurrentObject.info.CLASS_NAME = t.CLASS_NAME;
                                //CurrentObject.tbClassRoomInfo.Name = t.Name;
                                Framework.MessageBox mb = new Framework.MessageBox();
                                //mb.Title = CurrentObject.tbClassRoomInfo.Name;
                                mb.Title = "课程明细";
                                mb.Message = "班级名称：" + t.CLASS_NAME + ", 课程名称：" + t.COURSE_NAME + ", 开课时间：" + t.TIME + ", 开课地点：" + t.PLACE + ", 教室编号：" + t.ROOMNO;
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

        public List<AutoCompleteItem> getSearchResult(string searchKey)
        {
            List<AutoCompleteItem> result = new List<AutoCompleteItem>();

            HttpDataService service = new HttpDataService();
            List<ClassInfoItem> classInfoList = service.GetSearchClassInfoList(searchKey);

            if (classInfoList != null)
            {
                result = classInfoList.Select(r => new AutoCompleteItem() { Text = r.NAME, Value = r.ID }).ToList();
            }

            return result;
        }

        public void SearchBoxCloseAction(Framework.SearchBoxClassInfoNew searchBox, bool windowResult)
        {
            this.SearchWindow = null;

            if (windowResult)
            {
                List<ClassInfoVModel> searchResult = new List<ClassInfoVModel>();
                //此处过滤查询 Mark todo
                HttpDataService service = new HttpDataService();
                List<CourseScheduleByBJItem> currentCourse = service.GetCourseScheduleByClassInfo(searchBox.ClassSearchValue, searchBox.StartValue.ToString("yyyy-MM"));
                if (currentCourse != null)
                {
                    foreach (var item in currentCourse)
                    {
                        searchResult.Add(new ClassInfoVModel { info = item });
                    }
                }
                //此处调用查询接口查询结果

                classInfoPageModel.ResetData(searchResult);
                _currentPage = 1;
                lvMain.ItemsSource = DisplayList;
                lbPageInfo.Content = string.Format("当前第{0}页，共{1}页", _currentPage, _totalPage);

                lbEmptyTip.Visibility = searchResult.Count == 0 ? Visibility.Visible : Visibility.Hidden;
            }
        }

    }
}
