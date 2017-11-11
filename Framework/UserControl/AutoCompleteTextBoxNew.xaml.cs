using System;
using System.Collections.Generic;
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
using System.Timers;
using Microsoft.Practices.Prism.Commands;
using TouchScreenKeyboard.Controls;

namespace Framework
{
	/// <summary>
	/// UserTextBox.xaml 的交互逻辑
	/// </summary>
	public partial class AutoCompleteTextBoxNew : UserControl
	{
        public AutoCompleteTextBoxNew()
		{
			this.InitializeComponent();

            keypressTimer = new System.Timers.Timer();
            keypressTimer.Elapsed += new System.Timers.ElapsedEventHandler(SearchTextBox_OnTimedEvent);

            lstSearchResult.Visibility = Visibility.Hidden;
            lblHidResultList.Visibility = Visibility.Hidden;
		}
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(AutoCompleteTextBoxNew), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty ReadOnlyProperty = DependencyProperty.Register("ReadOnly", typeof(bool), typeof(AutoCompleteTextBoxNew), new FrameworkPropertyMetadata(false));


        private Timer keypressTimer;
        private delegate void TextChangedCallback();
        private int delayTime = 300;
        private string searchValue;
        public Func<string, List<AutoCompleteItem>> GetSearchResultFunc;

        public string SearchValue
        {
            get
            {
                if (string.IsNullOrWhiteSpace(searchValue))
                {
                    searchValue = txtbox.Text.Trim();
                }
                return searchValue;
            }
            set { searchValue = value; }
        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                lstSearchResult.Focus();
                if (lstSearchResult.SelectedIndex < 0)
                {
                    lstSearchResult.SelectedIndex = 0;
                }
                return;
            }

            if (delayTime > 0)
            {
                keypressTimer.Interval = delayTime;
                keypressTimer.Start();
            }
            else
            {
                SearchTextBox_TextChanged();
            }
        }

        private void SearchTextBox_TextChanged()
        {
            try
            {
                if (txtbox.IsFocused)
                {
                    if (string.IsNullOrWhiteSpace(txtbox.Text.Trim()))
                    {
                        lstSearchResult.ItemsSource = null;
                        lstSearchResult.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        lstSearchResult.DisplayMemberPath = "Text";
                        lstSearchResult.SelectedValuePath = "Value";
                        lstSearchResult.ItemsSource = GetSearchResultFunc(txtbox.Text.Trim());
                        lstSearchResult.Visibility = Visibility.Visible;
                        lblHidResultList.Visibility = Visibility.Visible;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void SearchTextBox_OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            keypressTimer.Stop();
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new TextChangedCallback(this.SearchTextBox_TextChanged));
        }

        private void lstSearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstSearchResult.SelectedIndex >= 0)
            {
                AutoCompleteItem item = lstSearchResult.SelectedItem as AutoCompleteItem;
                if (item != null)
                {
                    txtbox.Text = item.Text;
                    SearchValue = item.Value;
                }
            }
        }

        private void lblHidResultList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lstSearchResult.Visibility = Visibility.Hidden;
            lblHidResultList.Visibility = Visibility.Hidden;
        }
        
        private void lstSearchResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lstSearchResult.Visibility = Visibility.Hidden;
            lblHidResultList.Visibility = Visibility.Hidden;
        }

        private void lstSearchResult_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                lstSearchResult.Visibility = Visibility.Hidden;
                lblHidResultList.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// 获得焦点时边框的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbox_GotFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(232,116,57));
            b1.BorderBrush = brush;
        }

        /// <summary>
        /// 失去焦点时边框的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbox_LostFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(0xCA, 0xC3, 0xBA));
            b1.BorderBrush = brush;
        }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string UserLblText
        {
            get { return lbl.Content.ToString(); }
            set { lbl.Content = value; }
        }

        /// <summary>
        /// 文本框内容
        /// </summary>
        public string Text
        {
            get { return GetValue(TextProperty).ToString(); }
            set { SetValue(TextProperty, value); }
        }
        public bool ReadOnly
        {
            get { return (bool)GetValue(ReadOnlyProperty); }
            set { SetValue(ReadOnlyProperty, value); }
        }
        public string UserText
        {
            get { return txtbox.Text; }
            set { txtbox.Text = value; }
        }
        public int MaxLength
        {
            get { return txtbox.MaxLength; }
            set { txtbox.MaxLength = value; }
        }

        /// <summary>
        /// 控件是否有焦点
        /// </summary>
        public bool IsFocus
        {
            get { return txtbox.IsFocused; }
        }
	}
}
