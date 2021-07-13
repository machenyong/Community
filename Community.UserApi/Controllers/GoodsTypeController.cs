using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//引用
using Community.Model;
using Community.IRepository;
using Community.Common;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace Community.UserApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GoodsTypeController : ControllerBase
    {
        private readonly IGoodsTypeRepository _goodsRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        //构造函数
        public GoodsTypeController(IGoodsTypeRepository goodsRepository, IHostingEnvironment hostingEnvironment)
        {
            _goodsRepository = goodsRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 获取商品类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetGoodsType(string name="", string states="")
        {
            List<GoodsType> goodsTypes = _goodsRepository.GetGoodsType(name, states);
            //return JsonConvert.SerializeObject(new { result = goodsTypes });
            return Ok(new
            {
                code = "",
                data = goodsTypes,
                statu = 200
            });
        }

        /// <summary>
        /// 删除商品类型数据
        /// </summary>
        /// <param name="GoodsTypeId"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DelGoodsType(int GoodsTypeId)
        {
            int i = _goodsRepository.DelGoodsType(GoodsTypeId);

            //成功
            return Ok(new
            {
                code = "",
                data = i,
                statu = 200
            });
        }

        [HttpGet]
        /// <summary>
        /// 获取商品类型的父级
        /// </summary>
        /// <returns></returns>
        public string GetGoodsPrent()
        {
            List<GoodsTypeParent> list = _goodsRepository.GetParent();
            return JsonConvert.SerializeObject(
                new
                {
                    code = "",
                    data = list,
                    statu = 200
                });
        }


        /// <summary>
        /// 添加商品类型
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        [HttpPost]
        public int AddGoodsType(GoodsType goods)
        {
            int i = _goodsRepository.AddGoodsType(goods);
       
            return i;
        }

        /// <summary>
        /// 处理图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ManageImg()
        {
            IFormFile file = Request.Form.Files[0];
            UploadFilesHelper uploadFiles = new UploadFilesHelper(_hostingEnvironment,"Img");
            string img = uploadFiles.Main(file);
            return Ok(new {
                httpCode=200,
                result= img
            });
        }


        /// <summary>
        /// 查询不是该父级类型下的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ShowSubcatalog(int id)
        {
            List<GoodsType> list = _goodsRepository.ShowSubcatalog(id);
            //HttpContext.Session.SetString("prentId", id.ToString());
            return Ok(new { 
                code=200,
                data=list,
                msg=""
            });
        }


        /// <summary>
        /// 添加子目录
        /// </summary>
        /// <param name="GoodsTypeId"></param>
        /// <param name="prentId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddSubcatalog(int GoodsTypeId,int ParentId)
        {
            //string prent= HttpContext.Session.GetString("prentId");
            int i = _goodsRepository.AddSubcatalog(GoodsTypeId, ParentId);
            return Ok(new { 
                code=200,
                data=i,
                msg="添加成功"
            });
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult editGoodsTypeMsg(GoodsType goods)
        {
            int result = _goodsRepository.editGoodsTypeMsg(goods);

            return Ok(new { 
                code=200,
                data=result,
                msg="修改成功"
            });
        }


        /// <summary>
        /// 修改商品类型状态
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult editGoodsTypeStatus(int GoodsTypeId,int StatuId)
        {

            int result = _goodsRepository.editGoodsTypeStatus(GoodsTypeId, StatuId);
            return Ok(new
            {
                code = 200,
                data = result,
                msg = "修改成功"
            });
        }
    }
}
