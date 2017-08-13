﻿using System;
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

    public class RoomItemResponse
    {
        public List<RoomItem> roomList;
    }

    public class RoomItem
    {
        public string roomId { get; set; }
        public string roomName { get; set; }
    }

}
