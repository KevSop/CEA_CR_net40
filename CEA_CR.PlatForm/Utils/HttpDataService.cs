using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Web;

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

        public List<RoomItem> GetSearchRoomList(string room)
        {
            List<RoomItem> result = new List<RoomItem>();

            if (string.IsNullOrWhiteSpace(room))
            {
                return result;
            }

            #region 测试数据
            result.Add(new RoomItem { roomId = "JS-030", roomName = "青浦3号楼300" });
            result.Add(new RoomItem { roomId = "JS-031", roomName = "青浦3号楼301" });
            result.Add(new RoomItem { roomId = "JS-032", roomName = "青浦3号楼302" });
            result.Add(new RoomItem { roomId = "JS-033", roomName = "青浦3号楼303" });
            result.Add(new RoomItem { roomId = "JS-034", roomName = "青浦3号楼304" });
            result.Add(new RoomItem { roomId = "HQ-035", roomName = "虹桥3号楼305" });
            result.Add(new RoomItem { roomId = "HQ-036", roomName = "虹桥3号楼306" });
            result.Add(new RoomItem { roomId = "HQ-037", roomName = "虹桥3号楼307" });
            result.Add(new RoomItem { roomId = "HQ-038", roomName = "虹桥3号楼308" });
            result.Add(new RoomItem { roomId = "HQ-039", roomName = "虹桥3号楼309" });
            return result;
            #endregion


            //string responseJson = HttpHelper.GetHttpResponse(string.Format(ConfigStatic.GetCurrentCourseUrl, ConfigStatic.userName, ConfigStatic.password, classRoomId));

            //var response = JsonConvert.DeserializeObject<RoomItemResponse>(responseJson);
            //if (response != null)
            //{
            //    result = response.roomList;
            //}
               
            return result;
        }

        public List<ClassInfoItem> GetSearchClassInfoList(string className)
        {
            List<ClassInfoItem> result = new List<ClassInfoItem>();

            if (string.IsNullOrWhiteSpace(className))
            {
                return result;
            }

            #region 测试数据
            //result.Add(new ClassInfoItem { ClassId = "JS-A30", ClassName = "青浦A30班" });
            //result.Add(new ClassInfoItem { ClassId = "JS-A31", ClassName = "青浦A31班" });
            //result.Add(new ClassInfoItem { ClassId = "JS-A32", ClassName = "青浦A32班" });
            //result.Add(new ClassInfoItem { ClassId = "JS-A33", ClassName = "青浦A33班" });
            //result.Add(new ClassInfoItem { ClassId = "JS-A34", ClassName = "青浦A34班" });
            //result.Add(new ClassInfoItem { ClassId = "HQ-A35", ClassName = "虹桥A35班" });
            //result.Add(new ClassInfoItem { ClassId = "HQ-A36", ClassName = "虹桥A36班" });
            //result.Add(new ClassInfoItem { ClassId = "HQ-A37", ClassName = "虹桥A37班" });
            //result.Add(new ClassInfoItem { ClassId = "HQ-A38", ClassName = "虹桥A38班" });
            //result.Add(new ClassInfoItem { ClassId = "HQ-A39", ClassName = "虹桥A39班" });
            //return result;
            #endregion


            string responseJson = HttpHelper.GetHttpResponse(string.Format(ConfigStatic.GetClassInfoUrl, ConfigStatic.userName, ConfigStatic.password, HttpUtility.UrlEncode(className)));

            var response = JsonConvert.DeserializeObject<ClassInfoItemResponse>(responseJson);
            if (response != null)
            {
                result = response.classList;
            }

            return result;
        }

        public List<CourseScheduleByBJItem> GetCourseScheduleByClassInfo(string classID, string month)
        {
            List<CourseScheduleByBJItem> result = new List<CourseScheduleByBJItem>();

            if (string.IsNullOrWhiteSpace(classID))
            {
                return result;
            }

            #region 测试数据
            //result.Add(new CourseScheduleItem { classId = "27305333", className = "新雇员训练1671（本部）", courseId = "734", courseName = "机上服务标准与程序操作", place = "710", t_date = "2016-10-29", time = "07:30", roomNo = "JS-050" });
            //result.Add(new CourseScheduleItem { classId = "27305333", className = "新雇员训练1671（本部）", courseId = "610", courseName = "旅客特殊服务", place = "550", t_date = "2016-10-20", time = "14:30", roomNo = "JS-030" });
            //return result;
            #endregion

            string cacheKey = string.Format("classInfoSchedule:{0}-{1}", classID, month);
            result = CacheHelper.CacheManager.Get<List<CourseScheduleByBJItem>>(cacheKey);
            if (result == null || result.Count == 0)
            {
                string responseJson = HttpHelper.GetHttpResponse(string.Format(ConfigStatic.GetCourseScheduleByBJUrl, ConfigStatic.userName, ConfigStatic.password, classID, month));

                if (true)
                {
                    var response = JsonConvert.DeserializeObject<CourseScheduleByBJResponse>(responseJson);
                    if (response != null)
                    {
                        result = response.classList;
                    }
                }
                else
                {
                    using (StringReader rdr = new StringReader(responseJson))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(CourseScheduleByBJResponse));
                        var response = (CourseScheduleByBJResponse)serializer.Deserialize(rdr);
                        if (response != null)
                        {
                            result = response.classList;
                        }
                    }
                }

                if (result != null && result.Count > 0)
                {
                    result = result.OrderByDescending(r => r.T_DATE).ThenByDescending(r => r.TIME).ThenBy(r => r.COURSE_NAME).ToList();
                    CacheHelper.CacheManager.Set(cacheKey, result, 10);
                }
            }
            return result;
        }

        public List<CourseScheduleByBJItem> GetCourseScheduleByClassInfo(string classID)
        {
            List<CourseScheduleByBJItem> result = new List<CourseScheduleByBJItem>();

            if (string.IsNullOrWhiteSpace(classID))
            {
                return result;
            }

            //#region 测试数据
            result.Add(new CourseScheduleByBJItem { CLASS_ID = "27305333", CLASS_NAME = "新雇员训练1671（本部）", COURSEID = "734", COURSE_NAME = "机上服务标准与程序操作", PLACE = "710", T_DATE = "2016-10-29", TIME = "07:30", ROOMNO = "JS-050" });
            result.Add(new CourseScheduleByBJItem { CLASS_ID = "27305333", CLASS_NAME = "新雇员训练1671（本部）", COURSEID = "610", COURSE_NAME = "旅客特殊服务", PLACE = "550", T_DATE = "2016-10-20", TIME = "14:30", ROOMNO = "JS-030" });
            return result;
            //#endregion

            string cacheKey = string.Format("classInfoSchedule:{0}", classID);
            result = CacheHelper.CacheManager.Get<List<CourseScheduleByBJItem>>(cacheKey);
            if (result == null || result.Count == 0)
            {
                string responseJson = HttpHelper.GetHttpResponse(string.Format(ConfigStatic.GetCourseScheduleUrl, ConfigStatic.userName, ConfigStatic.password, classID));

                if (true)
                {
                    var response = JsonConvert.DeserializeObject<CourseScheduleByBJResponse>(responseJson);
                    if (response != null)
                    {
                        result = response.classList;
                    }
                }
                else
                {
                    using (StringReader rdr = new StringReader(responseJson))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(CourseScheduleByBJResponse));
                        var response = (CourseScheduleByBJResponse)serializer.Deserialize(rdr);
                        if (response != null)
                        {
                            result = response.classList;
                        }
                    }
                }

                if (result != null && result.Count > 0)
                {
                    result = result.OrderByDescending(r => r.T_DATE).ThenByDescending(r => r.TIME).ThenBy(r => r.COURSE_NAME).ToList();
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

    public class CourseScheduleByBJResponse
    {
        public List<CourseScheduleByBJItem> classList;
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

    public class CourseScheduleByBJItem
    {
        public string COURSEID { get; set; }
        public string ROOMNO { get; set; }
        public string T_DATE { get; set; }
        public string TIME { get; set; }
        public string PLACE { get; set; }
        public string CLASS_ID { get; set; }
        public string CLASS_NAME { get; set; }
        public string COURSE_NAME { get; set; }
    }

    public class RoomItemResponse
    {
        public List<RoomItem> roomList;
    }

    public class RoomItem
    {
        public string roomId { get; set; }
        public string roomName { get; set; }
    }

    public class ClassInfoItemResponse
    {
        public List<ClassInfoItem> classList;
    }

    public class ClassInfoItem
    {
        public string ID { get; set; }
        public string NAME { get; set; }
    }

}
