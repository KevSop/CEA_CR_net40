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
	/// <summary>
	/// SeatButton.xaml 的交互逻辑
	/// </summary>
	public partial class SeatButton : UserControl
	{
		public SeatButton()
		{
			this.InitializeComponent();
		}

        /// <summary>
        /// 按钮点击事件
        /// </summary>
        public static readonly RoutedEvent ButtonClickEvent = EventManager.RegisterRoutedEvent(
       "ButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SeatButton));
        public event RoutedEventHandler ButtonClick
        {
            add { AddHandler(ButtonClickEvent, value); }
            remove { RemoveHandler(ButtonClickEvent, value); }
        }
        void RaiseTapEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(SeatButton.ButtonClickEvent);
            RaiseEvent(newEventArgs);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            RaiseTapEvent();
        }


        /// <summary>
        /// 台号
        /// </summary>
        public string SeatContent
        {
            set { button1.Content = value; }
            get { return button1.Content.ToString(); }
        }

        /// <summary>
        /// 台位状态
        /// </summary>
        private eSeatStatus _seatStatus;

        /// <summary>
        /// 台位状态
        /// </summary>
        public eSeatStatus SeatStatus
        {
            set 
            {
                _seatStatus = value;
                ShowSeatStatus();
            }
            get { return _seatStatus; }
        }


        /// <summary>
        /// 台位是否选中
        /// </summary>
        private bool _isCheck;

        /// <summary>
        /// 台位是否选中
        /// </summary>
        public bool isCheck
        {
            get { return _isCheck; }

            set
            {
                _isCheck = value;

                if (_isCheck == false)
                {
                    button1.Style = (Style)UserControl.Resources["SeatButtonStyle"];
                }
                else
                {
                    button1.Style = (Style)UserControl.Resources["SeatButtonStyleCheck"];
                }

            }
        }

        /// <summary>
        /// 台位的显示状态
        /// </summary>
        public void ShowSeatStatus()
        { 
            string strImagePath ;
            if (_seatStatus == eSeatStatus.Free)
            {
                _image.Source = null; 
            }

            else if (_seatStatus == eSeatStatus.Book)
            {
                strImagePath = "ordered-ico.png";
                BitmapImage image = new BitmapImage(new Uri(strImagePath, UriKind.Relative));
                _image.Source = image;
            }

            else if (_seatStatus == eSeatStatus.On)
            {
                strImagePath = "dinner-ico.png";
                BitmapImage image = new BitmapImage(new Uri(strImagePath, UriKind.Relative));
                _image.Source = image;
            }
        }



        #region 台位状态
        /// <summary>
        ///台位状态
        /// </summary>

        public enum eSeatStatus
        {
            /// <summary>
            /// 空闲
            /// </summary>
            Free =1,
            /// <summary>
            /// 预定
            /// </summary>
            Book=2,
            /// <summary>
            /// 开台
            /// </summary>
            On=3

        }

        #endregion


	}
}