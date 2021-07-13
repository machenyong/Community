using Community.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IRepository
{
    public interface ISeckillGoodsRepository 
    {
        /// <summary>
        /// 绑定配置名称下拉框
        /// </summary>
        /// <returns></returns>
        List<SeckillConfign> GetSeckills();

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
        List<SeckillGoodsModel> GetSeckillGoods(int page, int limit, out int total,string confignName, string goodsName, int state);

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
        int Edit(SeckillGoods seckill);

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int EditStatu(int id, int state);
    }
}
