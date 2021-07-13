using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
    /// <summary>
    /// 入库和出库表
    /// </summary>
    public class OutInWarehouse
    {



        public int OIId { get; set; }                     /*出入库Id*/
        public string Odd { get; set; }                   /*单号*/
        public string GoodsImg { get; set; }              /*商品图片*/
        public string GoodsName { get; set; }             /*商品名称*/
        public string GoodsSpecification { get; set; }    /*商品规格*/
        public string GoodsNumber { get; set; }           /*商品编号*/
        public int WareId { get; set; }                   /*仓库名称外键*/
        public string InWareNumber { get; set; }          /*入库单号*/
        public int Quantity { get; set; }                 /*数量*/
        public DateTime CreateDate { get; set; }          /*入库时间*/

        public string NoCreateDate { get { return CreateDate.ToString("yyyy-MM-dd hh:mm:ss"); }}//转成string类型

        public int PlotId { get; set; }
        public string WareName { get; set; }  /* 仓库名称*/
        public string PlotName { get; set; }//小区名称
        public string WareAddress { get; set; }//地址
        public decimal WareLeft { get; set; }//坐标
        public decimal WareRight { get; set; }//坐标
    }
}
