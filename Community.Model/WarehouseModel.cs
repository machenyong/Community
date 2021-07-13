using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Community.Model
{
    [Table("Warehouse")]
    public class WarehouseModel
    {
        [Key]
        public int WareId { get; set; }                   /* 仓库Id*/
        public string WareName { get; set; }              /* 仓库名称*/
        public string WareAddress { get; set; }           /* 地址*/
        public decimal WareLeft { get; set; }             /* 左坐标*/
        public decimal WareRight { get; set; }            /* 有坐标*/
        public int WareCount { get; set; }                /* 数量*/
        public int WareStatus { get; set; }               /* 状态*/
        public int ColonelId { get; set; }            /* 编号*/
        [NotMapped]
        public int ColonelNumber { get; set; }      //团长人数
        public string LinkName { get; set; }              /* 联系人名称*/
        public string LinkPhone { get; set; }              /* 联系人手机号*/

        [NotMapped]
        public int PlotId { get; set; }                     //id主键
        [NotMapped]
        public string PlotName { get; set; }              //小区名称

    }
}
