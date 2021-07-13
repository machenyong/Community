using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Community.IRepository;
using Community.Repository;
using Community.Model;
using Newtonsoft.Json;
using System.Text;
using Community.Common;
using Microsoft.AspNetCore.Hosting;

namespace Community.UserApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ColonelController : ControllerBase
    {
        //构造注入
        private IColonelRepository _colonelRepository;
        private IColonelGradeRepository _colonelGradeRepository;
        private ISalespersonRepository  _salespersonRepository;
        private ICommissionRepository _commissionRepository;
        private ICommunityGroupRepository  _communityGroupRepository;
        private IHostingEnvironment _hostingEnvironment;
        public ColonelController(IColonelRepository colonelRepository,IColonelGradeRepository colonelGradeRepository,ISalespersonRepository salespersonRepository,ICommunityGroupRepository communityGroupRepository,IHostingEnvironment hostingEnvironment,ICommissionRepository commissionRepository)
        {
            _colonelRepository = colonelRepository;
            _colonelGradeRepository = colonelGradeRepository;
            _salespersonRepository = salespersonRepository;
            _communityGroupRepository = communityGroupRepository;
            _hostingEnvironment = hostingEnvironment;
            _commissionRepository = commissionRepository;
        }
        #region//团长信息显示
        /// <summary>
        /// 显示团长信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetColonelData(int page=1, int limit=3,string colonelName="",string status="",string start="",string end="")
        {
            int totalCount;
           //获取团长包括查询、显示数据
            var res = _colonelRepository.GetColonelDataBySearch(page,limit,out totalCount, colonelName,status,start,end);
            if (res!=null)
            {
                return JsonConvert.SerializeObject(new
                {
                    code = 200,
                    msg = "",
                    data = res,
                    count=totalCount
                });
            }
            return JsonConvert.SerializeObject(new
            {
                code = 500,
                msg = "数据不存在",
                data = res,
                count = totalCount
            });

        }
        #endregion

        #region///团长信息修改
        [HttpPut]
        public string EditColonelById(Colonel colonel)
        {
            //调用团长等级信息方法
            var res = _colonelRepository.EditColonelById(colonel);
            return JsonConvert.SerializeObject(new { code = 200, msg = "修改成功", data = res });
        }
        #endregion

        #region///团长审核状态通过
        [HttpPut]
        public string CheckColonelStatusOK(int colonelId)
        {
            //调用团长等级信息方法
            var res = _colonelRepository.CheckColonelStatusOK(colonelId);
            return JsonConvert.SerializeObject(new { code = 200, msg = "通过成功", data = res });
        }
        #endregion

        #region///团长审核状态不通过
        [HttpPut]
        public string CheckColonelStatusNO(int colonelId)
        {
            //调用团长等级信息方法
            var res = _colonelRepository.CheckColonelStatusNO(colonelId);
            return JsonConvert.SerializeObject(new { code = 200, msg = "拒绝成功", data = res });
        }
        #endregion
        #region//团长等级信息
        //团长等级
        [HttpGet]
        public string GetColonelGradeData(int page = 1, int limit = 3)
        {
            int totalCount;
            var res = _colonelGradeRepository.GetColonelGrades(page, limit, out totalCount);
            if (res!=null)
            {
                return JsonConvert.SerializeObject(new { 
                    code=200,
                    mag="成功",
                    data=res,
                    count = totalCount
                });
            }
            return JsonConvert.SerializeObject(new
            {
                code = 500,
                msg = "数据不存在",
                data = res,
                count = totalCount
            });
        }
        #endregion
        #region///团长等级状态扭转
        [HttpPut]
        public string UpdataColonelGradeStatus(int GradeId, int Status)
        {
            //调用状态扭转方法
            var res = _colonelGradeRepository.UpdateColonelGradeStatusById(GradeId,Status);
            return JsonConvert.SerializeObject(new { code=0,data=res});
        }
        #endregion
        #region///团长等级删除
        [HttpDelete]
        public string DeleteColonelGrade(int GradeId)
        {
            //调用状态扭转方法
            var res = _colonelGradeRepository.DeleteColonelGradeById(GradeId);
            return JsonConvert.SerializeObject(new { code = 200,msg="删除成功", data = res });
        }
        #endregion

        #region///团长等级信息修改
        [HttpPut]
        public string EditColonelGradeById(ColonelGrade colonelGrade)
        {
            //调用团长等级信息方法
            var res = _colonelGradeRepository.EditColonelGradeById(colonelGrade);
            return JsonConvert.SerializeObject(new { code = 200, msg = "修改成功", data = res });
        }
        #endregion

        #region///核销员新增
        [HttpPost]
        public string AddSalesperson(Salesperson  salesperson)
        {
            //调用团长等级信息方法
            var res = _salespersonRepository.AddSalesperson(salesperson);
            return JsonConvert.SerializeObject(new { code = 200, msg = "修改成功", data = res });
        }
        #endregion

        #region///查看当前团长下的核销员
        [HttpGet]
        public string ShowSalespersonByColonelId(int colonelId)
        {
            //调用查看当前团长下的核销员方法
            var res = _salespersonRepository.ShowSalespersonByColonelId(colonelId);
            return JsonConvert.SerializeObject(new { code = 200, msg = "成功查询到数据", data = res });
        }
        #endregion

        #region///删除当前团长下的核销员
        [HttpDelete]
        public string DeleteSalespersonBySalespersonID(int salespersonID)
        {
            //调用删除核销员方法
            var res = _salespersonRepository.DeleteSalespersonBySalespersonID(salespersonID);
            return JsonConvert.SerializeObject(new { code = 200, msg = "成功删除当前团长下核销员", data = res });
        }
        #endregion

        #region///团购配置新增
        [HttpPost]
        public string AddCommunityGroup(CommunityGroup  communityGroup)
        {

            
            
            //调用团长等级信息方法
            var res = _communityGroupRepository.AddCommunityGroup(communityGroup);
            return JsonConvert.SerializeObject(new { code = 200, msg = "修改成功", data = res });
        }
        #endregion

        #region///路线显示
        [HttpGet]
        public string ShowColonelLine(int page, int limit, string wareName="")
        {
            int total;
            //调用团长等级信息方法
            var res = _colonelRepository.ShowColonelLine(page,limit,out total,wareName);
            return JsonConvert.SerializeObject(new { code = 200, msg = "修改成功", data = res });
        }
        #endregion
        #region///获取佣金流水显示分页
        [HttpGet]
        public string GetCommissions(int page, int limit, int orderStatus=0,int commTypeID = 0,string begin="",string end="")
        {
            int total;
            //调用团长等级信息方法
            var res = _commissionRepository.GetCommissions(page, limit, out total,orderStatus, commTypeID,begin,end);
            return JsonConvert.SerializeObject(new { code = 200, msg = "成功显示", data = res });
        }
        #endregion
        #region///获取佣金流水类型
        [HttpGet]
        public string GetCommissionTypes()
        {
           
            var res =_commissionRepository.GetCommissionTypes();
            return JsonConvert.SerializeObject(new { code = 200, msg = "成功拿到", data = res });
        }
        #endregion

        #region///绑定商品
        [HttpPost]
        public string BindGoods(int colonelId, string goodsNumber)
        {
            var res = _colonelRepository.BindGoods(colonelId,goodsNumber);
            return JsonConvert.SerializeObject(new { code = 200, msg = "绑定商品成功", data = res });
        }
        #endregion
    }
}
