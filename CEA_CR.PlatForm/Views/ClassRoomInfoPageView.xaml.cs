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
using System.Windows.Shapes;
using CEA_CR.PlatForm.ViewModels;

namespace CEA_CR.PlatForm.Views
{
    /// <summary>
    /// Course.xaml 的交互逻辑
    /// </summary>
    public partial class ClassRoomInfoPageView : Window
    {
        public ClassRoomInfoPageView()
        {
            InitializeComponent();
            this.DataContext = new ClassRoomInfoPageViewModel();

            if (System.Configuration.ConfigurationManager.AppSettings["IsDev"] == "true")
            {
                this.Topmost = false;
            }
        }
    }
}
