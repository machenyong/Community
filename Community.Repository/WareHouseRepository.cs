using System;
using Community.IRepository;
using System.Collections.Generic;
using Community.Common;
using Community.Model;
using System.Text;
using System.Linq;

namespace Community.Repository
{
    public class WareHouseRepository : IWareHouseRepository
    {
        DbFactory dbFactory = new DbFactory();

        #region 仓库管理
        /// <summary>
        /// 显示所有仓库信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <param name="wareName"></param>
        /// <returns></returns>
        public List<WarehouseModel> GetWarehouseModels(int page, int limit, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select WareId,WareName,WareAddress,WareLeft,WareRight,WareCount,WareStatus from Warehouse where 1=1");
            var data = dbFactory.DbHelper().Query<WarehouseModel>(strSql.ToString(), null);
            totalCount = data.Count();//总数量
            return data.Skip((page - 1) * limit)
                .Take(limit)
                .ToList();
        }

        /// <summary>
        /// 添加仓库信息
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>        
        public int AddWareHouseInfo(WarehouseModel m)
        {
            StringBuilder strSql = new StringBuilder();
            m.WareCount = 0;
            strSql.Append("insert into Warehouse values(@wareName,@address,@left,@rigth,@count,@status,@number,@linkName,@phone);");
            var data = dbFactory.DbHelper().Execute(strSql.ToString(), new { @wareName = m.WareName, @address = m.WareAddress, @left = m.WareLeft, @rigth = m.WareRight, @count = m.WareCount, @status = m.WareStatus, @number = m.ColonelId, @linkName = m.LinkName, @phone = m.LinkPhone });
            return data;
        }
        /// <summary>
        /// 删除仓库信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteInfoShow(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Warehouse where WareId=@wareId");
            var data = dbFactory.DbHelper().Execute(strSql.ToString(), new { @wareId = id });
            return data;
        }
        /// <summary>
        /// 修改仓库信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateInfoShow(WarehouseModel m)
        {
            DbContextHelper dbContext = new DbContextHelper();
            dbContext.Entry(m).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return dbContext.SaveChanges();
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int EditStatus(int status, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Warehouse set WareStatus=@status where WareId=@wareId");
            var data = dbFactory.DbHelper().Execute(strSql.ToString(), new { @status = status, @wareId = id });
            return data;
        }

        #endregion

        #region 配送小区
        /// <summary>
        /// 配送小区
        /// </summary>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<DistributionPlot> GetDistributionPlots(int page, int limit, out int totalCount, int wId = 0)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PlotId,PlotName,WareName,Plot.WareId, Colonel.ColonelName,Colonel.ColonelPhone,PathNames,Warehouse.WareAddress,WareLeft,WareRight from Warehouse join Plot on Warehouse.WareId=Plot.WareId join Colonel on Colonel.ColonelId=Plot.ColonelId where 1=1");
            if (wId != 0) 
            {
                strSql.Append(" and Plot.WareId=@id");
            }
            var data = dbFactory.DbHelper().Query<DistributionPlot>(strSql.ToString(), new { @id = wId });
            totalCount = data.Count();//总数量
            return data.Skip((page - 1) * limit)
                .Take(limit)
                .ToList();
        }

