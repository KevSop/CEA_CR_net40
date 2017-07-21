using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Framework
{
	/// <summary>
	/// UserDataPicker.xaml 的交互逻辑
	/// </summary>
	public partial class UserLongDataPicker : UserControl
	{
        public UserLongDataPicker()
		{
			this.InitializeComponent();
		}

        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register("SelectedDate", typeof(DateTime), typeof(UserLongDataPicker), new FrameworkPropertyMetadata(DateTime.Now));


        public DateTime SelectedDate
        {
            get { return Convert.ToDateTime(GetValue(SelectedDateProperty)); }
            set { SetValue(SelectedDateProperty, value); }
        }

        public string UserLableText
        {
            get { return lbl.Content.ToString(); }
            set { lbl.Content = value; }
        }

        /// <summary>
        /// 获得焦点时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatePicker_GotFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(232, 116, 57));
            b1.BorderBrush = brush;
        }

        /// <summary>
        /// 失去焦点时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatePicker_LostFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(0xCA, 0xC3, 0xBA));
            b1.BorderBrush = brush;
        }


	}
}