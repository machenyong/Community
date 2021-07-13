using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
    //佣金流水
    public class Commission
    {
        public int CommissionID { get; set; }
        public int CommTypeID { get; set; }
        public string Img { get; set; }
        public string CommGetName { get; set; }
        public decimal IncomeComm { get; set; }
        public int Status { get; set; }
        public DateTime StockDate { get; set; }
        public int OrderStatus { get; set; }
        public string CommTypeName { get; set; }
    }
}
