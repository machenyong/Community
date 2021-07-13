using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//引用
using Community.IRepository;
using Community.Common;
using Community.Model;

namespace Community.Repository
{
    public class GoodsTypeRepository : IGoodsTypeRepository
    {
        //实例化工厂
        DbFactory dbFactory = new DbFactory();

        /// <summary>
        /// 获取商品类型数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        public List<GoodsType> GetGoodsType(string name, string states)
        {
            //sql语句
            string sql = "select ParentId,ParentName,GoodsTypeParentId,GoodsTypeId,TypeName,TypeIcon,Sort,[Status] from GoodsType a join GoodsTypeParent b on a.GoodsTypeParentId=b.ParentId where 1=1";
            
            List<GoodsType> goodsType = new List<GoodsType>();

            #region 条件查询
            if (!string.IsNullOrEmpty(name))
            {
                sql += $" and a.TypeName like '%{@name}%'";
                if (!string.IsNullOrEmpty(states))
                {
                    sql += " and a.Status = @state";
                    goodsType = dbFactory.DbHelper().Query<GoodsType>(sql, new { @name = name, @state = Convert.ToInt32(states) });
                }
                else
                {
                    goodsType = dbFactory.DbHelper().Query<GoodsType>(sql, new { @name = name });
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(states))
                {
                    sql += " and a.Status = @state";
                    goodsType = dbFactory.DbHelper().Query<GoodsType>(sql, new { @state = Convert.ToInt32(states) });
                }
                else
                {
                    goodsType = dbFactory.DbHelper().Query<GoodsType>(sql);
                }
            }
            #endregion

            //返回查询数据
            return goodsType;
        }


        /// <summary>
        /// 删除商品类型数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelGoodsType(int id)
        {
            string sql = "delete from GoodsType where GoodsTypeId=@id";
            //调用删除方法
            return  dbFactory.DbHelper().Execute(sql,new {@id=id });
        }


        /// <summary>
        /// 添加商品类型
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public int AddGoodsType(GoodsType goods)
        {
            string sql = "insert into GoodsType values(@prentId,@typeName,@img,@sort,@statu)";
           
            return dbFactory.DbHelper().Execute(sql, 
                new { @prentId=goods.GoodsTypeParentId,
                      @typeName=goods.TypeName,
                      @img=goods.TypeIcon,
                      @sort=goods.Sort,
                      @statu=goods.Status 
                });
        }


        /// <summary>
        /// 获取商品类型的父级
        /// </summary>
        /// <returns></returns>
        public List<GoodsTypeParent> GetParent()
        {
            string sql = "select * from GoodsTypeParent";
            //返回数据
            return dbFactory.DbHelper().Query<GoodsTypeParent>(sql);
        }


        /// <summary>
        /// 查询不是该父级类型下的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<GoodsType> ShowSubcatalog(int id)
        {
            string sql = "select * from GoodsType where GoodsTypeParentId not in (@id)";

            return dbFactory.DbHelper().Query<GoodsType>(sql,new { @id=id});
        }


        /// <summary>
        /// 添加子目录
        /// </summary>
        /// <returns></returns>
        public int AddSubcatalog(int GoodsTypeId,int prentId)
        {
            string sql = "update GoodsType set GoodsTypeParentId=@GoodsTypeId where GoodsTypeId=@prentId";

            int i = dbFactory.DbHelper().Execute(sql,new { @GoodsTypeId= GoodsTypeId, @prentId = prentId });
            return i;
        }


        /// <summary>
        /// 修改商品类型
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public int editGoodsTypeMsg(GoodsType goods)
        {
            string sql = "update GoodsType set GoodsTypeParentId=@ParentId,TypeName=@typeName,Sort=@Sort,Status=@Status where GoodsTypeId=@GoodsTypeId";

            var param = new
            {
                @ParentId=goods.GoodsTypeParentId,
                @typeName=goods.TypeName,
                @TypeIcon=goods.TypeIcon,
                @Sort=goods.Sort,
                @Status=goods.Status,
                @GoodsTypeId=goods.GoodsTypeId
            };

            return dbFactory.DbHelper().Execute(sql, param);
        }


        /// <summary>
        ///修改商品类型状态
        /// </summary>
        /// <param name="goodsTypeId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int editGoodsTypeStatus(int goodsTypeId, int Status)
        {
            string sql = "update GoodsType set Status=@Status where GoodsTypeId=@goodsTypeId";

            return dbFactory.DbHelper().Execute(sql,new {
                @Status=Status,
                @goodsTypeId= goodsTypeId
            });
        }
    }
}
