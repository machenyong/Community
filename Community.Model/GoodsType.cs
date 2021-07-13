using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
    public class GoodsType
    {
        public int GoodsTypeId	 { get; set; }
        public int GoodsTypeParentId { get; set; }
        public string TypeName	 { get; set; }
        public string TypeIcon	 { get; set; }
        public int Sort		 { get; set; }
        public int Status { get; set; }


        /// <summary>
        /// 两表联查，父级字段名称
        /// </summary>
        public string ParentName { get; set; }
        public int ParentId { get; set; }
    }
}
