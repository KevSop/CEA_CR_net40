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
using System.Collections;

namespace Framework
{
    /// <summary>
    /// UserCombox.xaml 的交互逻辑
    /// </summary>
    public partial class UserCombox : UserControl
    {
        public UserCombox()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// 文本框内容
        /// </summary>
        public string UserLabelText
        {
            get { return lbl.Content.ToString(); }
            set { lbl.Content = value; }
        }
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(IEnumerable<object>), typeof(UserCombox), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(UserCombox), new FrameworkPropertyMetadata(null));
        public static readonly RoutedEvent ComboxChangedEvent = EventManager.RegisterRoutedEvent(
       "SelectionChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserCombox));
        public event RoutedEventHandler SelectionChanged
        {
            add { AddHandler(ComboxChangedEvent, value); }
            remove { RemoveHandler(ComboxChangedEvent, value); }
        }
        void RaiseTapEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(UserCombox.ComboxChangedEvent);
            RaiseEvent(newEventArgs);
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
            //SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(232, 116, 57));
            //b1.BorderBrush = brush;
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
        }

        private void cbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RaiseTapEvent();
        }

        
	}
}
