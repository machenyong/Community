using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Common;
using Community.IRepository;
using Community.Model;

namespace Community.Repository
{
    public class GoodsRepository: IGoodsRepository
    {
        DbFactory dbFactory = new DbFactory();

        /// <summary>
        /// 获取商品数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<Goods> GetGoods(int pageIndex, int limit,int GoodsStatus,int GoodsTypeIdInquire, string GoodsName, out int count)
        {
            string sql = "select GoodsId,GoodsNo,GoodsImg,GoodsName,GoodsPrice,SaleNum,GoodsStock,Sort,[Status],SaleTime,GoodsStatusRecover,GoodsTypeId from Goods where GoodsStatus=@GoodsStatus";
            //调用方法
            List<Goods> goods = dbFactory.DbHelper().Query<Goods>(sql,new { @GoodsStatus= GoodsStatus });
            if (GoodsTypeIdInquire!=0)
            {
                goods = goods.Where(p => p.GoodsTypeId == GoodsTypeIdInquire).ToList();
            }
            if (!string.IsNullOrEmpty(GoodsName))
            {
                goods = goods.Where(p => p.GoodsName.Contains(GoodsName)).ToList();
            }
            //总条数
            count = goods.Count;
            //分页
            goods = goods.Skip((pageIndex - 1) * limit).Take(limit).ToList();
            //返回商品数据
            return goods;
        }
   
        /// <summary>
        /// 加入回收站
        /// </summary>
        /// <param name="GoodsId"></param>
        /// <returns></returns>
        public int recycleGoods(int GoodsId,int GoodsStatus)
        {
            string sql = "update Goods set GoodsStatus=@GoodsStatus where GoodsId=@GoodsId";

            return dbFactory.DbHelper().Execute(sql,new { @GoodsStatus= GoodsStatus, @GoodsId = GoodsId });
        }

        /// <summary>
        /// 商品修改
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public int EditGoods(Goods goods)
        {
            string sql = "update Goods set GoodsName=@GoodsName,GoodsPrice=@GoodsPrice,SaleNum=@SaleNum,GoodsStock=@GoodsStock,Sort=@Sort,SaleTime=@SaleTime where GoodsId=@GoodsId";
            var gather = new {
                @GoodsName=goods.GoodsName,
                @GoodsPrice=goods.GoodsPrice,
                @SaleNum=goods.SaleNum,
                @GoodsStock=goods.GoodsStock,
                @Sort=goods.Sort,
                @SaleTime=goods.SaleTime,
                @GoodsId=goods.GoodsId
            };
            return dbFactory.DbHelper().Execute(sql, gather);
        }


        /// <summary>
        /// 恢复商品
        /// </summary>
        /// <param name="GoodsId"></param>
        /// <param name="GoodsStatusRecover"></param>
        /// <returns></returns>
        public int RecoverGoods(int GoodsId, int GoodsStatusRecover)
        {
            string sql = "update goods set GoodsStatus=@GoodsStatusRecover where GoodsId=@GoodsId";

            return dbFactory.DbHelper().Execute(sql,new { @GoodsStatusRecover= GoodsStatusRecover, @GoodsId= GoodsId });
        }


        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="GoodsId"></param>
        /// <returns></returns>
         public int DelGoods(int GoodsId)
        {
            string sql = "delete from Goods where GoodsId=@GoodsId";
            return dbFactory.DbHelper().Execute(sql,new { @GoodsId= GoodsId });
        }


        /// <summary>
        /// 商品添加
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public int AddGoods(AllGoodsMsg goods)
        {
            #region 随机数
            Random random = new Random();
            string GoodsNo=random.Next(1001,9999).ToString();
            //用来关联运费模板表
            int FreightId =Convert.ToInt32((DateTime.Now).ToString("yyyyMMdd"));
            FreightId = Convert.ToInt32(GoodsNo) + FreightId;
            goods.WId = 1;
            goods.GoodsNo = GoodsNo;
            goods.SaleTime = DateTime.Now;
            goods.CreateTime = DateTime.Now;
            #endregion
            //商品
            string sql = "insert into Goods values(@GoodsNo,@GoodsName,@GoodsPrice,@GoodsIntro,@GoodsStock,@SaleNum,1,1,1,@GoodsDesc,@GoodsImg,@Sort,@SaleTime,@GoodsUnit,@Keyword,@GoodsIntegral,@GoodsTypeId,@FreightId,@WId,@CreateTime)";
            //商品属性
            string sql1 = "insert into GoodsAttribute values(@SpecificationName,@Picture,@Price,@CostPrice,@OriginalPrice,@Inventory,@Weight,@Bulk,@GoodsSerial)";
            string[] str = new string[2];
            object[] param = new object[2];

            #region  参数化数组
            str[0] = sql;
            str[1] = sql1;
            param[0] = new
            {
                @GoodsNo = goods.GoodsNo,
                @GoodsName = goods.GoodsName,
                @GoodsPrice = goods.Price,
                @GoodsIntro = goods.GoodsIntro,
                @GoodsStock = goods.Inventory,
                @SaleNum = goods.SaleNum,
                @GoodsDesc = goods.GoodsDesc,
                @GoodsImg = goods.GoodsImg,
                @Sort = goods.Sort,
                @SaleTime = goods.SaleTime,
                @GoodsUnit = goods.GoodsUnit,
                @Keyword = goods.Keyword,               
                @GoodsIntegral=goods.GoodsIntegral,
                @GoodsTypeId =goods.GoodsTypeId,
                @FreightId = FreightId,
                @WId=goods.WId,
                @CreateTime = goods.CreateTime
    
            };
          
            param[1] = new
            {
                @SpecificationName = goods.SpecificationName,
                @Picture = goods.Picture,
                @Price = goods.Price,
                @CostPrice = goods.CostPrice,
                @OriginalPrice = goods.OriginalPrice,
                @Inventory = goods.Inventory,
                @Weight = goods.Weight,
                @Bulk=goods.Bulk,
                @GoodsSerial = GoodsNo
            };
            #endregion

            int i = dbFactory.DbHelper().ExecuteTransaction(str,param);
            return i;
        }


        /// <summary>
        /// 修改商品状态(上架/下架)
        /// </summary>
        /// <param name="GoodsId"></param>
        /// <param name="GoodsStatu"></param>
        /// <returns></returns>
        public int PutGoodsStatu(int GoodsId, int GoodsStatu)
        {
            string sql = "update Goods set Status=@GoodsStatu where GoodsId=@GoodsId";

            int result = dbFactory.DbHelper().Execute(sql,new { @GoodsStatu= GoodsStatu , @GoodsId = GoodsId });

            return result;
        }

    }
}
