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
    /// MediaElementPlayer.xaml 的交互逻辑
    /// </summary>
    public partial class MediaElementPlayer : Window
    {
        public MediaElementPlayer(string source)
        {
            InitializeComponent();

            source = @"D:\东航数字资源文件\公司介绍\VID_20151011_165440.mp4";
            source = "http://dl124.80s.im:920/1706/[%E9%99%88%E5%A5%95%E8%BF%85]%E5%96%9C%E6%AC%A2%E4%B8%80%E4%B8%AA%E4%BA%BA/[%E9%99%88%E5%A5%95%E8%BF%85]%E5%96%9C%E6%AC%A2%E4%B8%80%E4%B8%AA%E4%BA%BA_hd.mp4";

            mediaElement.Source = new Uri(source);
            playBtn.IsEnabled = true;
            mediaElement.Play();
            mediaElement.ScrubbingEnabled = true;
            mediaElement.Pause();
            mediaElement.Position = TimeSpan.FromTicks(1);
        }

        private void PlayerPause()
        {
            //SetPlayer(true);
            if (playBtn.Content.ToString() == "播放")
            {
                mediaElement.Play();
                playBtn.Content = "暂停";
            }
            else
            {
                mediaElement.Pause();
                playBtn.Content = "播放";
            }
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            PlayerPause();
        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            mediaElement.Position = TimeSpan.FromTicks(1);
            playBtn.Content = "播放";
        }

        private void mediaElement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PlayerPause();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mediaElement.Position - TimeSpan.FromSeconds(15) < TimeSpan.Zero)
            {
                mediaElement.Position = TimeSpan.FromTicks(1);
            }
            else
            {
                mediaElement.Position = mediaElement.Position - TimeSpan.FromSeconds(15);
            }
        }

        private void forwardBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = mediaElement.Position + TimeSpan.FromSeconds(15);
        }

    
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  