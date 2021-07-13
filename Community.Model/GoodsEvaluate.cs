using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
    //评价表
    public class GoodsEvaluate
    {
        public int EvaluateId	 { get; set; }
        public int GoodsId		 { get; set; }
        public string Content		 { get; set; }
        public string EvaluatePic	 { get; set; }
        public DateTime EvaluateTime { get; set; }
        public string GoodsReply	 { get; set; }
        public DateTime ReplyTime	 { get; set; }
        public decimal GoodsScore	 { get; set; }
        public decimal ServeScore { get; set; }
        public int EvaluateState { get; set; }
    }
}
