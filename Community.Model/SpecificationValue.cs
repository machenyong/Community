using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
    //规格值表
    public class SpecificationValue
    {
        public int ValueId       { get; set; }
        public string ValueName     { get; set; }
        public int SpecificationFkId { get; set; }   //外键（与规格表主键连接）
    }
}
