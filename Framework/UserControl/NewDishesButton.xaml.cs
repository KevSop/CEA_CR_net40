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
	public partial class NewDishesButton : UserControl
	{
        public NewDishesButton()
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
       "ButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NewDishesButton));

        public event RoutedEventHandler ButtonClick
        {
            add { AddHandler(ButtonClickEvent, value); }
            remove { RemoveHandler(ButtonClickEvent, value); }
        }

        void RaiseTapEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(NewDishesButton.ButtonClickEvent);
            RaiseEvent(newEventArgs);
        }

        private ImageSource imageSource { get; set; }
        
        /// <summary>
        /// 图片ImageSource
        /// </summary>
        public ImageSource ImageSource
        {
            get
            {
                return imageSource;
            }
            set
            {
                imageSource = value;
                image2.Source = value;
            }
        }

        /// <summary>
        /// 图片SelectedImageSource(图片选中状态)
        /// </summary>
        public ImageSource SelectedImageSource{ get; set; }

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
                }
                else
                {
                    Thickness margin = new Thickness(35, 20, 35, 20);
                    image1.Margin = margin;
                    image1.Height = 50;
                    image1.Width = 50;
                }
            }
        }

        private void button_GotFocus(object sender, RoutedEventArgs e)
        {
            image2.Source = SelectedImageSource;
        }

        private void button_LostFocus(object sender, RoutedEventArgs e)
        {
            image2.Source = ImageSource;
        }

	}
}