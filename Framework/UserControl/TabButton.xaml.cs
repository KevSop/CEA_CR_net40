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
//using Microsoft.Expression.Drawing;

namespace Framework
{
	/// <summary>
	/// TabButton.xaml 的交互逻辑
	/// </summary>
	public partial class TabButton : UserControl
	{
		public TabButton()
		{
			this.InitializeComponent();
		}

        /// <summary>
        /// 按钮点击事件
        /// </summary>
        public static readonly RoutedEvent ButtonClickEvent = EventManager.RegisterRoutedEvent(
       "ButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabButton));
        public event RoutedEventHandler ButtonClick
        {
            add { AddHandler(ButtonClickEvent, value); }
            remove { RemoveHandler(ButtonClickEvent, value); }
        }
        void RaiseTapEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(TabButton.ButtonClickEvent);
            RaiseEvent(newEventArgs);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            RaiseTapEvent();
        }

        /// <summary>
        /// 菜系名称
        /// </summary>
        public string DishesTypeName
        {
            get { return button.Content.ToString(); }
            set { button.Content = value; }
        }

        /// <summary>
        /// 菜系是否选中
        /// </summary>
        private bool _isCheck;

        /// <summary>
        /// 菜系是否选中
        /// </summary>
        public bool isCheck
        {
            get { return _isCheck; }

            set
            {
                _isCheck = value;

                if (_isCheck == false)
                {
                    button.Style = (Style)UserControl.Resources["TabButton"];
                    //SolidColorBrush brush = new SolidColorBrush
                    //button.Foreground = 
                }
                else
                {
                    button.Style = (Style)UserControl.Resources["TabButtonCheck"];
                    SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(0x67, 0x54, 0x46));
                    regularPolygon.Fill = brush;
                }

            }
        }
	}
}