using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
    public class GoodsEvaluateMessage
    {
        //评价表
        public int EvaluateId { get; set; }                           //主健Id
        public int GoodsId { get; set; }                              //商品编号外键
        public string Content { get; set; }                           //评价内容	
        public string EvaluatePic { get; set; }                       //评价图片
        public DateTime EvaluateTime { get; set; }                    //评价时间   
        public string GoodsReply { get; set; }                        //店铺回复内容
        public DateTime ReplyTime { get; set; }                       //回复时间   
        public decimal GoodsScore { get; set; }                       //商品评分
        public decimal ServeScore { get; set; }                       //服务评分
        public int EvaluateState { get; set; }


        //商品信息
        public string GoodsName { get; set; }
        public string GoodsImg { get; set; }

        //用户登录名
        public string UserAccount { get; set; }   //登录名

    }
}
