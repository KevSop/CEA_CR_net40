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
	public partial class UserTelTextBox : UserControl
	{
		public UserTelTextBox()
		{
			this.InitializeComponent();
		}
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(UserTelTextBox), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty ReadOnlyProperty = DependencyProperty.Register("ReadOnly", typeof(bool), typeof(UserTelTextBox), new FrameworkPropertyMetadata(false));

        public static readonly RoutedEvent TextChangedEvent = EventManager.RegisterRoutedEvent(
       "TextChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserTelTextBox));
        public event RoutedEventHandler TextChanged
        {
            add { AddHandler(TextChangedEvent, value); }
            remove { RemoveHandler(TextChangedEvent, value); }
        }
        void RaiseTapEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(UserTelTextBox.TextChangedEvent);
            RaiseEvent(newEventArgs);
        }
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

        private void txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = sender as TextBox;           
            //屏蔽非法按键            
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Subtract || e.Key.ToString() == "Tab")
            {
                if (txt.Text.Contains("-") && e.Key == Key.Subtract)                
                {                    
                    e.Handled = true;                   
                 return;                
                }               
                e.Handled = false;           
            }
            else if (((e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.Subtract) && e.KeyboardDevice.Modifiers != ModifierKeys.Shift)            
            {
                if (txt.Text.Contains("-") && e.Key == Key.OemMinus)                
                {                    
                    e.Handled = true;                   
                    return;                
                }               
                e.Handled = false;            
            }            
            else            
            {                
                e.Handled = true;                
                if (e.Key.ToString() != "RightCtrl")               
                {                    
                   
                }           
            }
        }

        private void txtbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RaiseTapEvent();

            //屏蔽中文输入和非法字符粘贴输入
            TextBox textBox = sender as TextBox;
            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);

            int offset = change[0].Offset;
            if (change[0].AddedLength > 0)
            {
                double num = 0;
                if (!Double.TryParse(textBox.Text, out num))
                {
                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);
                }
            }

        }

      
	}
}