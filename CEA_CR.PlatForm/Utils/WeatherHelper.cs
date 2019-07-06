using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Diagnostics;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace CEA_CR.PlatForm.Utils
{
    public class WeatherHelper
    {
        public static string WeatherInfoService1 = "http://wthrcdn.etouch.cn/weather_mini?citykey=101020100"; 
        public static string WeatherInfoService2 = "http://tianqiapi.com/api.php?style=te&skin=pitaya";
        public static string WeatherInfoService3 = "http://t.weather.sojson.com/api/weather/city/101020100";

        /// <summary>
        /// 获取天气
        /// </summary>
        public static string GetWeather3()
        {
            string weatherInfo = string.Empty;

            try
            {
                string weatherJson = HttpHelper.GetHttpResponse(WeatherInfoService3, 500, true);

                JObject root = (JObject)JsonConvert.DeserializeObject(weatherJson);
                JObject data = (JObject)root["data"];
                JArray weatherList = (JArray)data["forecast"];
                if (weatherList != null && weatherList.Count > 0)
                {
                    string strDate = DateTime.Now.ToString("dd");
                    foreach (var item in weatherList)
                    {
                        string date = item["date"].ToString();
                        if (date.Contains(strDate) || (date.Length == 1 && date == int.Parse(strDate).ToString()) || date.StartsWith(int.Parse(strDate) + "日"))
                        {
                            weatherInfo = string.Format("{0} {1}~{2}", item["type"].ToString(),
                                item["low"].ToString().Replace("低温 ", "").Replace("℃", ""),
                                item["high"].ToString().Replace("高温 ", ""));

                            break;
                        }

                    }
                }
            }
            catch (Exception ex){}

            return weatherInfo;
        }

        /// <summary>
        /// 获取天气
        /// </summary>
        public static string GetWeather2()
        {
            string weatherInfo = string.Empty;

            try
            {
                string weatherJson = HttpHelper.GetHttpResponse(WeatherInfoService2, 500, true);

                if (!string.IsNullOrWhiteSpace(weatherJson))
                {
                    Match match = Regex.Match(weatherJson, @"(?<=<(body)[^>]*>).*(?=</\1>)", RegexOptions.Singleline);

                   if(match.Groups.Count > 0)
                   {
                       string regexstr = @"(&(#)?.+;)|(<[^>]*>)";    //去除HTML标签

                       Regex reg = new Regex(@"(?<=<(em)[^>]*>)(.*?)(?=</em>)", RegexOptions.IgnoreCase);   //取em标签的数据

                       MatchCollection mc = reg.Matches(match.Groups[0].Value);

                       foreach (Match m in mc)
                       {
                           weatherInfo += m.Value + " ";
                       }

                       weatherInfo = Regex.Replace(weatherInfo, regexstr, "", RegexOptions.IgnoreCase);

                       weatherInfo = weatherInfo.Replace("上海", "").Trim().Replace("°C~", "~").Replace("℃~", "~");
                   }
                }
            }
            catch (Exception ex) { }

            return weatherInfo;
        }

        /// <summary>
        /// 获取天气
        /// </summary>
        public static string GetWeather1()
        {
            string weatherInfo = string.Empty;

            try
            {
                string weatherJson = HttpHelper.GetHttpResponse(WeatherInfoService1, 500, true);

                JObject root = (JObject)JsonConvert.DeserializeObject(weatherJson);
                JObject data = (JObject)root["data"];
                JArray weatherList = (JArray)data["forecast"];
                if (weatherList != null && weatherList.Count > 0)
                {
                    string strDate = DateTime.Now.ToString("dd");
                    foreach (var item in weatherList)
                    {
                        string date = item["date"].ToString();
                        if (date.Contains(strDate) || date.StartsWith(int.Parse(strDate) + "日"))
                        {
                            weatherInfo = string.Format("{0} {1}~{2}", item["type"].ToString(),
                                item["low"].ToString().Replace("低温 ", "").Replace("℃", ""),
                                item["high"].ToString().Replace("高温 ", ""));

                            break;
                        }

                    }
                }
            }
            catch (Exception ex) { }
            

            return weatherInfo;
        }

        /// <summary>
        /// 获取天气
        /// </summary>
        public static string GetWeather()
        {
            string weatherInfo = GetWeather1();
            if (string.IsNullOrWhiteSpace(weatherInfo))
            {
                weatherInfo = GetWeather2();
            }
            if (string.IsNullOrWhiteSpace(weatherInfo))
            {
                weatherInfo = GetWeather3();
            }

            return weatherInfo;
        }
    }
}
