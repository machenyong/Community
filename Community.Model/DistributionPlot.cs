using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
     /// <summary>
     /// 配送小区
     /// </summary>
    public class DistributionPlot
    {

        //团长表
        public string ColonelName { get; set; }//团长名称
        public string ColonelPhone { get; set; }//团长电话
   ///     public string WareName { get; set; }      //仓库名称
        public string WareAddress { get; set; }//仓库表地址
        public decimal WareLeft { get; set; }//坐标
        public decimal WareRight { get; set; }//坐标


        //配送小区主键
        public int PlotId { get; set; }            /* 主键Id*/
        public string PlotName { get; set; }       /* 小区名称*/
        public int ColonelId { get; set; }         /* 团长信息*/
        public string PathNames { get; set; }      /* 路线*/
        public int WareId { get; set; }            /* 仓库外键*/

    }
}
