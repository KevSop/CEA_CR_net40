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

namespace CEA_CR.PlatForm.Views
{
    /// <summary>
    /// WebBrowserPageView.xaml 的交互逻辑
    /// </summary>
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class WebBrowserPageView : Window
    {
        WebKit.WebKitBrowser kitBrowser = new WebKit.WebKitBrowser();
        System.Windows.Forms.Integration.WindowsFormsHost windowsFormsHost = new System.Windows.Forms.Integration.WindowsFormsHost();

        string CompnayIndexPage = ConfigurationManager.AppSettings["CompnayIndexPage"];
        string SchoolIndexPage = ConfigurationManager.AppSettings["SchoolIndexPage"];

        string serverIndexPage = "";

        public WebBrowserPageView(int type)
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

            kitBrowser.Navigate(serverIndexPage);

            windowsFormsHost.Child = kitBrowser;
            grdBrowserHost.Children.Add(windowsFormsHost);

            kitBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(kitBrowser_DocumentCompleted);  

            if (System.Configuration.ConfigurationManager.AppSettings["IsDev"] == "true")
            {
                this.Topmost = false;
            }
        }

        void kitBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            kitBrowser.GetScriptManager.ScriptObject = this;
        }


        public void kitBrowser_PlayVideo(string path)
        {
            //var dom = kitBrowser.Document;

            MediaElementPlayer player = new MediaElementPlayer(path);
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
