using Community.IRepository;
using Community.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.UserApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly IGoodsRepository _goodsRepository;

        //构造函数
        public GoodsController(IGoodsRepository goodsRepository)
        {
            _goodsRepository = goodsRepository;
        }


        [HttpGet]
        /// <summary>
        /// 获取商品
        /// </summary>
        /// <returns></returns>
        public IActionResult GetGoods(int pageIndex, int limit,int goodsStatu, int GoodsTypeIdInquire = 0, string GoodsName = "")
        {
            int count;
            //调用商品
            List<Goods> goods = _goodsRepository.GetGoods(pageIndex, limit, goodsStatu, GoodsTypeIdInquire, GoodsName, out count);
            //返回
            return Ok(new { count = count, result = goods });
        }


        /// <summary>
        /// 回收商品
        /// </summary>
        /// <param name="GoodsId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult recycleGoods(int GoodsId,int GoodsStatus)
        {
            int result = _goodsRepository.recycleGoods(GoodsId,GoodsStatus);

            return Ok(new { 
                code=200,
                data=result,
                msg="回收成功"
            });
        }


        /// <summary>
        /// 修改商品
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult EditGoods(Goods goods)
        {
            int result = _goodsRepository.EditGoods(goods);
            return Ok(new { 
                code=1,
                data=result,
                msg="修改成功"
            });
        }


        /// <summary>
        /// 恢复商品
        /// </summary>
        /// <param name="GoodsId"></param>
        /// <param name="GoodsStatusRecover"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RecoverGoods(int GoodsId,int GoodsStatusRecover)
        {
            int result = _goodsRepository.RecoverGoods(GoodsId,GoodsStatusRecover);
            return Ok(new
            {
                code = 1,
                data = result,
                msg = "恢复成功"
            });
        }


        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="GoodsId"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DelGoods(int GoodsId)
        {
            int result = _goodsRepository.DelGoods(GoodsId);
            return Ok(new
            {
                code = 1,
                data = result,
                msg = "恢复成功"
            });
        }


        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="allGoods"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddGoods(AllGoodsMsg allGoods)
        {
            int result = _goodsRepository.AddGoods(allGoods);

            return Ok(new
            {
                code = 1,
                data = result,
                msg = "恢复成功"
            });
        }


        /// <summary>
        /// 修改商品状态(上架/下架)
        /// </summary>
        /// <param name="GoodsId"></param>
        /// <param name="GoodsStatu"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult PutGoodsStatus(int GoodsId,int GoodsStatu)
        {
            int result = _goodsRepository.PutGoodsStatu(GoodsId,GoodsStatu);
            return Ok(new
            {
                code = 1,
                data = result,
                msg = "修改成功"
            });
        }
    }
}
