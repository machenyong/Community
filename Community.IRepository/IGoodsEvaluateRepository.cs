using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//引用
using Community.Model;

namespace Community.IRepository
{
    public interface IGoodsEvaluateRepository
    {

        /// <summary>
        /// 获取评价
        /// </summary>
        /// <returns></returns>
        List<GoodsEvaluateMessage> GetGoodsEvaluate(string Statu = "", string GoodsName = "", string UserName = "",int time=99);
    }
}
