using Community.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IRepository
{
    public interface ICommissionRepository
    {
        #region //获取佣金流水显示分页
        /// <summary>
        /// 获取佣金流水显示分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<Commission> GetCommissions(int page, int limit, out int totalCount,int orderStatus, int commTypeID,string begin,string end);
        #endregion    
        #region //获取佣金流水类型数据
        /// <summary>
        /// 获取佣金流水类型数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<CommissionType> GetCommissionTypes();
        #endregion
    }
}
