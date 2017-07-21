﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CEA_CR.PlatForm.Views;

namespace CEA_CR.PlatForm
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //this.CoursePart.ImageSource = new BitmapImage(new Uri("book-icon.png", UriKind.Relative));  //先将图片右键生成操作设置成resource
            this.TeacherPart.ImageSource = new BitmapImage(new Uri("teachers.png", UriKind.Relative));
            this.RoomPart.ImageSource = new BitmapImage(new Uri("school.png", UriKind.Relative));

            this.StudentPart.ImageSource = new BitmapImage(new Uri("students.jpg", UriKind.Relative));
            this.CompusPart.ImageSource = new BitmapImage(new Uri("university.jpg", UriKind.Relative));
            this.CompanyPart.ImageSource = new BitmapImage(new Uri("company2.jpg", UriKind.Relative));

            //this.CoursePart.ButtonClick += CoursePart_ButtonClick;
            this.RoomPart.ButtonClick += RoomPart_ButtonClick;
            this.TeacherPart.ButtonClick += TeacherPart_ButtonClick;
            this.StudentPart.ButtonClick += StudentPart_ButtonClick;
            this.CompusPart.ButtonClick += CompusPart_ButtonClick;
            this.CompanyPart.ButtonClick += CompanyPart_ButtonClick;

            if (System.Configuration.ConfigurationManager.AppSettings["IsDev"] == "true")
            {
                this.Topmost = false;
            }
        }

        void CoursePart_ButtonClick(object sender, RoutedEventArgs e)
        {
            //原主界面不用管，新打开的在上层
            //ClassInfoPageView ci = new ClassInfoPageView();
            //ci.Show();
        }

        void RoomPart_ButtonClick(object sender, RoutedEventArgs e)
        {
            //原主界面不用管，新打开的在上层
            ClassRoomInfoPageView ci = new ClassRoomInfoPageView();
            ci.Show();
        }

        void TeacherPart_ButtonClick(object sender, RoutedEventArgs e)
        {
            //原主界面不用管，新打开的在上层
            TeacherInfoPageView ci = new TeacherInfoPageView();
            ci.Show();
        }
        void StudentPart_ButtonClick(object sender, RoutedEventArgs e)
        {
            //原主界面不用管，新打开的在上层
            StudentInfoPageView ci = new StudentInfoPageView();
            ci.Show();
        }
        void CompusPart_ButtonClick(object sender, RoutedEventArgs e)
        {
            //原主界面不用管，新打开的在上层
            CompusPageView ci = new CompusPageView();
            ci.Show();
        }
        void CompanyPart_ButtonClick(object sender, RoutedEventArgs e)
        {
            //原主界面不用管，新打开的在上层
            CompanyPageView ci = new CompanyPageView();
            ci.Show();
        }
    }
}
