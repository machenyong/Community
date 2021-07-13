using Community.Common;
using Community.IRepository;
using Community.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Repository
{
    public class BargainListRepository : IBargainListRepository
    {
        //实例化仓库
        DbFactory dbFactory = new DbFactory();

        /// <summary>
        /// 删除砍价列表信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string sql = $"delete from CutList where CutListId=@id";

            int i = dbFactory.DbHelper().Execute(sql, new { @id = id });

            return i;
        }

        /// <summary>
        /// 显示砍价列表信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<BargainList> GetBargainLists(int page, int limit, out int total, string begin, string end ,int state = -1)
        {
            string sql = $"select CutListId,CutGoods,A.BeginTime,A.EndTime,State,AllNumber,ResidueNumber,UserId,B.CutName,B.CutIntro,B.CutPrice,C.GoodsImg from CutList A join CutGoods B on A.CutGoods = B.CutGoodsID join Goods C on B.GoodsProperty = C.GoodsId where 1=1";

            if (!string.IsNullOrEmpty(begin))
            {
                sql += $" and A.BeginTime >= '{@begin}'";
            }
            if (!string.IsNullOrEmpty(end))
            {
                sql += $" and A.EndTime <= '{@end}'";
            }
            if (state == 1)
            {
                sql += $" and State = @state order by A.BeginTime desc";
            }
            if (state == 0)
            {
                sql += $" and State = @state order by A.BeginTime desc";
            }

            sql += $" order by BeginTime desc";

            List<BargainList> list = dbFactory.DbHelper().Query<BargainList>(sql, new { @begin = begin, @end = end, @state = state});

            total = list.Count();

            list = list.Skip((page - 1) * limit).Take(limit).ToList();

            return list;
        }

        /// <summary>
        /// 查看详情
        /// </summary>
        /// <returns></returns>
        public List<BargainList> GetUser(int id)
        {
            string sql = $"select A.BeginTime,A.UserId,C.UserImg,C.UserName,B.CutPrice from CutList A join CutGoods B on A.CutGoods=B.CutGoodsID join UserInfo C on A.UserId = C.UserId where A.UserId=@id";

            List<BargainList> list = dbFactory.DbHelper().Query<BargainList>(sql,new { @id=id});

            return list;
        }
    }
}
