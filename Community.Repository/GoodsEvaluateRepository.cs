using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//引用
using Community.IRepository;
using Community.Model;
using Community.Common;

namespace Community.Repository
{
    public class GoodsEvaluateRepository : IGoodsEvaluateRepository
    {
        //实例化工厂类
        DbFactory dbFactory = new DbFactory();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<GoodsEvaluateMessage> GetGoodsEvaluate(string Statu = "", string GoodsName = "", string UserName = "",int time=99)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select d.*,a.UserAccount,c.GoodsImg,c.GoodsName from UserInfo a join GoodsUserInfo b on a.UserId =b.UserGoodsId");
            stringBuilder.Append(" join Goods c on b.GoodsUserId=c.GoodsId");
            stringBuilder.Append(" join GoodsEvaluate d on c.GoodsId=d.GoodsId where 1=1");
            //时间倒叙排序
            stringBuilder.Append(" order by d.EvaluateTime desc");

            #region 查询
            List<GoodsEvaluateMessage> list = dbFactory.DbHelper().Query<GoodsEvaluateMessage>(stringBuilder.ToString());
            if (!string.IsNullOrEmpty(Statu))
            {
                list = list.Where(p => p.EvaluateState.Equals(Convert.ToInt32(Statu))).ToList();
            }
            if (!string.IsNullOrEmpty(GoodsName))
            {
                list = list.Where(p => p.GoodsName.Contains(GoodsName)).ToList();
            }
            if (!string.IsNullOrEmpty(UserName))
            {
                list = list.Where(p => p.UserAccount.Contains(UserName)).ToList();
            }
            #endregion

            //获取当前时间
            DateTime date = DateTime.Now;
   
            DateTime kk = DateTime.Now;
        
            if (time==99)
            {
                
            }
            if (time == 0)   //今天
            {
                
            }
            if (time==1)  //昨天
            {
               kk = DateTime.Now.AddDays(-1);
            } 
            if (time==2)  //7天前
            {
                kk = DateTime.Now.AddDays(-7);
            }
            if (time == 3)  //30天前
            {
                kk = DateTime.Now.AddDays(-30);
            }
            if (time==4)    //本月
            {
                int day = kk.Day;  //计算当前过了多少天
                kk = DateTime.Now.AddDays(-day);
            }
            if (time == 5)
            {
                
            }
            return list; 
        }
    }
}
