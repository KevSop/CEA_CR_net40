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
using System.Windows.Shapes;
using CEA_CR.PlatForm.ViewModels;
using Microsoft.Practices.Prism.Commands;
using CEA_EDU.Domain.Manager;
using CEA_EDU.Domain.Entity;
using WebKit;
using WebKit.Interop;
using System.Threading;
using System.Security.Permissions;
using System.Windows.Forms;
using Framework;
using System.Configuration;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace CEA_CR.PlatForm.Views
{
    /// <summary>
    /// WebBrowserPageView.xaml 的交互逻辑
    /// </summary>
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public partial class WebBrowserPageView : Window
    {
        WebKit.WebKitBrowser kitBrowser;
        System.Windows.Forms.WebBrowser webBrowser;
        System.Windows.Forms.Integration.WindowsFormsHost windowsFormsHost = new System.Windows.Forms.Integration.WindowsFormsHost();

        string CompnayIndexPage = ConfigurationManager.AppSettings["CompnayIndexPage"];
        string SchoolIndexPage = ConfigurationManager.AppSettings["SchoolIndexPage"];

        string serverIndexPage = "";

        public WebBrowserPageView(int type, int webBrowserType = 0)
        {
            InitializeComponent();

            if (type == 1)
            {
                serverIndexPage = CompnayIndexPage;
                lblTitle.Content = "公司介绍";
            }
            else
            {
                serverIndexPage = SchoolIndexPage;
                lblTitle.Content = "校园介绍";
            }

            //默认WebBrowser
            if (webBrowserType == 0)
            {
                webBrowser = new System.Windows.Forms.WebBrowser();
                webBrowser.ObjectForScripting = new WebBrowserCallbackClass();
                webBrowser.ScriptErrorsSuppressed = true;
                webBrowser.Navigate(serverIndexPage);

                windowsFormsHost.Child = webBrowser;
                grdBrowserHost.Children.Add(windowsFormsHost);

            }
            else
            {
                kitBrowser = new WebKit.WebKitBrowser();
                kitBrowser.Navigate(serverIndexPage);
                windowsFormsHost.Child = kitBrowser;
                grdBrowserHost.Children.Add(windowsFormsHost);

                kitBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(kitBrowser_DocumentCompleted);  
            }

            if (System.Configuration.ConfigurationManager.AppSettings["IsDev"] == "true")
            {
                this.Topmost = false;
            }
        }

        void kitBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            kitBrowser.GetScriptManager.ScriptObject = this;
        }

        public void WebBrowserPlayVideo(string path, string smallImage)
        {
            //var dom = kitBrowser.Document;

            MediaElementPlayer player = new MediaElementPlayer(path, smallImage);
            player.Topmost = true;
            player.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (webBrowser != null)
            {
                webBrowser.Dispose();
            }
            if (kitBrowser != null)
            {
                kitBrowser.Dispose();
            }

            GC.Collect();
            this.Close();
        }
        
    }

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public class WebBrowserCallbackClass
    {
        public void WebBrowserPlayVideo(string path, string smallImage)
        {
            MediaElementPlayer player = new MediaElementPlayer(path, smallImage);
            player.Topmost = true;
            player.Show();
        }
    }

    public class WebBrowserPageViewModel
    {
        #region 退出事件
        private ICommand closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new DelegateCommand<Window>(w =>
                    {
                        w.Close();
                    });
                }
                return closeCommand;
            }
        }
        #endregion
    }
}
