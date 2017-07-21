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
using System.Xml.Linq;

namespace Framework
{
    /// <summary>
    /// PCD.xaml 的交互逻辑
    /// </summary>
    public partial class PCD : UserControl
    {
        public PCD()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(PCD), new FrameworkPropertyMetadata("Null"));
        public static readonly DependencyProperty ProvincesProerty = DependencyProperty.Register("Provinces", typeof(string), typeof(PCD), new FrameworkPropertyMetadata("-1"));
        public static readonly DependencyProperty CitiesProerty = DependencyProperty.Register("Cities", typeof(string), typeof(PCD), new FrameworkPropertyMetadata("-1"));
        public static readonly DependencyProperty DistrictsProerty = DependencyProperty.Register("Districts", typeof(string), typeof(PCD), new FrameworkPropertyMetadata("-1"));
        private void txtbox_GotFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(232, 116, 57));
            b1.BorderBrush = brush;
        }

        private void txtbox_LostFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(0xCA, 0xC3, 0xBA));
            b1.BorderBrush = brush;
        }
        /// <summary>
        /// 字段名
        /// </summary>
        public string Lable
        {
            get { return lbl.Text; }
            set { lbl.Text = value; }
        }
        /// <summary>
        /// 省
        /// </summary>
        public string Provinces
        {
            get { return GetValue(ProvincesProerty).ToString(); }
            set {
                SetValue(ProvincesProerty, value);
            }
        }
        /// <summary>
        /// 市
        /// </summary>
        public string Cities
        {
            get { return GetValue(CitiesProerty).ToString(); }
            set { SetValue(CitiesProerty, value); }
        }
        /// <summary>
        /// 区域
        /// </summary>
        public string Districts
        {
            get { return GetValue(DistrictsProerty).ToString(); }
            set
            {
                   SetValue(DistrictsProerty, value);
            }
        }
        /// <summary>
        /// 详细
        /// </summary>
        public string Text
        {
            get { return GetValue(TextProperty).ToString(); }
            set { SetValue(TextProperty, value); }
        }
        private List<object> _pList;
        public List<object> pList { get {
            if (_pList == null)
            {
                _pList = new List<object>();
                XElement xe = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "Address/Provinces.xml");
                var xes = xe.Elements("Province").ToList();
                foreach (var x in xes)
                {
                    pList.Add(new { ID = x.Attribute("ID").Value, ProvinceName = x.Attribute("ProvinceName").Value });
                }
            }
            return _pList;
        } }
        /// <summary>
        /// 加载省份
        /// </summary>
        void LoadProvinces()
        {
            //if (pList == null)
            //{
            //    pList = new List<object>();
            //    XElement xe = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "DataBase/Provinces.xml");
            //    var xes = xe.Elements("Province").ToList();
            //    foreach (var x in xes)
            //    {
            //        pList.Add(new { ID = x.Attribute("ID").Value, ProvinceName = x.Attribute("ProvinceName").Value });
            //    }
            //}
            //cboP.ItemsSource = pList;
            //cboP.Items.Insert(0,"请选择省份");
        }
        /// <summary>
        /// 根据省份ID加载城市
        /// </summary>
        /// <param name="pid"></param>
        void LoadCities(string pid)
        {
            var clist = new List<object>();
            XElement xe = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "Address/Cities.xml");
            var xes = xe.Elements("City").Where(x => x.Attribute("PID").Value.Equals(pid)).ToList();
            foreach (var x in xes)
            {
                clist.Add(new { ID = x.Attribute("ID").Value, CityName = x.Attribute("CityName").Value });
            }
            cboC.ItemsSource = clist;
            //cboC.Items.Insert(0, "请选择城市");
        }
        /// <summary>
        /// 根据城市ID加载地区
        /// </summary>
        /// <param name="cid"></param>
        void LoadDistricts(string cid)
        {
            var dlist = new List<object>();
            XElement xe = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "Address/Districts.xml");
            var xes = xe.Elements("District").Where(x => x.Attribute("CID").Value.Equals(cid)).ToList();
            foreach (var x in xes)
            {
                dlist.Add(new { ID = x.Attribute("ID").Value, DistrictName = x.Attribute("DistrictName").Value });
            }
            cboD.ItemsSource = dlist;
            //cboD.Items.Insert(0, "请选择地区");
        }

        private void cboP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboP.SelectedValue != null)
            {
                var value = cboP.SelectedValue.ToString();
                cboD.ItemsSource = null;
                try
                {
                    if (Provinces.Equals("-1"))
                    {
                        cboC.ItemsSource = null;
                        return;
                    }
                }
                catch {
                    cboC.ItemsSource = null;
                    return;
                }
                LoadCities(value);
                
            }
        }

        private void cboC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cboC.SelectedValue!=null)
            LoadDistricts(cboC.SelectedValue.ToString());
        }

        private void cboP_Loaded(object sender, RoutedEventArgs e)
        {
            if (cboP.SelectedValue != null)
            {
                LoadCities(cboP.SelectedValue.ToString());
                try
                {
                    cboC.SelectedValue = Cities;
                }
                catch {
                    cboC.SelectedValue = "-1";
                }
            }
     //       cboD.ItemsSource = null;
        }

        private void cboC_Loaded(object sender, RoutedEventArgs e)
        {
            if (cboC.SelectedValue != null)
            {
                LoadDistricts(cboC.SelectedValue.ToString());
                try
                {
                    cboD.SelectedValue = Districts;
                }
                catch {
                    cboD.SelectedValue = "-1";
                }
            }
        }

     
    }
}
