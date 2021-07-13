using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
    public class UserInfo
    {
        public int UserId		 { get; set; }
        public int UserAccount  { get; set; }   //登录名
        public int UserPassword { get; set; }
        public int UserName     { get; set; }  //用户真实名称
        public int UserExplain	 { get; set; }
        public int UserStatus	 { get; set; }
        public int NickName     { get; set; }
        public int UserPhone { get; set; }
        public int UserAddress { get; set; }
        public int RoleId { get; set; }
    }              
}                  
