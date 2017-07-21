using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CEA_EDU.Domain
{
    /// <summary>
    /// 分页方法
    /// </summary>
    public class SplitPage
    {
        /// <summary>
        /// 分页SQL调用
        /// </summary>
        /// <param name="strConnection">连接串</param>
        /// <param name="select">查询语句</param>
        /// <param name="orderby">排序语句</param>
        /// <param name="listPara">参数</param>
        /// <param name="pageIndex">当前页号</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public static DataTable SqlSplitPage(string select, string orderby, IList<SqlParameter> listPara, int pageIndex, int pageSize, out int pageCount, out int recordCount)
        {
            select = select.Trim();
            if (select.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                select = select.Substring(6);
            if (string.IsNullOrWhiteSpace(orderby))
                orderby = "ORDER BY (SELECT 1)";
            if (pageIndex < 1)
                pageIndex = 1;
            if (listPara == null)
                listPara = new List<SqlParameter>();
            int startRowNo = ((pageIndex - 1) * pageSize) + 1;
            int endRowNo = pageIndex * pageSize;
            string sql = @"SELECT COUNT(1) FROM (SELECT {0}) A
            SELECT * FROM (SELECT ROW_NUMBER() OVER({1}) AS rowNO,{0}) B WHERE rowNo BETWEEN @startRowNo AND @endRowNo";
            sql = string.Format(sql, select, orderby);
            listPara.Add(new SqlParameter("@StartRowNo", startRowNo));
            listPara.Add(new SqlParameter("@EndRowNo", endRowNo));

            DataSet ds = DbHelperSQL.Query(sql, listPara.ToArray());

            recordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            pageCount = (int)Math.Ceiling(recordCount * 1.0 / pageSize);
            return ds.Tables[1];
            
        }
    }

}
