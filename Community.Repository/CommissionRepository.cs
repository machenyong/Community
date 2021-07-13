using Community.Common;
using Community.IRepository;
using Community.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Repository
{
    public class CommissionRepository: ICommissionRepository
    {
        DbFactory dbFactory = new DbFactory();
        /// <summary>
        /// 佣金流水显示
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<Commission> GetCommissions(int page,int limit,out int totalCount,int orderStatus, int commTypeID, string begin, string end)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select t.CommTypeName as CommTypeName,* from Commission c join CommissionType t on c.CommissionID=t.CommTypeID");
            var list = dbFactory.DbHelper().Query<Commission>(strSql.ToString()).ToList();
            if (orderStatus!=0)
            {
                list = list.Where(p => p.OrderStatus==orderStatus).ToList();
            }
            if (commTypeID!=0)
            {
                list = list.Where(p=>p.CommTypeID==commTypeID).ToList();
            }
            if (begin != "null" && end != "null")
            {
                if (!string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                {
                    list = list.Where(p => p.StockDate >= DateTime.Parse(begin) && p.StockDate <= DateTime.Parse(end)).ToList();
                }
            }
            totalCount = list.Count;
            return list.Skip((page-1)*limit).Take(limit).ToList();
        }
        /// <summary>
        /// 获取佣金类型数据
        /// </summary>
        /// <returns></returns>
        public List<CommissionType> GetCommissionTypes()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommTypeID,CommTypeName from CommissionType");
            return dbFactory.DbHelper().Query<CommissionType>(strSql.ToString());
        }
    }
}
