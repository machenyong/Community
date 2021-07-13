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
    public class SeckillConfignController : ControllerBase
    {
        private IHostingEnvironment _ev;
        ISeckillConfignRepository _seckillConfignRepository;

        public SeckillConfignController(ISeckillConfignRepository seckillConfignRepository, IHostingEnvironment ev)
        {
            _seckillConfignRepository = seckillConfignRepository;
            _ev = ev;
        }
        
        /// <summary>
        /// 显示秒杀配置信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="ConfignName"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetSeckillConfign(int page, int limit, string confignName, int confignState)
        {
            int total;

            List<SeckillConfign> data = _seckillConfignRepository.GetSeckillConfigns(page, limit, out total,confignName,confignState);

            return Ok(new { code = 0, data = data, count = total });
        }

        /// <summary>
        /// 添加秒杀配置的信息
        /// </summary>
        /// <param name="seckill"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddSeckillConfign(SeckillConfign seckill)
        {
            seckill.CreateTime = DateTime.Now;
            int? data1 = _seckillConfignRepository.Add(seckill);

            if (data1 == null)
            {
                return Ok(data1);
            }
            else
            {
                bool data= data1 > 0 ? true : false;
                return Ok(data);
            }
        }

        /// <summary>
        /// 删除秒杀配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public bool DelSeckillConfign(int id)
        {
            int? data = _seckillConfignRepository.Delete(id);

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
        /// 修改秒杀配置的信息
        /// </summary>
        /// <param name="seckill"></param>
        /// <returns></returns>
        [HttpPut]
        public bool PutSeckillConfign(SeckillConfign seckill)
        {
            int? data = _seckillConfignRepository.Edit(seckill);

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
            int? data = _seckillConfignRepository.EditStatu(id, state);

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
        /// 添加秒杀商品
        /// </summary>
        /// <param name="seckill"></param>
        /// <returns></returns>
        [HttpPost]
        public bool AddGoods(SeckillGoods seckill)
        {
            int? data = _seckillConfignRepository.AddGoods(seckill);

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
        /// 显示商品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUserGoods(int page, int limit, string goodsName)
        {
            int total;

            List<AllGoodsMsg> list = _seckillConfignRepository.GetUserGoods(page, limit, out total, goodsName);

            return Ok(new { code = 0, data = list, count = total });
        }

        /// <summary>
        /// 商品属性
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAttributes()
        {
            List<GoodsAttribute> list = _seckillConfignRepository.GetAttributes();

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
