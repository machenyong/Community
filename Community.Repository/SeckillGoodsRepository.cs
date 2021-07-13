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
    public class SeckillGoodsRepository : ISeckillGoodsRepository
    {
        //实例化工厂
        DbFactory dbFactory = new DbFactory();

        /// <summary>
        /// 删除秒杀商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string sql = $"delete from SeckillGoods where SeckillID=@id";

            int i = dbFactory.DbHelper().Execute(sql, new { @id = id });

            return i;
        }

        /// <summary>
        /// 修改秒杀商品的信息
        /// </summary>
        /// <param name="seckill"></param>
        /// <returns></returns>
        public int Edit(SeckillGoods seckill)
        {
            string sql = $"update SeckillGoods set ConfignID=@ConfignID,SeckillTitle=@SeckillTitle,SeckillIntro=@SeckillIntro,UnitPrice=@UnitPrice,PriceSpike=@PriceSpike,Limited=@Limited,LimitedResidue=@LimitedResidue,SeckillState=@SeckillState,CreateTime=@CreateTime,GoodsID=@GoodsID where SeckillID = @SeckillID";

            int i = dbFactory.DbHelper().Execute(sql, new { @ConfignID = seckill.ConfignID, @SeckillTitle = seckill.SeckillTitle, @SeckillIntro = seckill.SeckillIntro, @UnitPrice = seckill.UnitPrice, @PriceSpike = seckill.PriceSpike, @Limited = seckill.Limited, @LimitedResidue = seckill.LimitedResidue, @SeckillState = seckill.SeckillState, @CreateTime = seckill.CreateTime, @GoodsID = seckill.GoodsID, @SeckillID = seckill.SeckillID });

            return i;
        }

        /// <summary>
        /// 修改秒杀商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int EditStatu(int id, int state)
        {
            string sql = $"update SeckillGoods set SeckillState=@state where SeckillID=@id";

            int i = dbFactory.DbHelper().Execute(sql, new { @state = state, @id = id });

            return i;
        }

        /// <summary>
        /// 绑定商品下拉框
        /// </summary>
        /// <returns></returns>
        public List<Goods> GetGoods()
        {
            string sql = $"select GoodsId,GoodsName from Goods";

            List<Goods> list = dbFactory.DbHelper().Query<Goods>(sql);

            return list;
        }

        /// <summary>
        /// 显示秒杀商品
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="total"></param>
        /// <param name="confignName"></param>
        /// <param name="goodsName"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public List<SeckillGoodsModel> GetSeckillGoods(int page, int limit, out int total,string confignName, string goodsName, int state = -1)
        {
            string sql = $"select SeckillID,B.ConfignName,B.BeginTime,B.EndTime,A.ConfignID,SeckillTitle,SeckillIntro,UnitPrice,PriceSpike,Limited,LimitedResidue,SeckillState,A.CreateTime, C.GoodsImg, C.GoodsId, C.GoodsName from SeckillGoods A join SeckillConfign B on A.ConfignID = B.ConfignID join Goods C on A.GoodsID = C.GoodsId";

            if (!string.IsNullOrEmpty(confignName))
            {
                sql += $" and B.ConfignName like '%{@confignName}%'";
            }
            if (state == 1)
            {
                sql += $" and SeckillState = @state";
            }
            if (state == 0)
            {
                sql += $" and SeckillState = @state";
            }
            if (!string.IsNullOrEmpty(goodsName))
            {
                sql += $" and C.GoodsName like '%{@goodsName}%'";
            }

            List<SeckillGoodsModel> list = dbFactory.DbHelper().Query<SeckillGoodsModel>(sql,new { @confignName = confignName, @state = state, @goodsName = goodsName});

            total = list.Count();

            list = list.Skip((page - 1) * limit).Take(limit).ToList();

            return list;
        }

        /// <summary>
        /// 绑定配置名称下拉框
        /// </summary>
        /// <returns></returns>
        public List<SeckillConfign> GetSeckills()
        {
            string sql = $"select ConfignID,ConfignName from SeckillConfign";

            List<SeckillConfign> list = dbFactory.DbHelper().Query<SeckillConfign>(sql);

            return list;
        }
    }
}
