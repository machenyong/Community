using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//引用
using Community.IRepository;
using Community.Model;

namespace Community.UserApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GoodsEvaluateController : ControllerBase
    {
        private readonly IGoodsEvaluateRepository _goodsEvaluateRepository;
        public GoodsEvaluateController(IGoodsEvaluateRepository goodsEvaluateRepository)
        {
            _goodsEvaluateRepository = goodsEvaluateRepository;
        }


        [HttpGet] 
        public IActionResult GetGoodsEvaluate(string Statu="",string GoodsName="",string UserName="",int time=99)
        {
            List<GoodsEvaluateMessage> list = _goodsEvaluateRepository.GetGoodsEvaluate(Statu, GoodsName, UserName,time);

            return Ok(new { 
                code=200,
                data=list,
                msg="获取成功"
            });
        }
    }
}
