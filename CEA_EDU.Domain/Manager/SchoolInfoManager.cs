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
    /// 学校信息管理类
    /// </summary>
    public class SchoolInfoManager : ManagerBase
    {
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(SchoolInfoEntity entity)
        {
            entity.Valid = "T";
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Insert<SchoolInfoEntity>(session.Connection, entity, session.Transaction);
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

        public void Update(SchoolInfoEntity entity)
        {
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Update<SchoolInfoEntity>(session.Connection, entity, session.Transaction);
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

        public SchoolInfoEntity GetSchoolInfoByID(int id)
        {
            string sql = @"select * from SchoolInfo(nolock) where valid = 'T' and id=@id ";
            return Repository.Query<SchoolInfoEntity>(sql, new { id = id }).FirstOrDefault();
        }

        public SchoolInfoEntity GetSchoolInfoByCode(string code)
        {
            string sql = @"select * from SchoolInfo(nolock) where valid = 'T' and code=@code ";
            return Repository.Query<SchoolInfoEntity>(sql, new { code = code }).FirstOrDefault();
        }

        public List<SchoolInfoEntity> GetSchoolInfoByName(string name)
        {
            string sql = @"select * from SchoolInfo(nolock) where valid = 'T' and name like '%'+ @name + '%' ";
            return Repository.Query<SchoolInfoEntity>(sql, new { name = name }).ToList();
        }

        public List<SchoolInfoEntity> GetSearch(string keyString, string sort, string order, int offset, int pageSize, out int total)
        {
            int pageCount = 0;
            string querySql = string.Format("select * from SchoolInfo(nolock) where valid = 'T'  and (code like '%{0}%' or name like '%{0}%')", keyString);
            DataTable dt = SplitPage.SqlSplitPage(querySql, string.Format("order by {0} {1}", sort, order), null, offset / pageSize, pageSize, out pageCount, out total);

            List<SchoolInfoEntity> list = new List<SchoolInfoEntity>();
            foreach (DataRow dr in dt.Rows)
            {
                SchoolInfoEntity entity = new SchoolInfoEntity();

                entity.ID = Ext.ToInt(dr["ID"]);
                entity.Code = Ext.ToString(dr["Code"]);
                entity.Name = Ext.ToString(dr["Name"]);
                entity.Address = Ext.ToString(dr["Address"]);
                entity.Description = Ext.ToString(dr["Description"]);
                entity.Remark = Ext.ToString(dr["Remark"]);
                entity.Company = Ext.ToString(dr["Company"]);
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