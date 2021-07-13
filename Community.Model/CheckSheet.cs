using System;

namespace Community.Model
{
    public class CheckSheet
    {
        public int CheckId { get; set; }
        public string CheckCode { get; set; }       /*  盘点单号*/
        public int CheckNum { get; set; }           /*  原数量*/
        public int CheckAfterNum { get; set; }      /*  盘点后数量*/
        public string CheckPeople { get; set; }     /*  盘点人*/
        public DateTime CheckTime { get; set; }     /*  盘点时间*/
        public string CheckRemark { get; set; }     /*  盘点备注*/
        public int WId { get; set; }                /*  仓库外键*/
    }
}
