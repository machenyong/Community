using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
    public class CommunityGroup
    {
        [Key]
        public int GroupId { get; set; }
        public bool GroupStatus { get; set; }
        public string Notice { get; set; }
        public DateTime StopStartTime { get; set; }
        public DateTime StopEndTime { get; set; }
        public string TemaApplyPoster { get; set; }
        public string StopPoster { get; set; }
        public string SupplierApplyPoste { get; set; }
        public string SendType { get; set; }
        public string TeamheaderName { get; set; }
        public decimal CoverageArea { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal HintPrice { get; set; }
        public decimal Commission { get; set; }
    }
}
