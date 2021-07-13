using Community.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IRepository
{
    public interface ISeckillConfignRepository
    {
        /// <summary>
        /// 显示秒杀配置的信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="total"></param>
        /// <param name="confignName"></param>
        /// <param name="confignState"></param>
        /// <returns></returns>
        List<SeckillConfign> GetSeckillConfigns(int page, int limit, out int total, string confignName, int confignState);

        /// <summary>
        /// 添加秒杀配置的信息
        /// </summary>
        /// <param name="seckill"></param>
        /// <returns></returns>
        int Add(SeckillConfign seckill);

        /// <summary>
        /// 删除秒杀配置的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(int id);

        /// <summary>
        /// 修改秒杀配置的信息
        /// </summary>
        /// <param name="seckill"></param>
        /// <returns></returns>
        int Edit(SeckillConfign seckill);

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int EditStatu(int id, int state);

        /// <summary>
        /// 添加秒杀商品
        /// </summary>
        /// <returns></returns>
        int AddGoods(SeckillGoods seckill);

        /// <summary>
        /// 商品列表
        /// </summary>
        /// <returns></returns>
        List<AllGoodsMsg> GetUserGoods(int page, int limit, out int total, string goodsName);

        /// <summary>
        /// 商品属性
        /// </summary>
        /// <returns></returns>
        List<GoodsAttribute> GetAttributes();
    }
}
