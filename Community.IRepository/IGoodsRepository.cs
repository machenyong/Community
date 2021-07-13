using Community.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IRepository
{
    public interface IGoodsRepository
    {
        /// <summary>
        /// 获取商品数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        List<Goods> GetGoods(int pageIndex, int limit,int GoodsStatus,int GoodsTypeIdInquire,string GoodsName,out int count);

        /// <summary>
        /// 加入回收站(假删)
        /// </summary>
        /// <param name="GoodsId"></param>
        /// <returns></returns>
        int recycleGoods(int GoodsId,int GoodsStatus);

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        int EditGoods(Goods goods);

        /// <summary>
        /// 恢复商品
        /// </summary>
        /// <param name="GoodsId"></param>
        /// <param name="GoodsStatusRecover"></param>
        /// <returns></returns>
        int RecoverGoods(int GoodsId, int GoodsStatusRecover);


        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="GoodsId"></param>
        /// <returns></returns>
        int DelGoods(int GoodsId);


        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        int AddGoods(AllGoodsMsg goods);


        int PutGoodsStatu(int GoodsId,int GoodsStatu);
    }
}
