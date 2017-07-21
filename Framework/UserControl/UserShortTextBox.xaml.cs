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
	/// UserTextBox.xaml 的交互逻辑
	/// </summary>
	public partial class UserShortTextBox : UserControl
	{
        public UserShortTextBox()
		{
			this.InitializeComponent();
		}
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(UserShortTextBox), new FrameworkPropertyMetadata("Null"));
        public static readonly DependencyProperty ReadOnlyProperty = DependencyProperty.Register("ReadOnly", typeof(bool), typeof(UserShortTextBox), new FrameworkPropertyMetadata(false));
        
        /// <summary>
        /// 获得焦点时边框的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbox_GotFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(232,116,57));
            b1.BorderBrush = brush;
        }

        /// <summary>
        /// 失去焦点时边框的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbox_LostFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(0xCA, 0xC3, 0xBA));
            b1.BorderBrush = brush;
        }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string UserLblText
        {
            get { return lbl.Content.ToString(); }
            set { lbl.Content = value; }
        }

        /// <summary>
        /// 文本框内容
        /// </summary>
        public string Text
        {
            get { return GetValue(TextProperty).ToString(); }
            set { SetValue(TextProperty, value); }
        }
        public bool ReadOnly
        {
            get { return (bool)GetValue(ReadOnlyProperty); }
            set { SetValue(ReadOnlyProperty, value); }
        }
        public string UserText
        {
            get { return txtbox.Text; }
            set { txtbox.Text = value; }
        }

      
	}
}