        /// <summary>
        /// 搜索仓库，模糊搜索：查找仓库下所有小区；
        /// </summary>
        /// <returns></returns>
        public List<WarehouseModel> WareHouseType()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select WareId,WareName from Warehouse");
            var data = dbFactory.DbHelper().Query<WarehouseModel>(strSql.ToString(),null);
            return data;
        }

        #endregion

        #region 入库管理
        /// <summary>
        /// 入库管理
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <param name="name"></param>
        /// <param name="number"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<OutInWarehouse> GetOutInWarehouses(int page, int limit, out int totalCount, string name = "", string number = "", string begin = "", string end = "")
        {
            StringBuilder strSql = new StringBuilder();
            List<string> data = new List<string>();
           
            strSql.Append("select OIId,Odd,GoodsImg,GoodsName,GoodsSpecification,GoodsNumber,OutInWarehouse.WareId,InWareNumber,Quantity,CreateDate,Warehouse.WareName,Plot.PlotName,Warehouse.WareAddress,Warehouse.WareLeft,Warehouse.WareRight from OutInWarehouse join Warehouse on Warehouse.WareId=OutInWarehouse.WareId join Plot on  Plot.WareId=Warehouse.WareId where 1=1 ");
            if (!string.IsNullOrEmpty(name))
            {
                strSql.Append(" and WareName like concat('%',@name,'%')");
            }
            if (!string.IsNullOrEmpty(number))
            {
                strSql.Append(" and Odd like concat('%',@add,'%')");
            }
            var res = dbFactory.DbHelper().Query<OutInWarehouse>(strSql.ToString(), new { @name = name, @add = number });

            if (begin != "null" && end != "null")
            {
                if (!string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                {
                    res = res.Where(p => p.CreateDate >= Convert.ToDateTime(begin) && p.CreateDate <= Convert.ToDateTime(end)).ToList();
                }
            }
            totalCount = res.Count();//总数量
            return res.Skip((page - 1) * limit)//跳过几条
                .Take(limit)     //一页几条
                .OrderByDescending(p=>p.CreateDate)//倒叙排序
                .ToList();
        }
        /// <summary>
        /// 添加入库管理
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public int EnterAdd(OutInWarehouse m)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OutInWarehouse values(@add,@img,@name,@spe,@number,@id,@inware,@quantity,@date);");
            var data = dbFactory.DbHelper().Execute(strSql.ToString(), new { @add=m.Odd, @img=m.GoodsImg, @name=m.GoodsName, @spe=m.GoodsSpecification, @number=m.GoodsNumber, @id=m.WareId, @inware=m.InWareNumber, @quantity=m.Quantity, @date=m.CreateDate });
            return data;
        }

        /// <summary>
        /// 修改入库管理
        /// </summary>
        /// <returns></returns>
        public int EnterEdit(WarehouseModel outIn)
        {
            Dictionary<string, object> key = new Dictionary<string, object>();
            key.Add("update Warehouse set WareAddress=@address,WareLeft=@left,WareRight=@right where WareId=@id",new { @address =outIn.WareAddress, @left =outIn.WareLeft, @right =outIn.WareRight,@id=outIn.WareId});
            key.Add("update Plot set PlotName=@name where PlotId=@pid",new { @name =outIn.PlotName,@pid=outIn.PlotId});
            return dbFactory.DbHelper().ExecuteTransaction(key);//调用事务
        }

        /// <summary>
        /// 删除入库管理
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int EnterDelete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OutInWarehouse where OIId=@wareId");
            var data = dbFactory.DbHelper().Execute(strSql.ToString(), new { @wareId = id });
            return data;
        }
        #endregion

        #region 盘点库存
        /// <summary>
        /// 盘点仓库
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <param name="name"></param>
        /// <param name="number"></param>
        /// <param name="date"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<TakeStockModel> GetCheckSheets(int page, int limit, out int totalCount, string name = "", string number = "", string begin = "", string end = "")
        {
            StringBuilder strSql = new StringBuilder();
            List<string> data = new List<string>();

            strSql.Append("select  CheckId,CheckCode,WareName,CheckNum,CheckAfterNum,CheckPeople,CheckTime,Goods.GoodsID,Goods.GoodsImg,Goods.GoodsName,GoodsNo,OutInWarehouse.GoodsSpecification,GoodsStock,StockNum,CheckRemark from Warehouse");
            strSql.Append(" join Goods on Warehouse.WareId=Goods.WId");
            strSql.Append(" join Stock on Stock.GoodsID=Goods.GoodsId");
            strSql.Append(" join OutInWarehouse on OutInWarehouse.WareId=Warehouse.WareId");
            strSql.Append(" join CheckSheet on CheckSheet.WId=Warehouse.WareId");
            if (!string.IsNullOrEmpty(name))
            {
                strSql.Append(" and WareName like concat('%',@name,'%')");
            }
            if (!string.IsNullOrEmpty(number))
            {
                strSql.Append(" and CheckCode like concat('%',@add,'%')");
            }
            var res = dbFactory.DbHelper().Query<TakeStockModel>(strSql.ToString(), new { @name = name, @add = number });

            if (begin != "null" && end != "null")
            {
                if (!string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                {
                    res = res.Where(p => p.CheckTime >= Convert.ToDateTime(begin) && p.CheckTime <= Convert.ToDateTime(end)).ToList();
                }
            }
            totalCount = res.Count();//总数量
            return res.Skip((page - 1) * limit)//跳过几条
                .Take(limit)     //一页几条
                .OrderByDescending(p => p.CheckTime)//倒叙排序
                .ToList();
        }
        #endregion
        #region 现有库存
        /// <summary>
        /// //现有库存
        /// </summary>
        public List<Stock> NowRepertory(int page, int limit, out int totalCount, string wareName = "", string goodsName = "", int goodId = 0, string goodNo = "")
        {
            StringBuilder strSql = new StringBuilder();
            List<string> data = new List<string>();

            strSql.Append("select Goods.GoodsId,GoodsImg,GoodsName,GoodsNo,WareName,InRepertory,OutRepertory,StockNum,CreateTime  from Goods join Stock on Stock.GoodsID=Goods.GoodsId join Warehouse on Warehouse.WareId=Stock.WareID where 1=1");
            if (!string.IsNullOrEmpty(wareName))
            {
                strSql.Append(" and WareName like concat('%',@name,'%')");
            }
            if (!string.IsNullOrEmpty(goodsName))
            {
                strSql.Append(" and GoodsName like concat('%',@add,'%')");
            }
            var res = dbFactory.DbHelper().Query<Stock>(strSql.ToString(), new { @name = wareName, @add = goodsName });

            if (goodId != 0  || !string.IsNullOrEmpty(goodNo))
            {
       
               res = res.Where(p => p.GoodsId.Equals(goodId) || p.GoodsNo.Contains(goodNo)).ToList();
              
            }
            totalCount = res.Count();//总数量
            return res.Skip((page - 1) * limit)//跳过几条
                .Take(limit)     //一页几条
                .OrderByDescending(p => p.CreateTime)//倒叙排序
                .ToList();
        }
        #endregion
    }
}
