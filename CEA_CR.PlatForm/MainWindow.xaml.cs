using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CEA_CR.PlatForm.Views;
using System.Threading;
using System.Timers;
using CEA_CR.PlatForm.Utils;

namespace CEA_CR.PlatForm
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Timers.Timer aTimer = new System.Timers.Timer();

        public MainWindow()
        {
            InitializeComponent();

            //this.CoursePart.ImageSource = new BitmapImage(new Uri("book-icon.png", UriKind.Relative));  //先将图片右键生成操作设置成resource
            this.TeacherPart.ImageSource = new BitmapImage(new Uri("ceateacher.png", UriKind.Relative));
            this.RoomPart.ImageSource = new BitmapImage(new Uri("ceaclass.png", UriKind.Relative));
            this.StudentPart.ImageSource = new BitmapImage(new Uri("ceastudent.png", UriKind.Relative));

            this.TeacherPart.SelectedImageSource = new BitmapImage(new Uri("ceateacherselected.png", UriKind.Relative));
            this.RoomPart.SelectedImageSource = new BitmapImage(new Uri("ceaclassselected.png", UriKind.Relative));
            this.StudentPart.SelectedImageSource = new BitmapImage(new Uri("ceastudentselected.png", UriKind.Relative));

            //this.CoursePart.ButtonClick += CoursePart_ButtonClick;
            this.RoomPart.ButtonClick += RoomPart_ButtonClick;
            this.TeacherPart.ButtonClick += TeacherPart_ButtonClick;
            this.StudentPart.ButtonClick += StudentPart_ButtonClick;

            imageWX.Source = new BitmapImage(new Uri("Images/ceawxgzh.png", UriKind.Relative));
            imageLogo.Source = new BitmapImage(new Uri("Images/cealogo.png", UriKind.Relative));

            if (System.Configuration.ConfigurationManager.AppSettings["IsDev"] == "true")
            {
                this.Topmost = false;
            }

            //时间天气信息
            string weatherInfo = WeatherHelper.GetWeather();

            this.txtDate.Text = DateTime.Now.ToString("yyyy年 MM月dd日");
            this.txtTime.Text = DateTime.Now.ToString("HH:mm");
            this.txtWeather.Text = weatherInfo;

            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000 * 60;
            aTimer.Start();
        }

        void CoursePart_ButtonClick(object sender, RoutedEventArgs e)
        {
            //原主界面不用管，新打开的在上层
            //ClassInfoPageView ci = new ClassInfoPageView();
            //ci.Show();
        }

        void RoomPart_ButtonClick(object sender, RoutedEventArgs e)
        {
            //原主界面不用管，新打开的在上层
            //ClassRoomInfoPageView ci = new ClassRoomInfoPageView();
            ClassInfoPageView ci = new ClassInfoPageView();
            ci.Show();
        }

        void TeacherPart_ButtonClick(object sender, RoutedEventArgs e)
        {
            //原主界面不用管，新打开的在上层
            TeacherInfoPageView ci = new TeacherInfoPageView();
            ci.Show();
        }
        void StudentPart_ButtonClick(object sender, RoutedEventArgs e)
        {
            //原主界面不用管，新打开的在上层
            StudentInfoPageView ci = new StudentInfoPageView();
            ci.Show();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (DateTime.Now.Minute == 0)
            {
                string weatherInfo = WeatherHelper.GetWeather();

                Dispatcher.BeginInvoke(new Action(delegate
                {
                    this.txtWeather.Text = weatherInfo;
                }));
            }

            Dispatcher.BeginInvoke(new Action(delegate {
                this.txtDate.Text = DateTime.Now.ToString("yyyy年 MM月dd日");
                this.txtTime.Text = DateTime.Now.ToString("HH:mm");
            }));
        }
    }
}
