using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
    /// <summary>
    /// 盘点管理，只用于盘点管理中的详情表
    /// </summary>
    public class TakeStockModel
    {
        public int CheckId { get; set; }
        public string CheckCode { get; set; }       /*  盘点单号*/
        public int CheckNum { get; set; }           /*  原数量*/
        public int CheckAfterNum { get; set; }      /*  盘点后数量*/
        public string CheckPeople { get; set; }     /*  盘点人*/
        public DateTime CheckTime { get; set; }     /*  盘点时间*/
        public string CheckRemark { get; set; }     /*  盘点备注*/
        public int WId { get; set; }                /*  仓库外键*/
        public string NoCheckTime { get { return CheckTime.ToString("yyyy-MM-dd HH:mm:ss"); } }     /*  修改类型--盘点时间*/

        public string WareName { get; set; }          /*  仓库名称*/
        public string GoodsID { get; set; }
        public string GoodsImg { get; set; }
        public string GoodsName { get; set; }
        public string GoodsNo { get; set; }
        public string GoodsSpecification { get; set; }
        public string GoodsStock { get; set; }
        public string StockNum { get; set; }
    }
}
