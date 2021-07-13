using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
    ///核销员
    public class Salesperson
    {
        public int SalespersonID { get; set; }  //主键
        public string Img { get; set; }             //团员图片
        public string MemberName { get; set; }  //团员名称
        public string MemberPhone { get; set; } //团员手机号
        public int ColonelId { get; set; }      //团长外键
    }
}
