using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Model;
namespace Community.IRepository
{
    public interface IColonelRepository
    {

        /// <summary>
        /// 获取团长数据、查询
        /// </summary>
        /// <param name="colonelName"></param>
        /// <returns></returns>
        List<Colonel> GetColonelDataBySearch(int page, int limit, out int total, string colonelName, string status, string start, string end);
        #region//通过Id删除团长信息
        /// <summary>
        /// 通过Id删除团长信息
        /// </summary>
        /// <param name="colonelId"></param>
        /// <returns></returns>
        int DeleteColonelById(int colonelId);
        #endregion 

        #region//通过Id编辑团长信息
        /// <summary>
        /// 通过Id编辑团长信息
        /// </summary>
        /// <param name="colonelId"></param>
        /// <returns></returns>
        int EditColonelById(Colonel colonel);
        #endregion

        #region ///审核通过
        /// <summary>
        /// 审核状态通过
        /// </summary>
        /// <param name="colonelId"></param>
        /// <returns></returns>
        int CheckColonelStatusOK(int colonelId);

        #endregion

        #region ///审核不通过
        /// <summary>
        /// 审核状态不通过
        /// </summary>
        /// <param name="colonelId"></param>
        /// <returns></returns>
        int CheckColonelStatusNO(int colonelId);

        #endregion
        #region ///路线显示
        /// <summary>
        /// 审核状态不通过
        /// </summary>
        /// <param name="colonelId"></param>
        /// <returns></returns>
        List<WarehouseModel> ShowColonelLine(int page, int limit, out int totalCount, string wareName);
        #endregion

        #region ///绑定商品
        /// <summary>
        /// 绑定商品
        /// </summary>
        /// <param name="colonelId"></param>
        /// <returns></returns>
        int BindGoods(int colonelId, string goodsNumber);
        #endregion
    }
}
