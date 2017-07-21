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
    public partial class UserShortMoneyTextBox : UserControl
    {
        public UserShortMoneyTextBox()
        {
            this.InitializeComponent();
         

        }

     

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(UserShortMoneyTextBox), new FrameworkPropertyMetadata("Null"));
        public static readonly DependencyProperty ReadOnlyProperty = DependencyProperty.Register("ReadOnly", typeof(bool), typeof(UserShortMoneyTextBox), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// 获得焦点时边框的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbox_GotFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(232, 116, 57));
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


        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; 
                return;
            }
        }
        private void txtbox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key ==Key.Space)
            {
                e.Handled = true;
                return;
            }
            bool IsContainsDot = this.txtbox.Text.Contains(".");

            if (this.txtbox.Text.Trim().Length == 0) 
            {
                if (e.Key == Key.Decimal)
                {
                    e.Handled = true;
                    return;
                }
            }

            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)||(e.Key==Key.Delete)||(e.Key==Key.Decimal))
            {
                e.Handled = false;
            }
            else 
            {
                e.Handled = true;
            }
            if (this.txtbox.Text.Trim().Length == 0 && (e.Key == Key.Decimal))//如果第一个数输入是小数点 
            {
                e.Handled = true;
            }
            if (IsContainsDot && (e.Key == Key.Decimal)) //如果输入了小数点，并且再次输入 
            {
                e.Handled = true;
            }
            try
            {
                if ((e.Key != Key.Delete) && (e.Key != Key.Decimal))
                {
                    if (this.txtbox.Text.Trim().Substring(0, 1) == "0" && this.txtbox.Text.Trim().Length == 1)
                    {//检测第一个如果为0,接下来必须跟小数点 
                        if (e.Key != Key.Decimal)
                        {
                            e.Handled = true;
                        }
                    }

                }

                if (IsContainsDot) 
                {
                    if (this.txtbox.Text.Trim().Split('.')[1].Length > 1) 
                    {
                        e.Handled = true;
                    }
                }


            }
            catch
            { }
        }

        private void txtbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
          
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }


    }
}