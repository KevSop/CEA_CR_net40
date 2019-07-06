using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string weatherInfo = WeatherHelper.GetWeather();  
            label1.Text = weatherInfo;

            label2.Text = HttpHelper.GetHttpResponse(WeatherHelper.WeatherInfoService1, 500, true);

            label3.Text = HttpHelper.GetHttpResponse(WeatherHelper.WeatherInfoService2, 500, true);
        }
    }
}
