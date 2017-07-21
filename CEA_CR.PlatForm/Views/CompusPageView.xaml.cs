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
    public partial class CompusPageView : Window
    {
        SchoolInfoManager schoolInfoManager = new SchoolInfoManager();

        public CompusPageView()
        {
            InitializeComponent();

            if (System.Configuration.ConfigurationManager.AppSettings["IsDev"] == "true")
            {
                this.Topmost = false;
            }

            SchoolInfoEntity schoolInfoEntity = null;

            try
            {
                schoolInfoEntity = schoolInfoManager.GetSchoolInfoByCode("CEA");
            }
            catch (Exception ex)
            {
                Framework.MessageBox mb = new Framework.MessageBox();
                mb.Title = "异常提示";
                mb.Message = "数据获取出错, 错误信息:" + ex.Message;
                mb.Topmost = true;
                mb.ShowDialog();
            }

            if (schoolInfoEntity != null && !string.IsNullOrWhiteSpace(schoolInfoEntity.Description))
            {
                this.introduction.Text = schoolInfoEntity.Description;
            }
            else
            {
                this.introduction.Text = @"东航企业大学（即东航技术应用研发中心）位于上海市青浦区的校址正在加紧建设中，总建筑面积18.5万平方米，总投资16.3亿元，落成后将成为集“研、产、学、用”于一体的航空技术研发基地，主要功能涵盖航空飞行、空中服务、机务维修等新技术研发以及民航维修技术应用，代表着东航在技术创新领域的深化发展。东航设想，未来东航大学将带来整个培训体系战略化的转型，比如提升包括飞行、机务、乘务在内的航空专业人才队伍建设空间与硬件质量，同时也为管理人员全面了解业务提供更多的帮助；充分利用基地提供的硬件条件，使东航各专业人员的理论培训与技能实训得到有机结合，使质量得到全面提升；为未来东航实现教育培训资源的全面整合及向外拓展提供了发展空间；为培训自身实现从保障性资产向经营性资产转变提供了可能。
　　                东航集团非常重视高技能人才的培养，已经出台了《关于加强东航高技能人才队伍建设的实施意见》。就在去年年底，中国民航首家企业研究院——东航飞行安全技术应用研究院揭牌，东航飞行安全技术应用研究院是中国民航第一家企业性质的研究院。
                    随着东航企业规模的逐步扩大，东航的培训发展也开始越来越举足轻重，去年，东航培训的投入就达到10亿元人民币。2005年东航培训中心成立，与东方飞行培训有限公司、机务培训中心一起组成完整的培训体系；去年，东航培训从单纯的业务和技能培训向为东航企业提供战略支撑转变，从传统的机械式、单一课堂培训向多元化学习方式转变；明年，东航大学将有望建成，东航的培训将完成向东航企业战略发展全面支撑，强化企业核心竞争力的企业大学的转型发展。";
            }
        }
    }

    public class CompusPageViewModel
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
