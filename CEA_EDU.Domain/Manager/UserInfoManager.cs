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
    /// 用户管理类
    /// </summary>
    public class UserInfoManager : ManagerBase
    {
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(UserInfoEntity entity)
        {
            entity.Valid = "T";
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Insert<UserInfoEntity>(session.Connection, entity, session.Transaction);
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

        public void Update(UserInfoEntity entity)
        {
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Update<UserInfoEntity>(session.Connection, entity, session.Transaction);
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

        public UserInfoEntity GetUserByID(int id)
        {
            string sql = @"select * from UserInfo(nolock) where valid = 'T' and  id = @id";
            return Repository.Query<UserInfoEntity>(sql, new { id = id }).FirstOrDefault();
        }

        public UserInfoEntity GetUserByCode(string code)
        {
            string sql = @"select * from UserInfo(nolock) where valid = 'T' and  code = @code";
            return Repository.Query<UserInfoEntity>(sql, new { code = code }).FirstOrDefault();
        }

        public List<UserInfoEntity> GetUserByName(string name)
        {
            string sql = @"select * from UserInfo(nolock) where valid = 'T' and  name = @name";
            return Repository.Query<UserInfoEntity>(sql, new { name = name }).ToList();
        }

        public List<UserInfoEntity> GetUserByType(string type)
        {
            string sql = @"select * from UserInfo(nolock) where valid = 'T' and  type = @type";
            return Repository.Query<UserInfoEntity>(sql, new { type = type }).ToList();
        }

        public List<UserInfoEntity> GetSearch(string keyString, string type, string sort, string order, int offset, int pageSize, out int total)
        {
            int pageCount = 0;
            string querySql = string.Format("select * from UserInfo(nolock) where valid = 'T' {1} and (code like '%{0}%' or name like '%{0}%') ",
                keyString, string.IsNullOrWhiteSpace(type) ? "" : string.Format(" and Type = '{0}'", type));
            DataTable dt = SplitPage.SqlSplitPage(querySql, string.Format("order by {0} {1}", sort, order), null, offset / pageSize + 1, pageSize, out pageCount, out total);
            List<UserInfoEntity> list = new List<UserInfoEntity>();
            foreach (DataRow dr in dt.Rows)
            {
                UserInfoEntity entity = new UserInfoEntity();

                entity.ID = Ext.ToInt(dr["id"]);
                entity.Code = Ext.ToString(dr["code"]);
                entity.Name = Ext.ToString(dr["name"]);
                entity.Password = Ext.ToString(dr["password"]);
                entity.Type = Ext.ToString(dr["type"]);
                entity.Group = Ext.ToString(dr["group"]);
                entity.Company = Ext.ToString(dr["company"]);
                entity.Department = Ext.ToString(dr["department"]);
                entity.PositionID = Ext.ToString(dr["positionID"]);
                entity.PositionName = Ext.ToString(dr["positionName"]);
                entity.Sex = Ext.ToString(dr["sex"]);
                entity.Birthday = Ext.ToDate(dr["birthday"]);
                entity.Email = Ext.ToString(dr["email"]);
                entity.Phone = Ext.ToString(dr["phone"]);
                entity.Address = Ext.ToString(dr["address"]);
                entity.Valid = Ext.ToString(dr["valid"]);
                entity.CreateTime = Ext.ToDate(dr["createTime"]);
                entity.CreateBy = Ext.ToString(dr["createBy"]);
                entity.UpdateTime = Ext.ToDate(dr["updateTime"]);
                entity.UpdateBy = Ext.ToString(dr["updateBy"]);

                list.Add(entity);
            }

            return list;
        }
    }
}