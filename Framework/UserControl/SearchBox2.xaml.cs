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
using System.Collections;

namespace Framework
{
    /// <summary>
    /// SearchBox2.xaml 的交互逻辑
    /// </summary>
    public partial class SearchBox2 : Window
    {
        public SearchBox2()
        {
            InitializeComponent();
        }
        public string FieldName {
            get { return cboFieldName.SelectedValue == null ? string.Empty : cboFieldName.SelectedValue.ToString(); }
            set { cboFieldName.SelectedValue = value; }
        }
        public string FieldValue {
            get { return txtFieldValue.Text; }
            set { txtFieldValue.Text = value; }
        }
        public string StartValue {
            get { return txtStartValue.Text; }
            set { txtStartValue.Text = value; }
        }
        public string EndValue {
            get { return txtEndValue.Text; }
            set { txtEndValue.Text = value; }
        }
        public bool EnableStartValue {
            get { return txtStartValue.IsEnabled; }
            set { txtStartValue.IsEnabled = value; }
        }
        public bool EnableEndValue {
            get { return txtEndValue.IsEnabled; }
            set { txtEndValue.IsEnabled = value; }
        }
        public bool EnableFieldValue {
            get { return txtFieldValue.IsEnabled; }
            set { txtFieldValue.IsEnabled = value; }
        }
        /// <summary>
        /// 绑定字段名称集合，集合格式:List<object> 使用匿名对象{ CnName='',EnName=''}
        /// </summary>
        /// <param name="names"></param>
        public void BindFieldNames(List<object> names) {
            cboFieldName.ItemSource = names;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
