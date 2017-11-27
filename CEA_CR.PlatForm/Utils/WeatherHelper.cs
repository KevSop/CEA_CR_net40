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
        public static string WeatherInfoService3 = "http://www.sojson.com/open/api/weather/json.shtml?city=";

        /// <summary>
        /// 获取天气
        /// </summary>
        public static string GetWeather3(string cityName)
        {
            string weatherInfo = string.Empty;

            if (!string.IsNullOrWhiteSpace(cityName))
            {
                try
                {
                    cityName = HttpUtility.UrlEncode(cityName, Encoding.UTF8);
                    string weatherJson = HttpHelper.GetHttpResponse(WeatherInfoService3 + cityName, 500, true);

                    JObject root = (JObject)JsonConvert.DeserializeObject(weatherJson);
                    JObject data = (JObject)root["data"];
                    JArray weatherList = (JArray)data["forecast"];
                    if (weatherList != null && weatherList.Count > 0)
                    {
                        string strDate = DateTime.Now.ToString("dd");
                        foreach (var item in weatherList)
                        {
                           string date = item["date"].ToString();
                           if(date.Contains(strDate))
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
            }

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

                       weatherInfo = match.Groups[0].Value;

                       weatherInfo = Regex.Replace(weatherInfo, regexstr, "", RegexOptions.IgnoreCase);

                       weatherInfo = weatherInfo.Replace("上海", "").Trim().Replace("°C~", "~");
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
                        if (date.Contains(strDate))
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
                weatherInfo = GetWeather3("上海");
            }

            return weatherInfo;
        }
    }
}
