using System;
using System.ComponentModel.DataAnnotations;

namespace Community.Model
{
    /// <summary>
    /// 库存表
    /// </summary>
    public class Stock
    {
        [Key]
        public int StockID { get; set; }                 /*库存Id*/
        public int StockNum { get; set; }                /*现有库存*/
        public int WareID { get; set; }                  /*仓库管理名称*/
        public int GoodsID { get; set; }                 /*商品外键ID*/
        public int InRepertory { get; set; }             /*入库数量*/
        public int OutRepertory { get; set; }            /*出库数量*/


        public int GoodsId { get; set; }//商品主键
        public string GoodsNo { get; set; }//商品编号
        public string GoodsImg { get; set; }//商品图片
        public string GoodsName { get; set; }//商品名称
        public string WareName { get; set; }//仓库名称
        public DateTime CreateTime { get; set; }//商品创建时间

        public string NoCreateTime { get { return CreateTime.ToString("yyyy-MM-dd hh:mm:ss"); } }

    }
}
