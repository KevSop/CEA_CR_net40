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
	public partial class UserTimeTextBox : UserControl
	{
        public UserTimeTextBox()
		{
			this.InitializeComponent();
		}
        public static readonly DependencyProperty TextHourProperty = DependencyProperty.Register("TextHour", typeof(string), typeof(UserTimeTextBox), new FrameworkPropertyMetadata("小时"));
        public static readonly DependencyProperty TextMinProperty = DependencyProperty.Register("TextMin", typeof(string), typeof(UserTimeTextBox), new FrameworkPropertyMetadata("分钟"));
        public static readonly DependencyProperty ReadOnlyProperty = DependencyProperty.Register("ReadOnly", typeof(bool), typeof(UserTimeTextBox), new FrameworkPropertyMetadata(false));



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
        /// 小时框内容
        /// </summary>
        public string TextHour
        {
            get { return GetValue(TextHourProperty).ToString(); }
            set { SetValue(TextHourProperty, value); }
        }

        /// <summary>
        /// 分钟框内容
        /// </summary>
        public string TextMin
        {
            get { return GetValue(TextMinProperty).ToString(); }
            set { SetValue(TextMinProperty, value); }
        }

        public bool ReadOnly
        {
            get { return (bool)GetValue(ReadOnlyProperty); }
            set { SetValue(ReadOnlyProperty, value); }
        }

        public int HourMaxLength
        {
            get { return txtHour.MaxLength; }
            set { txtHour.MaxLength = value; }
        }

        public int MinMaxLength
        {
            get { return txtMin.MaxLength; }
            set { txtMin.MaxLength = value; }
        }

        
        private void textBox1_Pasting(object sender, DataObjectPastingEventArgs e)
        {

            if (e.DataObject.GetDataPresent(typeof(String)))
            {


                String text = (String)e.DataObject.GetData(typeof(String));
                if (!isNumberic(text))
                { 
                    e.CancelCommand(); 
                }

            }

            else { e.CancelCommand(); } 


        }



 
        private void textBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Space)

                e.Handled = true;
        }



        private void textBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            if (!isNumberic(e.Text))

            {

                e.Handled = true;

            }

            else
                e.Handled = false;

        }




        //isDigit是否是数字

        public static bool isNumberic(string _string)
        {

            if (string.IsNullOrEmpty(_string))


                return false;


            foreach (char c in _string)
            {

                if (!char.IsDigit(c))

                    return false;
            }


            return true;

        }

        /// <summary>
        /// 输入小时后，直接跳到分钟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHour_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtHour.Text.Length == 2)
            {
                txtMin.Focus();
                txtMin.SelectAll();
            }
        }

      
	}
}