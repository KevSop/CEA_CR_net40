using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CEA_CR.PlatForm.Utils;

namespace CEA_CR.PlatForm
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            log4net.Config.XmlConfigurator.Configure();

            //设置注册表IE版本（ForWebBrowser）
            IEVersion.BrowserEmulationSet();

            // 在异常由应用程序引发但未进行处理时发生。主要指的是UI线程。
            this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
            //  当某个异常未被捕获时出现。主要指的是非UI线程
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //可以记录日志并转向错误bug窗口友好提示用户
            if (e.ExceptionObject is System.Exception)
            {
                Exception ex = (System.Exception)e.ExceptionObject;

                log4net.ILog log = log4net.LogManager.GetLogger("SMTPAppender");
                log.Error(ex.Message, ex);

                Framework.MessageBox mb = new Framework.MessageBox();
                mb.Topmost = true;
                mb.Title = "异常提示";
                mb.Message = ex.Message;
                mb.ShowDialog();

                //重启程序，需要时加上重启的参数
                System.Diagnostics.ProcessStartInfo cp = new System.Diagnostics.ProcessStartInfo();
                cp.FileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                cp.Arguments = "";
                cp.UseShellExecute = true;
                System.Diagnostics.Process.Start(cp);
            }
        }
        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //可以记录日志并转向错误bug窗口友好提示用户
            e.Handled = true;


            log4net.ILog log = log4net.LogManager.GetLogger(this.GetType());
            log.Error(e.Exception.Message, e.Exception);

            Framework.MessageBox mb = new Framework.MessageBox();
            mb.Topmost = true;
            mb.Title = "异常提示";
            mb.Message = e.Exception.Message;
            mb.ShowDialog();

        }
    }
}
