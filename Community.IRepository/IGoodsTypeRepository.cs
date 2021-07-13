using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//引用
using Community.Model;

namespace Community.IRepository
{
    public interface IGoodsTypeRepository
    {
        /// <summary>
        /// 获取商品类型数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        List<GoodsType> GetGoodsType(string name,string states);

        /// <summary>
        /// 删除商品类型数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DelGoodsType(int id);


        /// <summary>
        /// 添加商品类型
        /// </summary>
        /// <param name="goods">商品类型模型</param>
        /// <returns></returns>
        int AddGoodsType(GoodsType goods);

        /// <summary>
        /// 获取商品类型的父级
        /// </summary>
        /// <returns></returns>
        List<GoodsTypeParent> GetParent();

        /// <summary>
        ///  查询不是该父级类型下的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<GoodsType> ShowSubcatalog(int id);

        /// <summary>
        /// 添加子目录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int AddSubcatalog(int GoodsTypeId, int prentId);


        /// <summary>
        /// 修改商品类型
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        int editGoodsTypeMsg(GoodsType goods);

        /// <summary>
        ///修改商品类型状态
        /// </summary>
        /// <param name="goodsTypeId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        int editGoodsTypeStatus(int goodsTypeId, int Status);

    }
}
