using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Community.Model
{
    public class SeckillGoodsModel
    {
        public int SeckillID { get; set; }      //秒杀商品ID

        public int ConfignID { get; set; }      //秒杀配置ID

        public string SeckillTitle { get; set; }          //秒杀标题

        public string SeckillIntro { get; set; }          //秒杀简介

        public decimal UnitPrice { get; set; }      //单价

        public decimal PriceSpike { get; set; }      //秒杀价

        public int Limited { get; set; }      //限量

        public int LimitedResidue { get; set; }      //限量剩余

        public int SeckillState { get; set; }          //秒杀状态

        public int GoodsID { get; set; }            //商品ID

        public DateTime CreateTime { get; set; }  //创建时间

        [NotMapped]
        public string CTime { get { return CreateTime.ToString("yyyy-MM-dd HH:mm:ss"); } }

        public string ConfignName { get; set; } //秒杀配置名称

        public DateTime BeginTime { get; set; } //秒杀配置开始时间
        [NotMapped]
        public string BTime { get { return BeginTime.ToString("HH:mm"); } }
        [NotMapped]
        public string BTime1 { get { return BeginTime.ToString("yyyy-MM-dd"); } }

        public DateTime EndTime { get; set; }   //秒杀配置结束时间
        [NotMapped]
        public string ETime { get { return EndTime.ToString("HH:mm"); } }
        [NotMapped]
        public string ETime1 { get { return EndTime.ToString("yyyy-MM-dd"); } }

        public string GoodsImg { get; set; }
        public string GoodsName { get; set; }
    }
}
