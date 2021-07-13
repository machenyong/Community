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
    public class BargainGoodsRepository : IBargainGoodsRepository
    {
        //实例化仓库
        DbFactory dbFactory = new DbFactory();

        /// <summary>
        /// 添加砍价商品
        /// </summary>
        /// <param name="bargain"></param>
        /// <returns></returns>
        public int Add(BargainGoods bargain)
        {
            string sql = $"insert into CutGoods values(@CutName,@CutIntro,@CutPrice,@CutSort,@BeginTime,@EndTime,@CutPeople,@CutNumber,@NumberLimited,@Freight,@CutState,@GoodsProperty,@CutSuccess,@CutPartici,@Limited,@LimitedResidue)";

            int i = dbFactory.DbHelper().Execute(sql, new { @CutName = bargain.CutName, @CutIntro = bargain.CutIntro, @CutPrice = bargain.CutPrice, @CutSort = bargain.CutSort, @BeginTime = bargain.BeginTime, @EndTime = bargain.EndTime, @CutPeople = bargain.CutPeople, @CutNumber = bargain.CutNumber, @NumberLimited = bargain.NumberLimited, @Freight = bargain.Freight, @CutState = bargain.CutState, @GoodsProperty = bargain.GoodsProperty, @CutSuccess = bargain.CutSuccess, @CutPartici = bargain.CutPartici, @Limited = bargain.Limited, @LimitedResidue = bargain.LimitedResidue });

            return i;
        }

        /// <summary>
        /// 删除砍价商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string sql = $"delete from CutGoods where CutGoodsID=@id";

            int i = dbFactory.DbHelper().Execute(sql, new { @id = id });

            return i;
        }

        /// <summary>
        /// 修改砍价商品信息
        /// </summary>
        /// <param name="bargain"></param>
        /// <returns></returns>
        public int Edit(BargainGoods bargain)
        {
            string sql = $"update CutGoods set CutName=@cutName,CutIntro=@cutIntro,CutPrice=@cutPrice,CutSort=@cutSort,BeginTime=@beginTime,EndTime=@endTime,CutPeople=@cutPeople,CutNumber=@cutNumber,NumberLimited=@numberLimited,Freight = @freight,CutState = @cutState,GoodsProperty = @goodsProperty,CutSuccess = @cutSuccess,CutPartici = @cutPartici,Limited = @limited,LimitedResidue = @limitedResidue where CutGoodsID = @cutGoodsID";

            int i = dbFactory.DbHelper().Execute(sql, new { @cutName = bargain.CutName, @cutIntro = bargain.CutIntro, @cutPrice = bargain.CutPrice, @cutSort = bargain.CutSort, @beginTime = bargain.BeginTime, @endTime = bargain.EndTime, @cutPeople = bargain.CutPeople, @cutNumber = bargain.CutNumber, @numberLimited = bargain.NumberLimited, @freight = bargain.Freight, @cutState = bargain.CutState, @goodsProperty = bargain.GoodsProperty, @cutSuccess = bargain.CutSuccess, @cutPartici = bargain.CutPartici, @limited = bargain.Limited, @limitedResidue = bargain.LimitedResidue, @cutGoodsID = bargain.CutGoodsID });

            return i;
        }

        /// <summary>
        /// 修改砍价商品状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int EditStatu(int id, int state)
        {
            string sql = $"update CutGoods set CutState=@state where CutGoodsID=@id";

            int i = dbFactory.DbHelper().Execute(sql, new { @state = state, @id = id });

            return i;
        }

        /// <summary>
        /// 显示砍价商品信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="total"></param>
        /// <param name="cutName"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public List<BargainGoods> GetBargainGoods(int page, int limit, out int total, string cutName, int state = -1)
        {
            string sql = $"select B.GoodsImg,A.* from CutGoods A join Goods B on A.GoodsProperty=B.GoodsId where 1=1";

            if (!string.IsNullOrEmpty(cutName))
            {
                sql += $" and CutName like '%{@cutName}%'";
            }
            if (state == 0)
            {
                sql += $" and CutState = @state";
            }
            if (state == 1)
            {
                sql += $" and CutState = @state";
            }

            sql += $" order by BeginTime desc";

            List<BargainGoods> list = dbFactory.DbHelper().Query<BargainGoods>(sql, new { @state = state, @cutName = cutName });

            total = list.Count();

            list = list.Skip((page - 1) * limit).Take(limit).ToList();

            return list;
        }

        /// <summary>
        /// 显示商品列表
        /// </summary>
        /// <returns></returns>
        public List<Goods> GetUserGoods(int page, int limit, out int total, string goodsName)
        {
            string sql = $"select A.GoodsId,A.GoodsName,A.GoodsImg,B.TypeName,C.ParentName,D.CutName,D.CutIntro from Goods A join GoodsType B on A.GoodsTypeId = B.GoodsTypeId join GoodsTypeParent C on B.GoodsTypeParentId = C.ParentId join CutGoods D on A.GoodsId = D.GoodsProperty where 1=1";

            if (!string.IsNullOrEmpty(goodsName))
            {
                sql += $" and A.GoodsName like '%{@goodsName}%'";
            }

            List<Goods> list = dbFactory.DbHelper().Query<Goods>(sql, new { @goodsName = goodsName });

            total = list.Count();

            list = list.Skip((page - 1) * limit).Take(limit).ToList();

            return list;
        }

        /// <summary>
        /// 绑定商品下拉框
        /// </summary>
        /// <returns></returns>
        public List<Goods> GetGoods()
        {
            string sql = $"select GoodsId,GoodsName from Goods";

            List<Goods> list = dbFactory.DbHelper().Query<Goods>(sql);

            return list;
        }

        /// <summary>
        /// 商品属性
        /// </summary>
        /// <returns></returns>
        public List<GoodsAttribute> GetAttributes()
        {
            string sql = $"select A.Picture,C.CutPrice,A.CostPrice,A.OriginalPrice,A.Inventory,A.Weight,A.[Bulk],C.Limited,A.GoodsSerial,C.CutState ,C.GoodsProperty from GoodsAttribute A join Goods B on A.GoodsSerial = B.GoodsNo join CutGoods C on B.GoodsId = C.GoodsProperty";

            List<GoodsAttribute> list = dbFactory.DbHelper().Query<GoodsAttribute>(sql);

            return list;
        }
    }
}
