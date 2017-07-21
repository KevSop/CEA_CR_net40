using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CEA_EDU.Domain.Entity;
using CEA_EDU.Common.Extend;
using CEA_EDU.Domain.Entity.ViewEntity;
using System.Data.SqlClient;

namespace CEA_EDU.Domain.Manager
{
    /// <summary>
    /// 班级排课管理类
    /// </summary>
    public class ArrangeClassManager : ManagerBase
    {
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(ArrangeClassEntity entity)
        {
            entity.Valid = "T";
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Insert<ArrangeClassEntity>(session.Connection, entity, session.Transaction);
                session.Commit();
            }
            catch (System.Exception)
            {
                session.Rollback();
                throw;
            }
            finally
            {
                session.Dispose();
            }
        }

        public void Update(ArrangeClassEntity entity)
        {
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Update<ArrangeClassEntity>(session.Connection, entity, session.Transaction);
                session.Commit();
            }
            catch (System.Exception)
            {
                session.Rollback();
                throw;
            }
            finally
            {
                session.Dispose();
            }
        }


        public List<ArrangeClassViewEntity> GetArrangeClassViewList(int? curriculumID, int? classID, int? classRoomID, int? teacherID, DateTime? startTime, DateTime? endTime)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select a.ID, cc.CurriculumID, cc.Code as 'CurriculumCode', cc.Name as 'CurriculumName', c.ClassID, c.Code as 'ClassCode',
                               c.Name as 'ClassName', cr.ClassRoomID, cr.Code as 'ClassRoomCode', cr.Name as 'ClassRoomName',
                               u.ID as 'TeacherID', u.Code as 'TeacherCode', u.Name as 'TeacherName', a.StartTime, a.EndTime,
                               a.Remark, a.BespeakCount, a.AttendCount, a.PassedCount
                           from ArrangeClass(nolock) a
                               inner join CurriculumInfo(nolock) cc on cc.CurriculumID = a.CurriculumID and cc.valid = 'T'
                               inner join ClassInfo(nolock) c on c.ClassID = a.ClassID and c.valid = 'T'
                               inner join ClassRoomInfo(nolock) cr on cr.ClassRoomID = a.ClassRoomID and cr.valid = 'T'
                               left join UserInfo(nolock) u on u.ID = a.TeacherID and u.valid = 'T'
                           where a.valid = 'T' ");

            List<SqlParameter> paraList = new List<SqlParameter>();

            if (curriculumID != null && curriculumID > 0)
            {
                sql.Append(" and a.CurriculumID = @CurriculumID");
                
                SqlParameter para = new SqlParameter("@CurriculumID", SqlDbType.Int);
                para.SqlValue = curriculumID;
                paraList.Add(para);
            }

            if (classID != null && classID > 0)
            {
                sql.Append(" and a.ClassID = @ClassID");

                SqlParameter para = new SqlParameter("@ClassID", SqlDbType.Int);
                para.SqlValue = classID;
                paraList.Add(para);
            }

            if (classRoomID != null && classRoomID > 0)
            {
                sql.Append(" and a.ClassRoomID = @ClassRoomID");

                SqlParameter para = new SqlParameter("@ClassRoomID", SqlDbType.Int);
                para.SqlValue = classRoomID;
                paraList.Add(para);
            }

            if (teacherID != null && teacherID > 0)
            {
                sql.Append(" and a.TeacherID = @TeacherID");

                SqlParameter para = new SqlParameter("@TeacherID", SqlDbType.Int);
                para.SqlValue = teacherID;
                paraList.Add(para);
            }

            if (teacherID != null && teacherID > 0)
            {
                sql.Append(" and a.TeacherID = @TeacherID");

                SqlParameter para = new SqlParameter("@TeacherID", SqlDbType.Int);
                para.SqlValue = teacherID;
                paraList.Add(para);
            }

            if (startTime != null)
            {
                sql.Append(" and a.StartTime >= @StartTime");

                SqlParameter para = new SqlParameter("@StartTime", SqlDbType.DateTime);
                para.SqlValue = startTime;
                paraList.Add(para);
            }


            if (endTime != null)
            {
                sql.Append(" and a.EndTime < @EndTime");

                SqlParameter para = new SqlParameter("@EndTime", SqlDbType.DateTime);
                para.SqlValue = endTime;
                paraList.Add(para);
            }

            DataSet ds = DbHelperSQL.Query(sql.ToString(), paraList.ToArray());

            List<ArrangeClassViewEntity> list = new List<ArrangeClassViewEntity>();

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ArrangeClassViewEntity entity = new ArrangeClassViewEntity();

                    entity.ID = Ext.ToInt(dr["ID"]);
                    entity.CurriculumID = Ext.ToInt(dr["CurriculumID"]);
                    entity.CurriculumCode = Ext.ToString(dr["CurriculumCode"]);
                    entity.CurriculumName = Ext.ToString(dr["CurriculumName"]);
                    entity.ClassID = Ext.ToInt(dr["ClassID"]);
                    entity.ClassCode = Ext.ToString(dr["ClassCode"]);
                    entity.ClassName = Ext.ToString(dr["ClassName"]);
                    entity.ClassRoomID = Ext.ToInt(dr["ClassRoomID"]);
                    entity.ClassRoomCode = Ext.ToString(dr["ClassRoomCode"]);
                    entity.ClassRoomName = Ext.ToString(dr["ClassRoomName"]);
                    entity.TeacherID = Ext.ToInt(dr["TeacherID"]);
                    entity.TeacherCode = Ext.ToString(dr["TeacherCode"]);
                    entity.TeacherName = Ext.ToString(dr["TeacherName"]);
                    entity.StartTime = Ext.ToDateOrNull(dr["StartTime"]);
                    entity.EndTime = Ext.ToDateOrNull(dr["EndTime"]);
                    entity.Remark = Ext.ToString(dr["Remark"]);
                    entity.BespeakCount = Ext.ToIntOrNull(dr["BespeakCount"]);
                    entity.AttendCount = Ext.ToIntOrNull(dr["AttendCount"]);
                    entity.PassedCount = Ext.ToIntOrNull(dr["PassedCount"]);

                    list.Add(entity);
                }
            }

            return list;
        }

        public ArrangeClassEntity GetArrangeClassByID(int id)
        {
            string sql = @"select * from ArrangeClass(nolock) where valid = 'T' and id=@id ";
            return Repository.Query<ArrangeClassEntity>(sql, new { id = id }).FirstOrDefault();
        }

        public ArrangeClassEntity GetArrangeClassByKeys(int curriculumID, int classID, int classRoomID, int teacherID)
        {
            string sql = @"select * from ArrangeClass(nolock) where curriculumID=@curriculumID and classID=@classID and classRoomID=@classRoomID and teacherID=@teacherID";
            return Repository.Query<ArrangeClassEntity>(sql, new { curriculumID=curriculumID, classID=classID, classRoomID=classRoomID, teacherID=@teacherID}).FirstOrDefault();
        }

        public List<ArrangeClassEntity> GetArrangeClassByCurriculumID(int curriculumID)
        {
            string sql = @"select * from ArrangeClass(nolock) where valid = 'T' and curriculumID=@curriculumID ";
            return Repository.Query<ArrangeClassEntity>(sql, new { curriculumID = curriculumID }).ToList();
        }

        public List<ArrangeClassEntity> GetArrangeClassByClassID(int classID)
        {
            string sql = @"select * from ArrangeClass where valid = 'T' and classID=@classID ";
            return Repository.Query<ArrangeClassEntity>(sql, new { classID = classID }).ToList();
        }

        public List<ArrangeClassEntity> GetArrangeClassByClassRoomID(int classRoomID)
        {
            string sql = @"select * from ArrangeClass(nolock) where valid = 'T' and classRoomID=@classRoomID ";
            return Repository.Query<ArrangeClassEntity>(sql, new { classRoomID = classRoomID }).ToList();
        }

        public List<ArrangeClassEntity> GetArrangeClassByTeacherID(int teacherID)
        {
            string sql = @"select * from ArrangeClass(nolock) where valid = 'T' and teacherID=@teacherID ";
            return Repository.Query<ArrangeClassEntity>(sql, new { teacherID = teacherID }).ToList();
        }

        public List<ArrangeClassEntity> GetSearch(string sort, string order, int offset, int pageSize, out int total)
        {
            int pageCount = 0;
            string querySql = string.Format("select * from ArrangeClass(nolock) where valid = 'T'");
            DataTable dt = SplitPage.SqlSplitPage(querySql, string.Format("order by {0} {1}", sort, order), null, offset / pageSize, pageSize, out pageCount, out total);

            List<ArrangeClassEntity> list = new List<ArrangeClassEntity>();
            foreach (DataRow dr in dt.Rows)
            {
                ArrangeClassEntity entity = new ArrangeClassEntity();

                entity.ID = Ext.ToInt(dr["ID"]);
                entity.CurriculumID = Ext.ToInt(dr["CurriculumID"]);
                entity.ClassID = Ext.ToInt(dr["ClassID"]);
                entity.ClassRoomID = Ext.ToInt(dr["ClassRoomID"]);
                entity.TeacherID = Ext.ToInt(dr["TeacherID"]);
                entity.StartTime = Ext.ToDateOrNull(dr["StartTime"]);
                entity.EndTime = Ext.ToDateOrNull(dr["EndTime"]);
                entity.Remark = Ext.ToString(dr["Remark"]);
                entity.BespeakCount = Ext.ToIntOrNull(dr["BespeakCount"]);
                entity.AttendCount = Ext.ToIntOrNull(dr["AttendCount"]);
                entity.PassedCount = Ext.ToIntOrNull(dr["PassedCount"]);
                entity.Valid = Ext.ToString(dr["Valid"]);
                entity.CreateTime = Ext.ToDate(dr["CreateTime"]);
                entity.CreateBy = Ext.ToString(dr["CreateBy"]);
                entity.UpdateTime = Ext.ToDate(dr["UpdateTime"]);
                entity.UpdateBy = Ext.ToString(dr["UpdateBy"]);

                list.Add(entity);
            }

            return list;

        }
    }
}