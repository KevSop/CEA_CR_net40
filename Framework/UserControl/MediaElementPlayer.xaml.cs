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
    /// MediaElementPlayer.xaml µÄ½»»¥Âß¼­
    /// </summary>
    public partial class MediaElementPlayer : Window
    {
        private string source;
        private string smallImage;

        public MediaElementPlayer(string source, string smallImage)
        {
            InitializeComponent();

            this.source = source;
            this.smallImage = smallImage;

            //mediaElement.Source = new Uri(source);
            //playBtn.IsEnabled = true;
            //mediaElement.Play();
            //mediaElement.ScrubbingEnabled = true;
            //mediaElement.Pause();
            //mediaElement.Position = TimeSpan.FromTicks(1);

            mediaElement.Source = new Uri(smallImage);
            mediaElement.Play();
            mediaElement.ScrubbingEnabled = true;
            mediaElement.Pause();
            mediaElement.Position = TimeSpan.FromTicks(1);
        }

        private void PlayerPause()
        {
            mediaElement.Source = new Uri(source);

            //SetPlayer(true);
            if (playBtn.Content.ToString() == "²¥·Å")
            {
                mediaElement.Play();
                playBtn.Content = "ÔÝÍ£";
            }
            else
            {
                mediaElement.Pause();
                playBtn.Content = "²¥·Å";
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
            playBtn.Content = "²¥·Å";
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
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  