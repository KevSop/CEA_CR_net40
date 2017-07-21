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
    /// 字典管理类
    /// </summary>
    public class SysDicManager : ManagerBase
    {
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(SysDicEntity entity)
        {
            entity.Valid = "T";
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Insert<SysDicEntity>(session.Connection, entity, session.Transaction);
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

        public void Update(SysDicEntity entity)
        {
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Update<SysDicEntity>(session.Connection, entity, session.Transaction);
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

        public SysDicEntity GetDicByID(int id)
        {
            string sql = @"select * from SysDic(nolock) where valid = 'T' and id=@id ";
            return Repository.Query<SysDicEntity>(sql, new { id = id }).FirstOrDefault();
        }

        public SysDicEntity GetDicByCode(string code)
        {
            string sql = @"select * from SysDic(nolock) where valid = 'T' and code=@code ";
            return Repository.Query<SysDicEntity>(sql, new { code = code }).FirstOrDefault();
        }

        public List<SysDicEntity> GetDicByParentCode(string parentCode)
        {
            string sql = @"select * from SysDic(nolock) where valid = 'T' and ParentCode=@ParentCode ";
            return Repository.Query<SysDicEntity>(sql, new { ParentCode = parentCode }).ToList();
        }

        public List<SysDicEntity> GetDicByName(string name)
        {
            string sql = @"select * from SysDic(nolock) where valid = 'T' and name like '%'+ @name + '%' ";
            return Repository.Query<SysDicEntity>(sql, new { name = name }).ToList();
        }

        public List<SysDicEntity> GetDicByType(string type)
        {
            string sql = @"select * from SysDic(nolock) where valid = 'T' and type=@type";
            return Repository.Query<SysDicEntity>(sql, new { type = type }).ToList();
        }

        public List<SysDicEntity> GetSearch(string keyString, string sort, string order, int offset, int pageSize, out int total)
        {
            int pageCount = 0;
            string querySql = string.Format("select * from SysDic(nolock) where valid = 'T'  and (code like '%{0}%' or name like '%{0}%') ", keyString);
            DataTable dt = SplitPage.SqlSplitPage(querySql, string.Format("order by {0} {1}", sort, order), null, offset / pageSize, pageSize, out pageCount, out total);

            List<SysDicEntity> list = new List<SysDicEntity>();
            foreach (DataRow dr in dt.Rows)
            {
                SysDicEntity entity = new SysDicEntity();

                entity.ID = Ext.ToInt(dr["ID"]);
                entity.Code = Ext.ToString(dr["Code"]);
                entity.Name = Ext.ToString(dr["Name"]);
                entity.ParentCode = Ext.ToString(dr["ParentCode"]);
                entity.Type = Ext.ToString(dr["Type"]);
                entity.Value = Ext.ToString(dr["Value"]);
                entity.SortValue = Ext.ToInt(dr["SortValue"]);
                entity.Description = Ext.ToString(dr["Description"]);
                entity.IsDisplay = Ext.ToString(dr["IsDisplay"]);
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