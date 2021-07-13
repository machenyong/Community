using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Community.Model
{
    [Table("CutList")]
    public class BargainList
    {
        [Key]
        public int CutListId { get; set; }

        public int CutGoods { get; set; }

        public DateTime BeginTime { get; set; }
        [NotMapped]
        public string BTime { get { return BeginTime.ToString("yyyy-MM-dd HH:mm"); } }
        [NotMapped]
        public string BTime1 { get { return BeginTime.ToString("yyyy-MM-dd HH:mm:ss"); } }

        public DateTime EndTime { get; set; }
        [NotMapped]
        public string ETime { get { return EndTime.ToString("yyyy-MM-dd HH:mm"); } }

        public int State { get; set; }

        public int AllNumber { get; set; }

        public int ResidueNumber { get; set; }

        public int UserId { get; set; }

        public string CutName { get; set; }
        public string CutIntro { get; set; }
        public decimal CutPrice { get; set; }
        public string GoodsImg { get; set; }
        public string UserImg { get; set; }
        public string UserName { get; set; }
    }
}
