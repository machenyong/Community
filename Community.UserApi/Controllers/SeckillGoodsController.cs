using Community.Common;
using Community.IRepository;
using Community.Model;
using Microsoft.AspNetCore.Hosting;
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
    public class SeckillGoodsController : ControllerBase
    {
        private IHostingEnvironment _ev;
        ISeckillGoodsRepository _seckillGoodsRepository;

        public SeckillGoodsController(ISeckillGoodsRepository seckillGoodsRepository, IHostingEnvironment ev)
        {
            _seckillGoodsRepository = seckillGoodsRepository;
            _ev = ev;
        }

        /// <summary>
        /// 显示秒杀商品
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetSeckillGoods(int page, int limit, string confignName, string goodsName, int state = -1)
        {
            int total;

            List<SeckillGoodsModel> list = _seckillGoodsRepository.GetSeckillGoods(page, limit, out total, confignName, goodsName, state);

            return Ok(new { code = 0, data = list, count = total });
        }

        /// <summary>
        /// 删除秒杀商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public bool DelSeckillGoods(int id)
        {
            int? data = _seckillGoodsRepository.Delete(id);

            if (data == null)
            {
                return false;
            }
            else
            {
                return data > 0 ? true : false;
            }
        }

        /// <summary>
        /// 修改秒杀商品的信息
        /// </summary>
        /// <param name="seckill"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Edit(SeckillGoods seckill)
        {
            int? data = _seckillGoodsRepository.Edit(seckill);

            if (data == null)
            {
                return false;
            }
            else
            {
                return data > 0 ? true : false;
            }
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="confignStatu"></param>
        /// <param name="confignID"></param>
        /// <returns></returns>
        [HttpPut]
        public bool EditStatu(int id, int state)
        {
            int? data = _seckillGoodsRepository.EditStatu(id, state);

            if (data == null)
            {
                return false;
            }
            else
            {
                return data > 0 ? true : false;
            }
        }

        /// <summary>
        /// 绑定配置名称下拉框
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetSeckills()
        {
            List<SeckillConfign> list = _seckillGoodsRepository.GetSeckills();

            return Ok(new { code = 0, data = list });
        }

        /// <summary>
        /// 绑定商品下拉框
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetGoods()
        {
            List<Goods> list = _seckillGoodsRepository.GetGoods();

            return Ok(new { code = 0, data = list });
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult upload()
        {
            UploadFilesHelper uploadFiles = new UploadFilesHelper(_ev, "img");
            IFormFile file = Request.Form.Files[0];
            var filenameX = uploadFiles.Main(file);
            return Ok(new { code = 0, data = filenameX });
        }
    }
}
