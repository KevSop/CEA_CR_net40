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
    /// 公司信息管理类
    /// </summary>
    public class CompanyInfoManager : ManagerBase
    {
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(CompanyInfoEntity entity)
        {
            entity.Valid = "T";
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Insert<CompanyInfoEntity>(session.Connection, entity, session.Transaction);
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

        public void Update(CompanyInfoEntity entity)
        {
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Update<CompanyInfoEntity>(session.Connection, entity, session.Transaction);
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

        public CompanyInfoEntity GetCompanyInfoByID(int id)
        {
            string sql = @"select * from CompanyInfo(nolock) where valid = 'T' and id=@id ";
            return Repository.Query<CompanyInfoEntity>(sql, new { id = id }).FirstOrDefault();
        }

        public CompanyInfoEntity GetCompanyInfoByCode(string code)
        {
            string sql = @"select * from CompanyInfo(nolock) where valid = 'T' and code=@code ";
            return Repository.Query<CompanyInfoEntity>(sql, new { code = code }).FirstOrDefault();
        }

        public List<CompanyInfoEntity> GetCompanyInfoByName(string name)
        {
            string sql = @"select * from CompanyInfo(nolock) where valid = 'T' and name like '%'+ @name + '%' ";
            return Repository.Query<CompanyInfoEntity>(sql, new { name = name }).ToList();
        }

        public List<CompanyInfoEntity> GetSearch(string keyString, string sort, string order, int offset, int pageSize, out int total)
        {
            int pageCount = 0;
            string querySql = string.Format("select * from CompanyInfo(nolock) where valid = 'T'  and (code like '%{0}%' or name like '%{0}%')", keyString);
            DataTable dt = SplitPage.SqlSplitPage(querySql, string.Format("order by {0} {1}", sort, order), null, offset / pageSize, pageSize, out pageCount, out total);

            List<CompanyInfoEntity> list = new List<CompanyInfoEntity>();
            foreach (DataRow dr in dt.Rows)
            {
                CompanyInfoEntity entity = new CompanyInfoEntity();

                entity.ID = Ext.ToInt(dr["ID"]);
                entity.Code = Ext.ToString(dr["Code"]);
                entity.Name = Ext.ToString(dr["Name"]);
                entity.Address = Ext.ToString(dr["Address"]);
                entity.Description = Ext.ToString(dr["Description"]);
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