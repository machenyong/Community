using Community.IRepository;
using System;
using System.Collections.Generic;
using Community.Common;
using Community.Model;
using System.Text;
using System.Linq;

namespace Community.Repository
{
    public class ColonelRepository : IColonelRepository
    {
        DbFactory dbFactory = new DbFactory();
        # region///获取团长数据、查询、分页
        /// <summary>
        /// 获取团长数据、查询
        /// </summary>
        /// <param name="colonelName"></param>
        /// <returns></returns>
        public List<Colonel> GetColonelDataBySearch(int page,int limit,out int total,string colonelName,string status,string start,string end)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Colonel where 1=1");
            //strSql.Append("select ColonelId,Img,ColonelName,ColonelPhone,City,PickUpID,Colonelfee,CheckName,ApplyDate,CheckDate,CheckStatu from Colonel where 1=1");
            if (!string.IsNullOrEmpty(colonelName))
            {
                strSql.Append(" and ColonelName like CONCAT('%',@ColonelName,'%')");
            }
            if (!string.IsNullOrEmpty(status))
            {
                strSql.Append(" and CheckStatu=@status");
            }
            var data= dbFactory.DbHelper().Query<Colonel>(strSql.ToString(), new { @ColonelName = colonelName, @status=status });
            //区间查询
            if (start != "null" && end != "null")
            {
                if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
                {
                    data = data.Where(p => p.ApplyDate > Convert.ToDateTime(start) && p.CheckDate < Convert.ToDateTime(end)).ToList();
                }
            }
           
            total = data.Count;
            return data.Skip((page-1)*limit).Take(limit).ToList();
        }
        #endregion//
        #region ///通过Id删除团长信息
        public int DeleteColonelById(int colonelId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Colonel where ColonelId=@colonelId");
            var res = dbFactory.DbHelper().Execute(strSql.ToString(), new { @colonelId = colonelId });
            return res;
        }
        #endregion

        #region ///编辑团长信息
        public int EditColonelById(Colonel colonel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Colonel set Img=@Img,ColonelName=@ColonelName,ColonelPhone=@ColonelPhone,City=@City,PickUpID=@PickUpID,PickUpCommunity=@PickUpCommunity,PickUpAddress=@PickUpAddress,PickUpX=@PickUpX,PickUpY=@PickUpY,MapPosition=@MapPosition,Colonelfee=@Colonelfee,CheckName=@CheckName,ApplyDate=@ApplyDate,CheckDate=@CheckDate,CheckStatu=@CheckStatu,RegisterDate=@RegisterDate,IsOrNotSendHome=@IsOrNotSendHome,ColonelSex=@ColonelSex,NickName=@NickName,MemberNumber=@MemberNumber,SuperiorTeamHeader=@SuperiorTeamHeader,Integral=@Integral,Sale=@Sale,Alipay=@Alipay,Bank=@Bank,CardHolderName=@CardHolderName,GoodsID=@GoodsID,CardNumber=@CardNumber where ColonelId=@colonelId");
            var res = dbFactory.DbHelper().Execute(strSql.ToString(), new { @Img=colonel.Img, @ColonelName=colonel.ColonelName, @ColonelPhone=colonel.ColonelPhone, @City=colonel.City, @PickUpID=colonel.PickUpID, @PickUpCommunity=colonel.PickUpCommunity, @PickUpAddress=colonel.PickUpAddress, @PickUpX=colonel.PickUpX, @PickUpY=colonel.PickUpY, @MapPosition=colonel.MapPosition, @Colonelfee=colonel.Colonelfee, @CheckName=colonel.CheckName, @ApplyDate=colonel.ApplyDate, @CheckDate=colonel.CheckDate, @CheckStatu=colonel.CheckStatu, @RegisterDate=colonel.RegisterDate, @IsOrNotSendHome=colonel.IsOrNotSendHome, @ColonelSex=colonel.ColonelSex, @NickName=colonel.NickName, @MemberNumber=colonel.MemberNumber, @SuperiorTeamHeader=colonel.SuperiorTeamHeader, @Integral=colonel.Integral, @Sale=colonel.Sale, @Alipay=colonel.Alipay, @Bank=colonel.Bank, @CardHolderName=colonel.CardHolderName, @GoodsID=colonel.GoodsID, @CardNumber=colonel.CardNumber, @colonelId = colonel.ColonelId });
            return res;
        }
        #endregion

        #region ///审核通过
        public int CheckColonelStatusOK(int colonelId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Colonel set CheckStatu=1 where ColonelId=@colonelId");
            var res = dbFactory.DbHelper().Execute(strSql.ToString(), new {  @colonelId = colonelId});
            return res;
        }
        #endregion

        #region ///审核不通过
        public int CheckColonelStatusNO(int colonelId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Colonel set CheckStatu=2 where ColonelId=@colonelId");
            var res = dbFactory.DbHelper().Execute(strSql.ToString(), new { @colonelId = colonelId });
            return res;
        }
        #endregion

        #region ///路线显示
        public List<WarehouseModel> ShowColonelLine(int page, int limit, out int totalCount,string wareName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Warehouse where 1=1");
            if (!string.IsNullOrEmpty(wareName))
            {
                strSql.Append(" and WareName like CONCAT('%',@WareName,'%')");
            }
            var data = dbFactory.DbHelper().Query<WarehouseModel>(strSql.ToString(), new { @WareName =wareName});
            string sql = "select count(ColonelId) as ColonelNumber from Warehouse group by ColonelId having ColonelId = 1";
            var num = dbFactory.DbHelper().Query<WarehouseModel>(sql);
            foreach (var item in data)
            {
                foreach (var temp in num)
                {
                    item.ColonelNumber = temp.ColonelNumber;
                }
            }
            totalCount = data.Count();//总数量
            return data.Skip((page - 1) * limit)
                .Take(limit)
                .ToList();
        }
        #endregion
        #region ///绑定商品
        /// <summary>
        /// 绑定商品
        /// </summary>
        /// <param name="colonelId"></param>
        /// <param name="goodsNumber"></param>
        /// <returns></returns>
        public int BindGoods(int colonelId, string goodsNumber)
        {
            StringBuilder strSql = new StringBuilder();
            string[] vs = goodsNumber.Split(',');
            int res=0;
            foreach (var item in vs)
            {
                strSql.Append("insert into GoodsColonel values(@goodsNumber,@colonelId)");
                res = dbFactory.DbHelper().Execute(strSql.ToString(), new { @goodsNumber = item, @colonelId = colonelId });
            }
            return res;
        }
        #endregion
    }
}
