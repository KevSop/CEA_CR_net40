using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CEA_EDU.Domain.Entity;
using CEA_EDU.Common.Extend;

namespace CEA_EDU.Domain.Manager
{
    /// <summary>
    /// 学生历史课程管理类
    /// </summary>
    public class StudentClassHistoryManager : ManagerBase
    {
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(StudentClassHistoryEntity entity)
        {
            entity.Valid = "T";
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Insert<StudentClassHistoryEntity>(session.Connection, entity, session.Transaction);
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

        public void Update(StudentClassHistoryEntity entity)
        {
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Update<StudentClassHistoryEntity>(session.Connection, entity, session.Transaction);
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

        public StudentClassHistoryEntity GetStudentClassHistoryByID(int id)
        {
            string sql = @"select * from StudentClassHistory(nolock) where valid = 'T' and id=@id ";
            return Repository.Query<StudentClassHistoryEntity>(sql, new { id = id }).FirstOrDefault();
        }

        public List<StudentClassHistoryEntity> GetStudentClassHistoryByStudentID(int studentID)
        {
            string sql = @"select * from StudentClassHistory(nolock) where valid = 'T' and studentID=@studentID ";
            return Repository.Query<StudentClassHistoryEntity>(sql, new { studentID = studentID }).ToList();
        }

        public List<StudentClassHistoryEntity> GetSearch(string sort, string order, int offset, int pageSize, out int total)
        {
            int pageCount = 0;
            string querySql = string.Format("select * from StudentClassHistory(nolock) where valid = 'T'");
            DataTable dt = SplitPage.SqlSplitPage(querySql, string.Format("order by {0} {1}", sort, order), null, offset / pageSize, pageSize, out pageCount, out total);

            List<StudentClassHistoryEntity> list = new List<StudentClassHistoryEntity>();
            foreach (DataRow dr in dt.Rows)
            {
                StudentClassHistoryEntity entity = new StudentClassHistoryEntity();

                entity.ID = Ext.ToInt(dr["ID"]);
                entity.StudentID = Ext.ToInt(dr["StudentID"]);
                entity.ArrangeClassID = Ext.ToInt(dr["ArrangeClassID"]);
                entity.StartTime = Ext.ToDateOrNull(dr["StartTime"]);
                entity.EndTime = Ext.ToDateOrNull(dr["EndTime"]);
                entity.Score = Ext.ToIntOrNull(dr["Score"]);
                entity.IsAttend = Ext.ToString(dr["IsAttend"]);
                entity.IsPass = Ext.ToString(dr["IsPass"]);
                entity.Remark = Ext.ToString(dr["Remark"]);
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