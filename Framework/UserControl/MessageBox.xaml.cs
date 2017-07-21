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

namespace Framework
{
    /// <summary>
    /// MessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBox : Window
    {
        public MessageBox()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 消息框标题
        /// </summary>
        public string Title {
            get { return txtTitle.Text; }
            set { txtTitle.Text = value; }
        }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message {
            get { return txtMessage.Text; }
            set { txtMessage.Text = value; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
