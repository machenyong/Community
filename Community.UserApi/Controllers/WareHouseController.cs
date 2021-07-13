using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Community.IRepository;
using Community.Model;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using Community.Common;
using Microsoft.AspNetCore.Hosting;

namespace Community.UserApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WareHouseController : Controller
    {
        //构造注入
        private IWareHouseRepository _wareHouseRepository;
        private IHostingEnvironment _hostingEnvironment;
        public WareHouseController(IWareHouseRepository wareHouseRepository, IHostingEnvironment hostingEnvironment)
        {
            _wareHouseRepository = wareHouseRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        #region 仓库管理
        /// <summary>
        /// 获取仓库数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="wareName"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetData(int page, int limit)
        {
            int total;
            var data = _wareHouseRepository.GetWarehouseModels(page, limit, out total);
            if (data != null) 
            {
                 return JsonConvert.SerializeObject(new { code = 200, count = total, data = data });
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    code = 500,
                    msg = "数据不存在",
                    data = data
                });
            }

        }
        /// <summary>
        /// 添加仓库信息
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        public int PostAddInfo(WarehouseModel m)
        {
            var list = _wareHouseRepository.AddWareHouseInfo(m);
            if (list > 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 修改仓库信息
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPut]
        public int UpdateInfoShow(WarehouseModel m)
        {
            var list = _wareHouseRepository.UpdateInfoShow(m);
            if (list > 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 删除仓库信息
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpDelete]
        public int DeleteInfo(int Id)
        {
            var list = _wareHouseRepository.DeleteInfoShow(Id);
            if (list > 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 修改仓库信息
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPut]
        public string PutStatus(int status,int Id)
        {
            var list = _wareHouseRepository.EditStatus(status,Id);
            return JsonConvert.SerializeObject(new { code = 200, msg = "修改状态成功", data = list });
        }
        #endregion

        #region 配送小区
        /// <summary>
        /// 获取配送小区信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetPlotData(int page, int limit, int wId = 0)
        {
            int total;
            var data = _wareHouseRepository.GetDistributionPlots(page, limit, out total, wId);
            if (data != null)
            {
                return JsonConvert.SerializeObject(new { code = 200, count = total, data = data });
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    code = 500,
                    msg = "数据不存在",
                    data = data
                });
            }
        }
        /// <summary>
        /// 仓库分类
        /// </summary>
        [HttpGet]
        public string WareHouseType()
        {
            var list = _wareHouseRepository.WareHouseType();
            return JsonConvert.SerializeObject(new { code = 0, msg = "成功", data = list });
        }
        #endregion

        #region 入库管理
        /// <summary>
        /// 入库管理
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetEnterManager(int page, int limit, string name = "", string number = "", string date = "", string end = "")
        {
            int total;
            var data = _wareHouseRepository.GetOutInWarehouses(page, limit, out total, name, number, date, end);
            if (data != null)
            {
                return JsonConvert.SerializeObject(new { code = 200, count = total, data = data });
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    code = 500,
                    msg = "数据不存在",
                    data = data
                });
            }
        }

        /// <summary>
        /// 添加入库信息
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        public int PostEnterInfo(OutInWarehouse m)
        {
            var list = _wareHouseRepository.EnterAdd(m);
            if (list > 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 修改入库信息
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPut]
        public int PutEnterInfo(WarehouseModel outIn)
        {
            var list = _wareHouseRepository.EnterEdit(outIn);
            if (list > 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 删除入库信息
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpDelete]
        public int DeleteEnterInfo(int Id)
        {
            var list = _wareHouseRepository.EnterDelete(Id);
            if (list > 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        #endregion

        #region 盘点管理
        /// <summary>
        /// 盘点管理
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="number"></param>
        /// <param name="date"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetCheckSheet(int page, int limit, string name = "", string number = "", string date = "", string end = "")
        {
            int total;
            var data = _wareHouseRepository.GetCheckSheets(page, limit, out total, name, number, date, end);
            if (data != null)
            {
                return JsonConvert.SerializeObject(new { code = 200, count = total, data = data });
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    code = 500,
                    msg = "数据不存在",
                    data = data
                });
            }
        }
        #endregion

        #region 现有库存
        /// <summary>
        /// 现有库存
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="wareName"></param>
        /// <param name="goodsName"></param>
        /// <param name="goodId"></param>
        /// <param name="goodNo"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetRepertory(int page, int limit, string wareName = "", string goodsName = "", int goodId = 0, string goodNo = "")
        {
            int total;
            var data = _wareHouseRepository.NowRepertory(page, limit, out total, wareName, goodsName, goodId, goodNo);
            if (data != null)
            {
                return JsonConvert.SerializeObject(new { code = 200, count = total, data = data });
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    code = 500,
                    msg = "数据不存在",
                    data = data
                });
            }
        }
        #endregion

        /// <summary>
        /// 处理图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ManageImg()
        {
            IFormFile file = Request.Form.Files[0];
            UploadFilesHelper uploadFiles = new UploadFilesHelper(_hostingEnvironment,"img");
            string img = uploadFiles.Main(file);
            return Ok(new
            {
                httpCode = 200,
                data = img
            });
        }

    }

}
