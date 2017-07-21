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
    /// 登录信息管理类
    /// </summary>
    public class LoginLogManager : ManagerBase
    {
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(LoginLogEntity entity)
        {
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Insert<LoginLogEntity>(session.Connection, entity, session.Transaction);
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

        public void Update(LoginLogEntity entity)
        {
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Update<LoginLogEntity>(session.Connection, entity, session.Transaction);
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

        public LoginLogEntity GetLoginLogByID(int id)
        {
            string sql = @"select * from LoginLog(nolock) where id=@id ";
            return Repository.Query<LoginLogEntity>(sql, new { id = id }).FirstOrDefault();
        }

        public LoginLogEntity GetLoginLogByGuid(string guid)
        {
            string sql = @"select * from LoginLog(nolock) where guid=@guid ";
            return Repository.Query<LoginLogEntity>(sql, new { guid = guid }).FirstOrDefault();
        }

        public List<LoginLogEntity> GetLoginLogByLoginID(int loginID)
        {
            string sql = @"select * from LoginLog(nolock) where loginID=@loginID ";
            return Repository.Query<LoginLogEntity>(sql, new { loginID = loginID }).ToList();
        }

        public List<LoginLogEntity> GetLoginLogByLoginName(string loginName)
        {
            string sql = @"select * from LoginLog(nolock) where loginName=@loginName ";
            return Repository.Query<LoginLogEntity>(sql, new { loginName = loginName }).ToList();
        }

        public List<LoginLogEntity> GetLoginLogByType(string type)
        {
            string sql = @"select * from LoginLog(nolock) where type=@type ";
            return Repository.Query<LoginLogEntity>(sql, new { type = type }).ToList();
        }

        public List<LoginLogEntity> GetLoginLogByLoginType(string loginType)
        {
            string sql = @"select * from LoginLog(nolock) where type=@type ";
            return Repository.Query<LoginLogEntity>(sql, new { loginType = loginType }).ToList();
        }

        public List<LoginLogEntity> GetSearch(string whereCondition, string sort, string order, int offset, int pageSize, out int total)
        {
            int pageCount = 0;
            string querySql = string.Format("select * from LoginLog(nolock) {0}", whereCondition);
            DataTable dt = SplitPage.SqlSplitPage(querySql, string.Format("order by {0} {1}", sort, order), null, offset / pageSize, pageSize, out pageCount, out total);

            List<LoginLogEntity> list = new List<LoginLogEntity>();
            foreach (DataRow dr in dt.Rows)
            {
                LoginLogEntity entity = new LoginLogEntity();

                entity.ID = Ext.ToInt(dr["ID"]);
                entity.Guid = Ext.ToString(dr["Guid"]);
                entity.Type = Ext.ToString(dr["Type"]);
                entity.LoginID = Ext.ToInt(dr["LoginName"]);
                entity.LoginName = Ext.ToString(dr["LoginName"]);
                entity.LoginType = Ext.ToString(dr["LoginType"]);
                entity.Action = Ext.ToString(dr["Action"]);
                entity.TimeRecord = Ext.ToDateOrNull(dr["TimeRecord"]);
                entity.Remark = Ext.ToString(dr["Remark"]);
                entity.MachineID = Ext.ToString(dr["MachineID"]);
                entity.LoginIP = Ext.ToString(dr["LoginIP"]);
                entity.LoginIP2 = Ext.ToString(dr["LoginIP2"]);
                entity.StartTime = Ext.ToDateOrNull(dr["StartTime"]);
                entity.EndTime = Ext.ToDateOrNull(dr["EndTime"]);
                entity.CreateTime = Ext.ToDate(dr["CreateTime"]);
                entity.UpdateTime = Ext.ToDate(dr["UpdateTime"]);

                list.Add(entity);
            }

            return list;

        }
    }
}