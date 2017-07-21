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

namespace CEA_CR.PlatForm.Views
{
    /// <summary>
    /// Course.xaml 的交互逻辑
    /// </summary>
    public partial class CompanyPageView : Window
    {
        CompanyInfoManager companyInfoManager = new CompanyInfoManager();

        public CompanyPageView()
        {
            InitializeComponent();

            if (System.Configuration.ConfigurationManager.AppSettings["IsDev"] == "true")
            {
                this.Topmost = false;
            }

            CompanyInfoEntity companyInfoEntity = null;

            try 
            {
                companyInfoEntity = companyInfoManager.GetCompanyInfoByCode("CEA");
            }
            catch (Exception ex)
            {
                Framework.MessageBox mb = new Framework.MessageBox();
                mb.Title = "异常提示";
                mb.Message = "数据获取出错, 错误信息:" + ex.Message;
                mb.Topmost = true;
                mb.ShowDialog();
            }

            if (companyInfoEntity != null && !string.IsNullOrWhiteSpace(companyInfoEntity.Description))
            {
                this.introduction.Text = companyInfoEntity.Description;
            }
            else
            {
                this.introduction.Text = "中国东方航空股份有限公司（China Eastern Airlines）是一家总部位于中国上海的国有控股航空公司，在原中国东方航空集团公司的基础上，兼并中国西北航空公司，联合中国云南航空公司重组而成。是中国民航第一家在香港、纽约和上海三地上市的航空公司，1997年2月4日、5日及11月5日，中国东方航空股份有限公司分别在纽约证券交易所、香港联合交易所和上海证券交易所成功挂牌上市。是中国三大国有大型骨干航空企业之一（其余二者是中国国际航空股份有限公司、中国南方航空股份有限公司）。";
            }
        }
    }

    public class CompanyPageViewModel
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
