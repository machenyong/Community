using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
    //团长等级表
    public class ColonelGrade
    {
        public int GradeID { get; set; }
        public string GradeName { get; set; }
        public int GradeSuffer { get; set; }
        public decimal Rewardratio { get; set; }
        public DateTime CreateTime { get; set; }
        public int Status { get; set; }
    }
}
