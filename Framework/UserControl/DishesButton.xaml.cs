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
	/// DishesButton.xaml 的交互逻辑
	/// </summary>
	public partial class DishesButton : UserControl
	{
		public DishesButton()
		{
			this.InitializeComponent();
		}

        /// <summary>
        /// 图片属性
        /// </summary>
        //public static readonly DependencyProperty ImageSourceProperty=
        //    DependencyProperty.Register("ImageSource", typeof(BitmapSource), typeof(DishesButton), 
        //    new FrameworkPropertyMetadata(null));

        /// <summary>
        /// 按钮点击事件
        /// </summary>
        public static readonly RoutedEvent ButtonClickEvent = EventManager.RegisterRoutedEvent(
       "ButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DishesButton));

        public event RoutedEventHandler ButtonClick
        {
            add { AddHandler(ButtonClickEvent, value); }
            remove { RemoveHandler(ButtonClickEvent, value); }
        }

        void RaiseTapEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(DishesButton.ButtonClickEvent);
            RaiseEvent(newEventArgs);
        }
        
        /// <summary>
        /// 菜品图片ImageSource
        /// </summary>
        public ImageSource ImageSource
        {
            get
            {
                return image2.Source;
            }
            set
            {
               image2.Source=value;
            }
        }

        /// <summary>
        /// 菜品名称
        /// </summary>
        public string DishesName
        {
            get { return lbl.Content.ToString(); }
            set { lbl.Content = value; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RaiseTapEvent();
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
                    image1.Height = 0;
                    image1.Width = 0;
                    image1.Source = null;
                    //菜品名称的显示
                    SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(0xB2,0x77, 0x6B, 0x5F));
                    lbl.Background = brush;
                    button.BorderBrush = brush;
                    SolidColorBrush brush1 = new SolidColorBrush(Color.FromArgb(0x00,0x00, 0x00, 0x00));
                    b1.BorderBrush = brush1;
                }
                else
                {
                    Thickness margin = new Thickness(35, 20, 35, 20);
                    image1.Margin = margin;
                    image1.Height = 50;
                    image1.Width = 50;
                    string strImagePath = "icon-chick.png";
                    BitmapImage image = new BitmapImage(new Uri(strImagePath, UriKind.Relative));
                    image1.Source = image;
                    SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(0xF0, 0x71, 0x18));
                    lbl.Background = brush;
                    b1.BorderBrush = brush;
                }
            }
        }

        private void button_GotFocus(object sender, RoutedEventArgs e)
        {
            Thickness margin = new Thickness(35, 20, 35, 20);
            image1.Margin = margin;
            image1.Height = 50;
            image1.Width = 50;
            string strImagePath = "icon-chick.png";
            BitmapImage image = new BitmapImage(new Uri(strImagePath, UriKind.Relative));
            image1.Source = image;
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(0xF0, 0x71, 0x18));
            lbl.Background = brush;
            b1.BorderBrush = brush;
        }

        private void button_LostFocus(object sender, RoutedEventArgs e)
        {
            image1.Height = 0;
            image1.Width = 0;
            image1.Source = null;
            //菜品名称的显示
            SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(0xB2, 0x77, 0x6B, 0x5F));
            lbl.Background = brush;
            button.BorderBrush = brush;
            SolidColorBrush brush1 = new SolidColorBrush(Color.FromArgb(0x00, 0x00, 0x00, 0x00));
            b1.BorderBrush = brush1;
        }

	}
}