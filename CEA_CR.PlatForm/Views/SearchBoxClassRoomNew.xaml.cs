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
using CEA_CR.PlatForm.Utils;
using Framework;
using System.Timers;
using CEA_EDU.Domain.Manager;

namespace CEA_CR.PlatForm.Views
{
    /// <summary>
    /// SearchBox2.xaml 的交互逻辑
    /// </summary>
    public partial class SearchBoxClassRoomNew : Window
    {
        public SearchBoxClassRoomNew()
        {
            InitializeComponent();

            InitSearchTextBox();
        }
        public string RoomSearch
        {
            get { return txtRoomValue.Text; }
            set { txtRoomValue.Text = value; }
        }
        //public DateTime StartValue {
        //    get { return txtStartValue.SelectedDate; }
        //    set { txtStartValue.SelectedDate = value; }
        //}
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

        private Timer keypressTimer;
        private delegate void TextChangedCallback();
        private int delayTime;
        private string roomValue;
        public Func<string, List<RoomItem>> getSearchResultAction;

        public void InitSearchTextBox()
        {
            delayTime = 300;

            getSearchResultAction = getSearchResult;

            keypressTimer = new System.Timers.Timer();
            keypressTimer.Elapsed += new System.Timers.ElapsedEventHandler(SearchTextBox_OnTimedEvent);
        }

        public string SearchTextBoxValue
        {
            get {
                if (string.IsNullOrWhiteSpace(roomValue))
                {
                    roomValue = txtRoomValue.UserText;
                }
                return roomValue; 
            }
            set { roomValue = value; }
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
                if (txtRoomValue.IsFocus)
                {
                    if (string.IsNullOrWhiteSpace(txtRoomValue.UserText.Trim()))
                    {
                        lstSearchResult.ItemsSource = null;
                        lstSearchResult.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        lstSearchResult.DisplayMemberPath = "roomName";
                        lstSearchResult.SelectedValuePath = "roomId";
                        lstSearchResult.ItemsSource = getSearchResultAction(txtRoomValue.UserText.Trim());
                        lstSearchResult.Visibility = Visibility.Visible;
                    }
                }
            }
            catch(Exception ex) { System.Windows.Forms.MessageBox.Show(ex.Message); }
        }

        public List<RoomItem> getSearchResult(string searchKey)
        {
            //HttpDataService service = new HttpDataService();
            //List<RoomItem> roomList = service.GetSearchRoomList(searchKey);

            //if (roomList != null)
            //{
            //    roomList = roomList.Where(r => r.roomName.Contains(searchKey)).ToList();
            //}

            List<RoomItem> result = new List<RoomItem>();

            ClassRoomInfoManager classRoomInfoManager = new ClassRoomInfoManager();

            var roomList = classRoomInfoManager.GetClassRoomInfoByName(searchKey);

            if(roomList != null){
                roomList.ForEach(r=> result.Add(new RoomItem(){ roomId = r.Code, roomName = r.Name}));
            }

            return result;
        }

        private void SearchTextBox_OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            keypressTimer.Stop();
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new TextChangedCallback(this.SearchTextBox_TextChanged));
        }

        private void lstSearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstSearchResult.SelectedIndex >= 0)
            {
                RoomItem roomItem = lstSearchResult.SelectedItem as RoomItem;
                if (roomItem != null)
                {
                    txtRoomValue.UserText = roomItem.roomName;
                    SearchTextBoxValue = roomItem.roomId;
                }
            }
        }

    }

    public class SearchBoxClassRoomNewViewModel
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
