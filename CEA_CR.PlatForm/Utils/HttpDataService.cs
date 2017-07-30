using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace CEA_CR.PlatForm.Utils
{
    public class HttpDataService
    {
        public List<CurrentCourseItem> GetCurrentCourse(string classRoomId)
        {
            List<CurrentCourseItem> result = new List<CurrentCourseItem>();

            if (string.IsNullOrWhiteSpace(classRoomId))
            {
                return result;
            }

            classRoomId = classRoomId.ToUpper();

            //#region 测试数据
            //result.Add(new CurrentCourseItem { classId = "354", className = "新雇员训练1671（本部）", courseId = "3", courseName = "各种机型设备理论", time = "2016-10-11 07:30" });
            //result.Add(new CurrentCourseItem { classId = "354", className = "新雇员训练1671（本部）", courseId = "33", courseName = "紧急医学事件处置训练理论", time = "2016-10-11 15:30" });
            //return result;
            //#endregion

            string cacheKey = string.Format("course:{0}", classRoomId);
            result = CacheHelper.CacheManager.Get<List<CurrentCourseItem>>(cacheKey);
            if (result == null || result.Count == 0)
            {
                string responseJson = HttpHelper.GetHttpResponse(string.Format(ConfigStatic.GetCurrentCourseUrl, ConfigStatic.userName, ConfigStatic.password, classRoomId));
                
                if (true)
                {
                    var response = JsonConvert.DeserializeObject<CurrentCourseResponse>(responseJson);
                    if (response != null)
                    {
                        result = response.classnow;
                    }
                }
                else
                {
                    using (StringReader rdr = new StringReader(responseJson))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(CurrentCourseResponse));
                        var response = (CurrentCourseResponse)serializer.Deserialize(rdr);
                        if (response != null)
                        {
                            result = response.classnow;
                        }
                    }
                }

                if (result != null && result.Count > 0)
                {
                    result = result.OrderByDescending(r => r.time).ThenBy(r => r.courseName).ToList();
                    CacheHelper.CacheManager.Set(cacheKey, result, 10);
                }
            }
            return result;
        }

        public List<CourseScheduleItem> GetCourseSchedule(string identityCard, string month, string userType)
        {
            List<CourseScheduleItem> result = new List<CourseScheduleItem>();

            if (string.IsNullOrWhiteSpace(identityCard))
            {
                return result;
            }

            //#region 测试数据
            //result.Add(new CourseScheduleItem { classId = "27305333", className = "新雇员训练1671（本部）", courseId = "734", courseName = "机上服务标准与程序操作", place = "710", t_date = "2016-10-29", time = "07:30", roomNo = "JS-050" });
            //result.Add(new CourseScheduleItem { classId = "27305333", className = "新雇员训练1671（本部）", courseId = "610", courseName = "旅客特殊服务", place = "550", t_date = "2016-10-20", time = "14:30", roomNo = "JS-030" });
            //return result;
            //#endregion

            string cacheKey = string.Format("schedule:{0}-{1}-{2}", identityCard, month, userType);
            result = CacheHelper.CacheManager.Get<List<CourseScheduleItem>>(cacheKey);
            if (result == null || result.Count == 0)
            {
                string responseJson = HttpHelper.GetHttpResponse(string.Format(ConfigStatic.GetCourseScheduleUrl, ConfigStatic.userName, ConfigStatic.password, identityCard, month, userType));
                
                if (true)
                {
                    var response = JsonConvert.DeserializeObject<CourseScheduleResponse>(responseJson);
                    if (response != null)
                    {
                        result = response.courseAndClassList;
                    }
                }
                else
                {
                    using (StringReader rdr = new StringReader(responseJson))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(CourseScheduleResponse));
                        var response = (CourseScheduleResponse)serializer.Deserialize(rdr);
                        if (response != null)
                        {
                            result = response.courseAndClassList;
                        }
                    }
                }

                if (result != null && result.Count > 0)
                {
                    result = result.OrderByDescending(r => r.t_date).ThenByDescending(r => r.time).ThenBy(r => r.courseName).ToList();
                    CacheHelper.CacheManager.Set(cacheKey, result, 10);
                }
            }
            return result;
        }

    }

    public class CurrentCourseResponse
    {
        public List<CurrentCourseItem> classnow;
    }

    public class CurrentCourseItem
    {
        public string courseId { get; set; }
        public string courseName { get; set; }
        public string classId { get; set; }
        public string className { get; set; }
        public string time { get; set; }
    }

    public class CourseScheduleResponse
    {
        public List<CourseScheduleItem> courseAndClassList;
    }

    public class CourseScheduleItem
    {
        public string t_date { get; set; }
        public string courseId { get; set; }
        public string courseName { get; set; }
        public string classId { get; set; }
        public string className { get; set; }
        public string time { get; set; }
        public string place { get; set; }
        public string roomNo { get; set; }
    }

}
