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
    public class BargainGoodsController : ControllerBase
    {
        private IHostingEnvironment _ev;
        IBargainGoodsRepository _bargainGoodsRepository;

        public BargainGoodsController(IBargainGoodsRepository bargainGoodsRepository, IHostingEnvironment ev)
        {
            _bargainGoodsRepository = bargainGoodsRepository;
            _ev = ev;
        }

        /// <summary>
        /// 显示砍价商品
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetBargainGoods(int page, int limit, string cutName, int state = -1)
        {
            int total;

            List<BargainGoods> list = _bargainGoodsRepository.GetBargainGoods(page, limit, out total, cutName, state);

            return Ok(new { code = 0, data = list, count = total });
        }

        /// <summary>
        /// 删除砍价商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public bool DelBargainGoods(int id)
        {
            int? data = _bargainGoodsRepository.Delete(id);

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
        /// 修改砍价商品状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPut]
        public bool EditStatu(int id, int state)
        {
            int? data = _bargainGoodsRepository.EditStatu(id, state);

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
        /// 修改砍价商品
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public bool EditBargainGoods(BargainGoods bargain)
        {
            int? data = _bargainGoodsRepository.Edit(bargain);

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
        /// 绑定商品下拉框
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetGoods()
        {
            List<Goods> list = _bargainGoodsRepository.GetGoods();

            return Ok(new { code = 0, data = list });
        }

        /// <summary>
        /// 显示商品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUserGoods(int page, int limit, string goodsName)
        {
            int total;

            List<Goods> list = _bargainGoodsRepository.GetUserGoods(page, limit, out total, goodsName);

            return Ok(new { code = 0, data = list, count = total });
        }

        /// <summary>
        /// 商品属性
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAttributes()
        {
            List<GoodsAttribute> list = _bargainGoodsRepository.GetAttributes();

            return Ok(new { code = 0, data = list });
        }

        /// <summary>
        /// 添加砍价商品信息
        /// </summary>
        /// <param name="bargain"></param>
        /// <returns></returns>
        [HttpPost]
        public bool AddBargainGoods(BargainGoods bargain)
        {
            int? data = _bargainGoodsRepository.Add(bargain);

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
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult upload()
        {
            UploadFilesHelper uploadFiles = new UploadFilesHelper(_ev,"img");
            IFormFile file = Request.Form.Files[0];
            var filenameX = uploadFiles.Main(file);
            return Ok(new { code = 0, data = filenameX });
        }
    }
}
