using Community.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IRepository
{
    public interface IBargainGoodsRepository
    {
        /// <summary>
        /// 绑定商品下拉框
        /// </summary>
        /// <returns></returns>
        List<Goods> GetGoods();

        /// <summary>
        /// 显示秒杀配置的信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="total"></param>
        /// <param name="confignName"></param>
        /// <param name="confignState"></param>
        /// <returns></returns>
        List<BargainGoods> GetBargainGoods(int page, int limit, out int total,string cutName, int state);

        /// <summary>
        /// 添加秒杀配置的信息
        /// </summary>
        /// <param name="seckill"></param>
        /// <returns></returns>
        int Add(BargainGoods bargain);

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
        int Edit(BargainGoods bargain);

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int EditStatu(int id, int state);

        /// <summary>
        /// 商品列表
        /// </summary>
        /// <returns></returns>
        List<Goods> GetUserGoods(int page, int limit, out int total, string goodsName);

        /// <summary>
        /// 商品属性
        /// </summary>
        /// <returns></returns>
        List<GoodsAttribute> GetAttributes();
    }
}
