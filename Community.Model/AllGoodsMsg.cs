using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
    public class AllGoodsMsg
    {
        //运费模板表
        public int TemplateId { get; set; }
        public int GoodsTemplateId { get; set; }
        public string TemplateName { get; set; }
        public string MailingMethod { get; set; }
        public string Distribution { get; set; }
        public decimal ExemptionWeight { get; set; }
        public decimal ExemptionMoney { get; set; }
        public decimal InitialWeight { get; set; }
        public decimal Freight { get; set; }
        public decimal AddWeight { get; set; }
        public decimal Renew { get; set; }


        //规格表
        public int SpecificationId { get; set; }
        public string SpecificationName { get; set; }
        public string GoodsSpecification { get; set; }


        //规格值表
        public int ValueId       { get; set; }
        public string ValueName { get; set; }
        public int SpecificationFkId { get; set; }   //外键（与规格表主键连接）



        //商品属性表
       // public int AttributeId { get; set; }
        public string Specification { get; set; }
        public string Picture { get; set; }
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Inventory { get; set; }
        public decimal Weight { get; set; }
        public decimal Bulk { get; set; }
        public string GoodsSerial { get; set; }


        //轮播图
        public int SlideId { get; set; }
        public string SlideImgUrl { get; set; }
        public int SlideGoodsId { get; set; }


        //商品
        public int GoodsId { get; set; }
        public string GoodsNo { get; set; }
        public string GoodsName { get; set; }
        public decimal GoodsPrice { get; set; }
        public string GoodsIntro { get; set; }
        public int GoodsStock { get; set; }
        public int SaleNum { get; set; }
        public int Status { get; set; }
        public int GoodsStatus { get; set; }        //商品状态  （在仓库1，以买完2，在售卖3，回收站99）
        public int GoodsStatusRecover { get; set; }     //商品状态  (恢复)
        public DateTime CreateTime { get; set; }
        public string GoodsDesc { get; set; }
        public string GoodsImg { get; set; }
        public int Sort { get; set; }
        public DateTime SaleTime { get; set; }
        public string SaleTimeModel { get { return SaleTime.ToString("yyyy-MM-dd hh:ss"); } }
        public string GoodsUnit { get; set; }
        public string Keyword { get; set; }
        public int GoodsTypeId { get; set; }
        public int FreightId { get; set; }
        public int GoodsIntegral { get; set; }  //积分
        public int WId { get; set; }


        public int GoodsTypeParentId { get; set; }
        public string TypeName { get; set; }
        public string TypeIcon { get; set; }



        /// <summary>
        /// 两表联查，父级字段名称
        /// </summary>
        public string ParentName { get; set; }
        public int ParentId { get; set; }


        public string SeckillIntro { get; set; }
        public string SeckillTitle { get; set; }
    }
}
