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
    /// 班级学生信息管理类
    /// </summary>
    public class ClassStudentMapManager : ManagerBase
    {
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(ClassStudentMapEntity entity)
        {
            entity.Valid = "T";
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Insert<ClassStudentMapEntity>(session.Connection, entity, session.Transaction);
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

        public void Update(ClassStudentMapEntity entity)
        {
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Update<ClassStudentMapEntity>(session.Connection, entity, session.Transaction);
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

        public ClassStudentMapEntity GetClassStudentMapByID(int id)
        {
            string sql = @"select * from ClassStudentMap(nolock) where valid = 'T' and id=@id ";
            return Repository.Query<ClassStudentMapEntity>(sql, new { id = id }).FirstOrDefault();
        }

        public ClassStudentMapEntity GetClassStudentMapByKeys(int classID, int studentID)
        {
            string sql = @"select * from ClassStudentMap(nolock) where classID=@classID and studentID = @studentID";
            return Repository.Query<ClassStudentMapEntity>(sql, new { classID = classID, studentID = studentID }).FirstOrDefault();
        }

        public List<ClassStudentMapEntity> GetClassStudentMapByClassID(int classID)
        {
            string sql = @"select * from ClassStudentMap(nolock) where valid = 'T' and classID=@classID ";
            return Repository.Query<ClassStudentMapEntity>(sql, new { classID = classID }).ToList();
        }

        public List<ClassStudentMapEntity> GetClassStudentMapByStudentID(int studentID)
        {
            string sql = @"select * from ClassStudentMap(nolock) where valid = 'T' and studentID=@studentID ";
            return Repository.Query<ClassStudentMapEntity>(sql, new { studentID = studentID }).ToList();
        }

        public List<ClassStudentMapViewEntity> GetClassStudentMapViewList(int? classID, int? studentID, string studentSearchKey = null)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"select cs.ID, c.ClassID, c.Code as 'ClassCode', c.Name as 'ClassName',
                            u.ID as 'StudentID', u.Code as 'StudentCode', u.Name as 'StudentName'
                           from ClassStudentMap(nolock) cs
                               inner join UserInfo(nolock) u on u.ID = cs.StudentID and u.valid = 'T' 
                               inner join ClassInfo(nolock) c on c.ClassID = cs.ClassID and c.valid = 'T' 
                           where cs.valid = 'T'");

            List<SqlParameter> paraList = new List<SqlParameter>();

            if (classID != null && classID > 0)
            {
                sql.Append(" and cs.ClassID = @ClassID");

                SqlParameter para = new SqlParameter("@ClassID", SqlDbType.Int);
                para.SqlValue = classID;
                paraList.Add(para);
            }

            if (studentID != null && studentID > 0)
            {
                sql.Append(" and a.StudentID = @StudentID");

                SqlParameter para = new SqlParameter("@StudentID", SqlDbType.Int);
                para.SqlValue = studentID;
                paraList.Add(para);
            }

            if(!string.IsNullOrWhiteSpace(studentSearchKey))
            {
                 sql.Append(" and (u.code like '%'+ @studentSearchKey + '%' or u.name like '%'+ @studentSearchKey + '%')");

                SqlParameter para = new SqlParameter("@studentSearchKey", SqlDbType.NVarChar);
                para.SqlValue = studentSearchKey;
                paraList.Add(para);
            }

            DataSet ds = DbHelperSQL.Query(sql.ToString(), paraList.ToArray());

            List<ClassStudentMapViewEntity> list = new List<ClassStudentMapViewEntity>();

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ClassStudentMapViewEntity entity = new ClassStudentMapViewEntity();

                    entity.ID = Ext.ToInt(dr["ID"]);
                    entity.ClassID = Ext.ToInt(dr["ClassID"]);
                    entity.ClassCode = Ext.ToString(dr["ClassCode"]);
                    entity.ClassName = Ext.ToString(dr["ClassName"]);
                    entity.StudentID = Ext.ToInt(dr["StudentID"]);
                    entity.StudentCode = Ext.ToString(dr["StudentCode"]);
                    entity.StudentName = Ext.ToString(dr["StudentName"]);
    
                    list.Add(entity);
                }
            }

            return list;
        }

        public List<ClassStudentMapViewEntity> GetSearch(int? classID, string keyString, string order, int offset, int pageSize, out int total)
        {
            int pageCount = 0;
            string querySql = string.Format(@"select cs.ID, c.ClassID, c.Code as 'ClassCode', c.Name as 'ClassName',
                                                u.ID as 'StudentID', u.Code as 'StudentCode', u.Name as 'StudentName'
                                              from ClassStudentMap(nolock) cs
                                                   inner join UserInfo(nolock) u on u.ID = cs.StudentID and u.valid = 'T' 
                                                   inner join ClassInfo(nolock) c on c.ClassID = cs.ClassID and c.valid = 'T' 
                                              where cs.valid = 'T' {1} and (c.Code like '%{0}%' or c.Name like '%{0}%'
                                                    or u.Code like '%{0}%' or u.Name like '%{0}%') ", 
                                              keyString, classID == null ? "" : " and cs.ClassID = " + classID);

            DataTable dt = SplitPage.SqlSplitPage(querySql, string.Format("order by cs.ClassID {0}", order), null, offset / pageSize, pageSize, out pageCount, out total);

            List<ClassStudentMapViewEntity> list = new List<ClassStudentMapViewEntity>();
            foreach (DataRow dr in dt.Rows)
            {
                ClassStudentMapViewEntity entity = new ClassStudentMapViewEntity();

                entity.ID = Ext.ToInt(dr["ID"]);
                entity.ClassID = Ext.ToInt(dr["ClassID"]);
                entity.ClassCode = Ext.ToString(dr["ClassCode"]);
                entity.ClassName = Ext.ToString(dr["ClassName"]);
                entity.StudentID = Ext.ToInt(dr["StudentID"]);
                entity.StudentCode = Ext.ToString(dr["StudentCode"]);
                entity.StudentName = Ext.ToString(dr["StudentName"]);

                list.Add(entity);
            }

            return list;

        }
    }
}