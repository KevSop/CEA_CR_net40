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
    /// SettleView.xaml 的交互逻辑
    /// </summary>
    public partial class SettleView : Window
    {
        public SettleView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 应付款
        /// </summary>
        public float PayMoney { get; set; }
        /// <summary>
        /// 未付
        /// </summary>
        public float NoPay { get; set; }
        /// <summary>
        /// 已付
        /// </summary>
        public float Paied { get; set; }
        private void btnToCashSettleView_Click(object sender, RoutedEventArgs e)
        {
            gdCashSettleView.Visibility = Visibility.Visible;
        }

        private void btnToBankCardView_Click(object sender, RoutedEventArgs e)
        {
            gdBankCardView.Visibility = Visibility.Visible;
        }

        private void btnExitCashView_Click(object sender, RoutedEventArgs e)
        {
            gdCashSettleView.Visibility = Visibility.Hidden;
        }

        private void btnExitBCView_Click(object sender, RoutedEventArgs e)
        {
            gdBankCardView.Visibility = Visibility.Hidden;
        }

        private void btnCancer_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
