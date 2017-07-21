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
using Microsoft.Practices.Prism.Commands;
using TouchScreenKeyboard.Controls;

namespace Framework
{
    /// <summary>
    /// SearchBox2.xaml 的交互逻辑
    /// </summary>
    public partial class SearchBoxTeacher : Window
    {
        public SearchBoxTeacher()
        {
            InitializeComponent();
        }
        public string TeacherSearch
        {
            get { return txtTeacherValue.Text; }
            set { txtTeacherValue.Text = value; }
        }
        public DateTime StartValue {
            get { return txtStartValue.SelectedDate; }
            set { txtStartValue.SelectedDate = value; }
        }
        //public DateTime EndValue
        //{
        //    get { return txtEndValue.SelectedDate; }
        //    set { txtEndValue.SelectedDate = value; }
        //}
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

    public class SearchBoxTeacherViewModel
    {
        private ICommand gotFocusCommand;
        public ICommand GotFocusCommand
        {
            get
            {
                if (gotFocusCommand == null)
                {
                    gotFocusCommand = new DelegateCommand<FloatingTouchScreenKeyboard>(Keyboard =>
                    {
                        Keyboard.IsShow = Visibility.Visible;
                    });
                }
                return gotFocusCommand;
            }
        }
        private ICommand lostFocusCommand;
        public ICommand LostFocusCommand
        {
            get
            {
                if (lostFocusCommand == null)
                {
                    lostFocusCommand = new DelegateCommand<FloatingTouchScreenKeyboard>(Keyboard =>
                    {
                        Keyboard.IsShow = Visibility.Hidden;
                    });
                }
                return lostFocusCommand;
            }
        }
    }
}
