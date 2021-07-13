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
    public class BargainListController : ControllerBase
    {
        private IHostingEnvironment _ev;
        IBargainListRepository _bargainListRepository;

        public BargainListController(IBargainListRepository bargainListRepository, IHostingEnvironment ev)
        {
            _bargainListRepository = bargainListRepository;
            _ev = ev;
        }

        /// <summary>
        /// 删除砍价列表信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public bool DelBargainList(int id)
        {
            int? data = _bargainListRepository.Delete(id);

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
        /// 显示砍价列表信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetBargainLists(int page, int limit, string begin, string end ,int state = -1)
        {
            int total;

            List<BargainList> list = _bargainListRepository.GetBargainLists(page, limit, out total, begin, end, state);

            return Ok(new { code = 0, data = list, count = total });
        }

        /// <summary>
        /// 查看详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUser(int id)
        {
            List<BargainList> list = _bargainListRepository.GetUser(id);

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
