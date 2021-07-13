using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Community.Model
{
    [Table("SeckillConfign")]
    public class SeckillConfign
    {
        [Key]
        public int ConfignID { get; set; }      //秒杀配置ID

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

        public string ConfignImg { get; set; }  //秒杀配置轮播图

        public int ConfignState { get; set; }   //秒杀配置状态

        public DateTime CreateTime { get; set; } //秒杀配置创建时间

        [NotMapped]
        public string CTime { get { return CreateTime.ToString("yyyy-MM-dd HH:mm:ss"); } }
    }
}
