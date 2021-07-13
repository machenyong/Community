using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Model;
using System.Collections.Generic;
namespace Community.IRepository
{
    public interface IWareHouseRepository
    {
        #region 仓库管理
        /// <summary>
        /// 获取仓库数据、查询
        /// </summary>
        /// <param name="colonelName"></param>
        /// <returns></returns>
        List<WarehouseModel> GetWarehouseModels(int page, int limit, out int totalCount);

        /// <summary>
        /// 添加仓库信息
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        int AddWareHouseInfo(WarehouseModel m);

        /// <summary>
        /// 删除仓库信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteInfoShow(int id);

        /// <summary>
        /// 修改仓库信息
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        int UpdateInfoShow(WarehouseModel m);

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        int EditStatus(int status, int id);
        #endregion

        #region 配送小区
        /// <summary>
        /// /配送小区
        /// </summary>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<DistributionPlot> GetDistributionPlots(int page, int limit, out int totalCount, int wId = 0);
        /// <summary>
        /// 搜索仓库，模糊搜索：查找仓库下所有小区；
        /// </summary>
        List<WarehouseModel> WareHouseType();
        #endregion

        #region 入库管理
        /// <summary>
        /// 入库管理
        /// </summary>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<OutInWarehouse> GetOutInWarehouses(int page, int limit, out int totalCount, string name = "", string number = "", string date = "", string end = "");
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        int  EnterAdd(OutInWarehouse m);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="outIn"></param>
        /// <returns></returns>
        int EnterEdit(WarehouseModel outIn);
        /// <summary>
        /// 删除入库信息
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        int EnterDelete(int id);
        #endregion

        #region 盘点库存
        /// <summary>
        /// 盘点管理
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <param name="name"></param>
        /// <param name="number"></param>
        /// <param name="date"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        List<TakeStockModel> GetCheckSheets(int page, int limit, out int totalCount, string name = "", string number = "", string date = "", string end = "");
        #endregion

        #region 现有库存
        /// <summary>
        /// 现有库存
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <param name="wareName"></param>
        /// <param name="goodsName"></param>
        /// <param name="goodId"></param>
        /// <param name="goodNo"></param>
        /// <returns></returns>
        List<Stock> NowRepertory(int page, int limit, out int totalCount, string wareName = "", string goodsName = "", int goodId = 0, string goodNo = "");
        #endregion
    }
}
