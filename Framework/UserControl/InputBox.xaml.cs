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
    /// MessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class InputBox : Window
    {
        public InputBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 消息框标题
        /// </summary>
        public string Title 
        {
            get { return txtTitle.Text; }
            set { txtTitle.Text = value; }
        }

        /// <summary>
        /// 第一行文本框提示文本
        /// </summary>
        public string txtTitle1
        {
            get { return txtBox1.Text; }
            set { txtBox1.Text = value; }
        }

        /// <summary>
        /// 第二行文本框提示文本
        /// </summary>
        public string txtTitle2
        {
            get { return txtBox2.Text; }
            set { txtBox2.Text = value; }
        }

        /// <summary>
        /// 第一行显示金额
        /// </summary>
        public string AmountText1
        { 
            get { return txtAmount1.Text;}
            set { txtAmount1.Text = value; }
        }

       /// <summary>
       /// 第二行输入金额
       /// </summary>
        public string AmountText2
        {
            get { return txtAmount2.Text; }
            set { txtAmount2.Text = value; }
        }


        /// <summary>
        /// 输入金额
        /// </summary>
        public string AmountText
        {
            get { return txtAmount.Text; }
            set { txtAmount.Text = value; }
        }

        /// <summary>
        /// 输入金额提示信息
        /// </summary>
        public string AmountTitle
        {
            get { return lbl.Content.ToString(); }
            set { lbl.Content = value; }
        }

        private bool IsContainsDot = false;

        private void btnOne_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtAmount.Text.Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries).GetValue(1).ToString().Length >= 4)
                {
                    //如果小数后为两位时用户输入失效 
                    //txtAmount.Text = txtAmount.Text.Substring(0, txtAmount.Text.Length - 1);
                    TextFocus(txtAmount);
                    return;
                }
            }
            catch
            {
                TextFocus(txtAmount);
            }

            Button btn = (Button)sender;
            txtAmount.Text += btn.Content;
            TextFocus(txtAmount);
            //检测是否已经输入了小数点 
            IsContainsDot = this.txtAmount.Text.Contains(".");
            if (IsContainsDot)
            {
                btnPoint.IsEnabled = false;
            }
            else
            {
                btnPoint.IsEnabled = true;
            }
        }

        //获得焦点
        private void TextFocus(TextBox textbox)
        {
            textbox.Focus();
            textbox.SelectionStart = textbox.Text.Length;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnPoint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.txtAmount.Text.Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries).GetValue(1).ToString().Length >= 2)
                {
                    //如果小数后为两位时用户输入失效 
                    //txtAmount.Text = txtAmount.Text.Substring(0, txtAmount.Text.Length - 1);
                    TextFocus(txtAmount);
                    return;
                }
            }
            catch
            {
                TextFocus(txtAmount);
            }

            txtAmount.Text += btnPoint.Content.ToString().Trim();
            TextFocus(txtAmount);

            //检测是否已经输入了小数点 
            IsContainsDot = this.txtAmount.Text.Contains(".");

            //如果第一个输入了小数点
            if (this.txtAmount.Text.Trim().Length == 1 && IsContainsDot)
            {
                txtAmount.Text = txtAmount.Text.Substring(0, txtAmount.Text.Length - 1);
                return;
            }

            if (IsContainsDot)
            {
                btnPoint.IsEnabled = false;
            }
            else
            {
                btnPoint.IsEnabled = true;
            }
        }

        //清除
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtAmount.Text = "";
            txtAmount.Focus();
            //检测是否已经输入了小数点 
            IsContainsDot = this.txtAmount.Text.Contains(".");
            if (IsContainsDot)
            {
                btnPoint.IsEnabled = false;
            }
            else
            {
                btnPoint.IsEnabled = true;
            }

        }

         //后退
        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (txtAmount.Text.Length > 0)
            {
                txtAmount.Text = txtAmount.Text.Substring(0, txtAmount.Text.Length - 1);
                TextFocus(txtAmount);
            }
            //检测是否已经输入了小数点 
            IsContainsDot = this.txtAmount.Text.Contains(".");
            if (IsContainsDot)
            {
                btnPoint.IsEnabled = false;
            }
            else
            {
                btnPoint.IsEnabled = true;
            }

        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = sender as TextBox;
            //屏蔽非法按键            
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Decimal || e.Key.ToString() == "Tab")
            {
                if (txt.Text.Contains(".") && e.Key == Key.Decimal)
                {
                    e.Handled = true;
                    return;
                }
                e.Handled = false;
            }
            else if (((e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.OemPeriod) && e.KeyboardDevice.Modifiers != ModifierKeys.Shift)
            {
                if (txt.Text.Contains(".") && e.Key == Key.OemPeriod)
                {
                    e.Handled = true;
                    return;
                }
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                if (e.Key.ToString() != "RightCtrl")
                {

                }
            }
        }

        private void txtAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
             //屏蔽中文输入和非法字符粘贴输入
            TextBox textBox = sender as TextBox;
            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);

            int offset = change[0].Offset;
            if (change[0].AddedLength > 0)
            {
                double num = 0;
                if (!Double.TryParse(textBox.Text, out num))
                {
                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);

                }
            }
        }
        
    }
}
