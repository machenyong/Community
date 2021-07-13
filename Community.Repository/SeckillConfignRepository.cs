using Community.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Common;
using Community.Model;

namespace Community.Repository
{
    public class SeckillConfignRepository : ISeckillConfignRepository
    {
        //实例化仓库
        DbFactory dbFactory = new DbFactory();

        /// <summary>
        /// 添加秒杀配置的信息
        /// </summary>
        /// <param name="seckill"></param>
        /// <returns></returns>
        public int Add(SeckillConfign seckill)
        {
            string sql = $"insert into SeckillConfign values(@ConfignName,@BeginTime,@EndTime,@ConfignImg,@ConfignState,@CreateTime)";

            int i = dbFactory.DbHelper().Execute(sql, new { @ConfignName = seckill.ConfignName, @BeginTime = seckill.BeginTime, @EndTime = seckill.EndTime, @ConfignImg = seckill.ConfignImg, @ConfignState = seckill.ConfignState, @CreateTime = seckill.CreateTime });

            return i;
        }

        /// <summary>
        /// 删除秒杀配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string sql = $"delete from SeckillConfign where ConfignID=@id";

            int i = dbFactory.DbHelper().Execute(sql, new { @id = id });

            return i;
        }

        /// <summary>
        /// 修改秒杀配置的信息
        /// </summary>
        /// <param name="seckill"></param>
        /// <returns></returns>
        public int Edit(SeckillConfign seckill)
        {
            string sql = $"update SeckillConfign set ConfignName=@ConfignName, BeginTime=@BeginTime, EndTime=@EndTime, ConfignImg=@ConfignImg,ConfignState=@ConfignState,CreateTime=@CreateTime where ConfignID=@ConfignID";

            int i = dbFactory.DbHelper().Execute(sql, new { @ConfignName = seckill.ConfignName, @BeginTime = seckill.BeginTime, @EndTime = seckill.EndTime, @ConfignImg = seckill.ConfignImg, @ConfignState = seckill.ConfignState, @CreateTime = seckill.CreateTime, @ConfignID = seckill.ConfignID });


            return i;
        }

        /// <summary>
        /// 显示秒杀配置的信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="total"></param>
        /// <param name="confignName"></param>
        /// <param name="confignState"></param>
        /// <returns></returns>
        public List<SeckillConfign> GetSeckillConfigns(int page, int limit, out int total, string confignName, int confignState)
        {
            string sql = $"select ConfignID,ConfignName,BeginTime,EndTime,ConfignImg,ConfignState,CreateTime from SeckillConfign where 1=1";
            if (!string.IsNullOrEmpty(confignName))
            {
                sql += $" and ConfignName like '%{@confignName}%'";
            }
            if (confignState == 0)
            {
                sql += $" and ConfignState = @confignStatue";
            }
            if (confignState == 1)
            {
                sql += $" and ConfignState = @confignStatue";
            }

            sql += $" order by CreateTime desc";

            List<SeckillConfign> data = dbFactory.DbHelper().Query<SeckillConfign>(sql, new { @confignName = confignName, @confignStatue = confignState });


            total = data.Count();

            data = data.Skip((page - 1) * limit).Take(limit).ToList();

            return data;
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="confignStatu"></param>
        /// <param name="confignID"></param>
        /// <returns></returns>
        public int EditStatu(int id, int state)
        {
            string sql = $"update SeckillConfign set ConfignState=@state where ConfignID=@id";

            int i = dbFactory.DbHelper().Execute(sql, new { @state = state, @id = id });

            return i;
        }

        /// <summary>
        /// 添加秒杀商品
        /// </summary>
        /// <param name="seckill"></param>
        /// <returns></returns>
        public int AddGoods(SeckillGoods seckill)
        {
            string sql = $"insert into SeckillGoods values(@ConfignID,@SeckillTitle,@SeckillIntro,@UnitPrice,@PriceSpike,@Limited,@LimitedResidue,@SeckillState,@CreateTime,@GoodsID)";

            int i = dbFactory.DbHelper().Execute(sql,new { @ConfignID = seckill.ConfignID, @SeckillTitle = seckill.SeckillTitle, @SeckillIntro = seckill.SeckillIntro, @UnitPrice = seckill.UnitPrice, @PriceSpike = seckill.PriceSpike, @Limited = seckill.Limited, @LimitedResidue = seckill.LimitedResidue, @SeckillState = seckill.SeckillState, @CreateTime = seckill.CreateTime, @GoodsID = seckill.GoodsID});

            return i;
        }

        /// <summary>
        /// 显示商品列表
        /// </summary>
        /// <returns></returns>
        public List<AllGoodsMsg> GetUserGoods(int page, int limit, out int total, string goodsName)
        {
            string sql = $"select A.GoodsId,A.GoodsName,A.GoodsImg,B.TypeName,C.ParentName,D.SeckillTitle,D.SeckillIntro from Goods A join GoodsType B on A.GoodsTypeId = B.GoodsTypeId join GoodsTypeParent C on B.GoodsTypeParentId = C.ParentId join SeckillGoods D on A.GoodsId = D.GoodsID join SeckillConfign E on D.ConfignID = E.ConfignID where 1=1";

            if (!string.IsNullOrEmpty(goodsName))
            {
                sql += $" and A.GoodsName like '%{@goodsName}%'";
            }

            List<AllGoodsMsg> list = dbFactory.DbHelper().Query<AllGoodsMsg>(sql, new { @goodsName = goodsName });

            total = list.Count();

            list = list.Skip((page - 1) * limit).Take(limit).ToList();

            return list;
        }

        /// <summary>
        /// 商品属性
        /// </summary>
        /// <returns></returns>
        public List<GoodsAttribute> GetAttributes()
        {
            string sql = $"select A.Picture,C.PriceSpike,A.CostPrice,A.OriginalPrice,A.Inventory,A.Weight,A.[Bulk],C.Limited,A.GoodsSerial,C.SeckillState,C.GoodsID from GoodsAttribute A join Goods B on A.GoodsSerial = B.GoodsNo join SeckillGoods C on B.GoodsId = C.GoodsID";

            List<GoodsAttribute> list = dbFactory.DbHelper().Query<GoodsAttribute>(sql);

            return list;
        }
    }
}
