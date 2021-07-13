using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Community.Model
{
    [Table("CutGoods")]
    public class BargainGoods
    {
        [Key]
        public int CutGoodsID { get; set; }
        public string CutName { get; set; }
        public string CutIntro { get; set; }
        public decimal CutPrice { get; set; }
        public int CutSort { get; set; }
        public DateTime BeginTime { get; set; }
        [NotMapped]
        public string BTime { get { return BeginTime.ToString("yyyy-MM-dd"); } }

        public DateTime EndTime { get; set; }
        [NotMapped]
        public string ETime { get { return EndTime.ToString("yyyy-MM-dd"); } }

        public int CutPeople { get; set; }
        public int CutNumber { get; set; }
        public int NumberLimited { get; set; }
        public int Freight { get; set; }
        public int CutState { get; set; }
        public int GoodsProperty { get; set; }
        public int CutSuccess { get; set; }
        public int CutPartici { get; set; }
        public int Limited { get; set; }
        public int LimitedResidue { get; set; }

        [NotMapped]
        public string GoodsImg { get; set; }
    }
}
