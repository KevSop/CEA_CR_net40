using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Reflection;

namespace Framework
{
  
    /// <summary>
    /// PasswordBox.xaml 的交互逻辑
    /// </summary>
    public partial class PasswordWidthBox : UserControl
    {
        public PasswordWidthBox()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty EnableProperty = DependencyProperty.Register("Enable", typeof(bool), typeof(PasswordWidthBox), new FrameworkPropertyMetadata(true));
        public static readonly RoutedEvent PasswordChangedEvent = EventManager.RegisterRoutedEvent(
        "PasswordChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PasswordWidthBox));
        public event RoutedEventHandler PasswordChanged
        {
            add { AddHandler(PasswordChangedEvent, value); }
            remove { RemoveHandler(PasswordChangedEvent, value); }
        }
        void RaiseTapEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(PasswordBox.PasswordChangedEvent);
            RaiseEvent(newEventArgs);
        }
        private void txtbox_GotFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(232, 116, 57));
            b1.BorderBrush = brush;
        }
      
       
        private void txtbox_LostFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(0xCA, 0xC3, 0xBA));
            b1.BorderBrush = brush;

        }
        /// <summary>
        /// 字段名
        /// </summary>
        public string Lable {
            get { return lbl.Content.ToString(); }
            set { lbl.Content = value; }
        }
        public string Password {
            get { return txtbox.Password; }
            set { txtbox.Password = value; }
        }
        public bool Enable
        {
            get { return (bool)GetValue(EnableProperty); }
            set { SetValue(EnableProperty, value); }
        }
        public int MaxLength {
            get { return txtbox.MaxLength; }
            set { txtbox.MaxLength = value; }
        }
        private void txtbox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            RaiseTapEvent();
            System.Windows.Controls.PasswordBox passwordtext = (System.Windows.Controls.PasswordBox)sender;

            SetPasswordBoxSelection(passwordtext, passwordtext.Password.Length + 1, passwordtext.Password.Length + 1);
        }

        private static void SetPasswordBoxSelection(System.Windows.Controls.PasswordBox passwordBox, int start, int length)
        {
            var select = passwordBox.GetType().GetMethod("Select",
                            BindingFlags.Instance | BindingFlags.NonPublic);

            select.Invoke(passwordBox, new object[] { start, length });
        }
    }
}
