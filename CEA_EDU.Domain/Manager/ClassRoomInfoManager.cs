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
    /// 教室信息管理类
    /// </summary>
    public class ClassRoomInfoManager : ManagerBase
    {
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(ClassRoomInfoEntity entity)
        {
            entity.Valid = "T";
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Insert<ClassRoomInfoEntity>(session.Connection, entity, session.Transaction);
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

        public void Update(ClassRoomInfoEntity entity)
        {
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Update<ClassRoomInfoEntity>(session.Connection, entity, session.Transaction);
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

        public List<ClassRoomInfoEntity> GetAll()
        {
            string sql = @"select * from ClassRoomInfo(nolock) where valid = 'T'";
            return Repository.Query<ClassRoomInfoEntity>(sql).ToList();
        }

        public ClassRoomInfoEntity GetClassRoomInfoByID(int classRoomID)
        {
            string sql = @"select * from ClassRoomInfo(nolock) where valid = 'T' and classRoomID=@classRoomID ";
            return Repository.Query<ClassRoomInfoEntity>(sql, new { classRoomID = classRoomID }).FirstOrDefault();
        }

        public ClassRoomInfoEntity GetClassRoomInfoByCode(string code)
        {
            string sql = @"select * from ClassRoomInfo(nolock) where valid = 'T' and code=@code ";
            return Repository.Query<ClassRoomInfoEntity>(sql, new { code = code }).FirstOrDefault();
        }

        public List<ClassRoomInfoEntity> GetClassRoomInfoByName(string name)
        {
            string sql = @"select * from ClassRoomInfo(nolock) where valid = 'T' and name like '%'+ @name + '%' ";
            return Repository.Query<ClassRoomInfoEntity>(sql, new { name = name }).ToList();
        }

        public List<ClassRoomInfoEntity> GetSearch(string keyString, string sort, string order, int offset, int pageSize, out int total)
        {
            int pageCount = 0;
            string querySql = string.Format("select * from ClassRoomInfo(nolock) where valid = 'T'  and (code like '%{0}%' or name like '%{0}%')", keyString);
            DataTable dt = SplitPage.SqlSplitPage(querySql, string.Format("order by {0} {1}", sort, order), null, offset / pageSize, pageSize, out pageCount, out total);

            List<ClassRoomInfoEntity> list = new List<ClassRoomInfoEntity>();
            foreach (DataRow dr in dt.Rows)
            {
                ClassRoomInfoEntity entity = new ClassRoomInfoEntity();

                entity.ClassRoomID = Ext.ToInt(dr["ClassRoomID"]);
                entity.Code = Ext.ToString(dr["Code"]);
                entity.Name = Ext.ToString(dr["Name"]);
                entity.Address = Ext.ToString(dr["Address"]);
                entity.Type = Ext.ToString(dr["Type"]);
                entity.SeatNum = Ext.ToIntOrNull(dr["SeatNum"]);
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