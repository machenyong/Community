using Community.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IRepository
{
    public interface ISalespersonRepository
    {
        #region ///绑定核销员
        /// <summary>
        /// 新增核销员
        /// </summary>
        /// <param name="salesperson"></param>
        /// <returns></returns>
        int AddSalesperson(Salesperson salesperson);
        #endregion

        #region ///查看当前团长下的核销员
        /// <summary>
        /// 查看当前团长下的核销员
        /// </summary>
        /// <param name="colonelId"></param>
        /// <returns></returns>
        List<Salesperson> ShowSalespersonByColonelId(int colonelId);

        #endregion

        #region ///删除当前团长下的核销员
        /// <summary>
        /// 删除当前团长下的核销员
        /// </summary>
        /// <param name="salespersonID"></param>
        /// <returns></returns>
        int DeleteSalespersonBySalespersonID(int salespersonID);
        
        #endregion
    }
}
