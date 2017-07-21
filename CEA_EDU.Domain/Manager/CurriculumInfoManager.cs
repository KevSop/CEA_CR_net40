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
    /// 课程信息管理类
    /// </summary>
    public class CurriculumInfoManager : ManagerBase
    {
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(CurriculumInfoEntity entity)
        {
            entity.Valid = "T";
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Insert<CurriculumInfoEntity>(session.Connection, entity, session.Transaction);
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

        public void Update(CurriculumInfoEntity entity)
        {
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Update<CurriculumInfoEntity>(session.Connection, entity, session.Transaction);
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

        public List<CurriculumInfoEntity> GetAll()
        {
            string sql = @"select * from CurriculumInfo(nolock) where valid = 'T'";
            return Repository.Query<CurriculumInfoEntity>(sql).ToList();
        }

        public CurriculumInfoEntity GetCurriculumInfoByID(int curriculumID)
        {
            string sql = @"select * from CurriculumInfo(nolock) where valid = 'T' and curriculumID=@curriculumID ";
            return Repository.Query<CurriculumInfoEntity>(sql, new { curriculumID = curriculumID }).FirstOrDefault();
        }

        public CurriculumInfoEntity GetCurriculumInfoByCode(string code)
        {
            string sql = @"select * from CurriculumInfo(nolock) where valid = 'T' and code=@code ";
            return Repository.Query<CurriculumInfoEntity>(sql, new { code = code }).FirstOrDefault();
        }

        public List<CurriculumInfoEntity> GetCurriculumInfoByName(string name)
        {
            string sql = @"select * from CurriculumInfo(nolock) where valid = 'T' and name like '%'+ @name + '%' ";
            return Repository.Query<CurriculumInfoEntity>(sql, new { name = name }).ToList();
        }

        public List<CurriculumInfoEntity> GetSearch(string keyString, string sort, string order, int offset, int pageSize, out int total)
        {
            int pageCount = 0;
            string querySql = string.Format("select * from CurriculumInfo(nolock) where valid = 'T'  and (code like '%{0}%' or name like '%{0}%')", keyString);
            DataTable dt = SplitPage.SqlSplitPage(querySql, string.Format("order by {0} {1}", sort, order), null, offset / pageSize, pageSize, out pageCount, out total);

            List<CurriculumInfoEntity> list = new List<CurriculumInfoEntity>();
            foreach (DataRow dr in dt.Rows)
            {
                CurriculumInfoEntity entity = new CurriculumInfoEntity();

                entity.CurriculumID = Ext.ToInt(dr["CurriculumID"]);
                entity.Code = Ext.ToString(dr["Code"]);
                entity.Name = Ext.ToString(dr["Name"]);
                entity.Type = Ext.ToString(dr["Type"]);
                entity.Score = Ext.ToIntOrNull(dr["Score"]);
                entity.StartTime = Ext.ToDateOrNull(dr["StartTime"]);
                entity.EndTime = Ext.ToDateOrNull(dr["EndTime"]);
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