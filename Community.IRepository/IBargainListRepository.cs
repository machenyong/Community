using Community.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IRepository
{
    public interface IBargainListRepository
    {
        /// <summary>
        /// 显示砍价列表的信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="total"></param>
        /// <param name="confignName"></param>
        /// <param name="confignState"></param>
        /// <returns></returns>
        List<BargainList> GetBargainLists(int page, int limit, out int total, string begin, string end,int state);

        /// <summary>
        /// 删除砍价列表的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(int id);

        /// <summary>
        /// 查看详情
        /// </summary>
        /// <returns></returns>
        List<BargainList> GetUser(int id);
    }
}
