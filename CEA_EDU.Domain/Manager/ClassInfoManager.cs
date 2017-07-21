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
    /// 班级信息管理类
    /// </summary>
    public class ClassInfoManager : ManagerBase
    {
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(ClassInfoEntity entity)
        {
            entity.Valid = "T";
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Insert<ClassInfoEntity>(session.Connection, entity, session.Transaction);
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

        public void Update(ClassInfoEntity entity)
        {
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Update<ClassInfoEntity>(session.Connection, entity, session.Transaction);
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

        public List<ClassInfoEntity> GetAll()
        {
            string sql = @"select * from ClassInfo(nolock) where valid = 'T'";
            return Repository.Query<ClassInfoEntity>(sql).ToList();
        }

        public ClassInfoEntity GetClassInfoByID(int classID)
        {
            string sql = @"select * from ClassInfo(nolock) where valid = 'T' and classID=@classID ";
            return Repository.Query<ClassInfoEntity>(sql, new { classID = classID }).FirstOrDefault();
        }

        public ClassInfoEntity GetClassInfoByCode(string code)
        {
            string sql = @"select * from ClassInfo(nolock) where valid = 'T' and code=@code ";
            return Repository.Query<ClassInfoEntity>(sql, new { code = code }).FirstOrDefault();
        }

        public List<ClassInfoEntity> GetClassInfoByName(string name)
        {
            string sql = @"select * from ClassInfo(nolock) where valid = 'T' and name like '%'+ @name + '%' ";
            return Repository.Query<ClassInfoEntity>(sql, new { name = name }).ToList();
        }

        public List<ClassInfoEntity> GetSearch(string keyString, string sort, string order, int offset, int pageSize, out int total)
        {
            int pageCount = 0;
            string querySql = string.Format("select * from ClassInfo(nolock) where valid = 'T'  and (code like '%{0}%' or name like '%{0}%')", keyString);
            DataTable dt = SplitPage.SqlSplitPage(querySql, string.Format("order by {0} {1}", sort, order), null, offset / pageSize, pageSize, out pageCount, out total);

            List<ClassInfoEntity> list = new List<ClassInfoEntity>();
            foreach (DataRow dr in dt.Rows)
            {
                ClassInfoEntity entity = new ClassInfoEntity();

                entity.ClassID = Ext.ToInt(dr["ClassID"]);
                entity.Code = Ext.ToString(dr["Code"]);
                entity.Name = Ext.ToString(dr["Name"]);
                entity.Type = Ext.ToString(dr["Type"]);
                entity.StartTime = Ext.ToDateOrNull(dr["StartTime"]);
                entity.EndTime = Ext.ToDateOrNull(dr["EndTime"]);
                entity.TeacherID = Ext.ToIntOrNull(dr["TeacherID"]);
                entity.Company = Ext.ToString(dr["Company"]);
                entity.Department = Ext.ToString(dr["Department"]);
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