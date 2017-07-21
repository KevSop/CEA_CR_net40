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
using System.Reflection;
using System.Collections;

namespace Framework
{
	public partial class NoticeButton : UserControl
	{
        public NoticeButton()
		{
			this.InitializeComponent();
		}

        /// <summary>
        /// 按钮点击事件
        /// </summary>
        public static readonly RoutedEvent ButtonClickEvent = EventManager.RegisterRoutedEvent(
       "ButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NoticeButton));
        public event RoutedEventHandler ButtonClick
        {
            add { AddHandler(ButtonClickEvent, value); }
            remove { RemoveHandler(ButtonClickEvent, value); }
        }
        void RaiseTapEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(NoticeButton.ButtonClickEvent);
            RaiseEvent(newEventArgs);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            RaiseTapEvent();
        }

        /// <summary>
        /// 消息数量
        /// </summary>
        /// <returns></returns>
        public string NoticeNum
        {
            get { return lbl.Content.ToString(); }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    lbl.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    lbl.Content = value;
                }
            }
        }


        /// <summary>
        /// 台号
        /// </summary>
        public string Content
        {
            set { button1.Content = value; }
            get { return button1.Content.ToString(); }
        }

 
	}
}