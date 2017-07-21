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
	/// UserComTextBox.xaml 的交互逻辑
	/// </summary>
	public partial class UserComTextBox : UserControl
	{
		public UserComTextBox()
		{
			this.InitializeComponent();
            //this.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() => { Keyboard.Focus(txt); }));
		}

        public static readonly DependencyProperty ItemSourceProperty =
           DependencyProperty.Register("ItemSource", typeof(IEnumerable<object>), typeof(UserComTextBox), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(UserComTextBox), new FrameworkPropertyMetadata(null));
        public static readonly RoutedEvent ComboxChangedEvent = EventManager.RegisterRoutedEvent(
       "SelectionChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserComTextBox));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(UserComTextBox), new FrameworkPropertyMetadata(null));

        //文本框属性
        public string Text
        {
            get { return (string)(GetValue(TextProperty)); }
            set { SetValue(TextProperty, value); }
        }
        
        public event RoutedEventHandler SelectionChanged
        {
            add { AddHandler(ComboxChangedEvent, value); }
            remove { RemoveHandler(ComboxChangedEvent, value); }
        }
        void RaiseTapEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(UserCombox.ComboxChangedEvent);
            RaiseEvent(newEventArgs);
            //Keyboard.Focus(txt);
        }


        /// <summary>
        /// 数据源
        /// </summary>
        public IEnumerable<object> ItemSource
        {
            get { return (IEnumerable<object>)(GetValue(ItemSourceProperty)); }
            set { SetValue(ItemSourceProperty, value); }
        }
        public string DisplayMemberPath
        {
            get { return cbo.DisplayMemberPath; }
            set { cbo.DisplayMemberPath = value; }
        }
        public string SelectedValuePath
        {
            get { return cbo.SelectedValuePath; }
            set { cbo.SelectedValuePath = value; }
        }
        public object SelectedValue
        {
            get { return GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }
        private void ComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //Keyboard.Focus(txt);
        }

        private void ComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(119, 107, 95));
            //b1.BorderBrush = brush;
        }

        private void cbo_DropDownOpened(object sender, EventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(232, 116, 57));
            b1.BorderBrush = brush;
        }

        private void cbo_DropDownClosed(object sender, EventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(0xCA, 0xC3, 0xBA));
            b1.BorderBrush = brush;
            Keyboard.Focus(txt);
        }

        private void cbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RaiseTapEvent();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(0xCA, 0xC3, 0xBA));
            b1.BorderBrush = brush;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(232, 116, 57));
            b1.BorderBrush = brush;
        }

        /// <summary>
        /// 文本框获得焦点
        /// </summary>
        public void SetTextFocus()
        {
            Keyboard.Focus(txt);
        }

 
	}


}