using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Model
{
    //运费模板表
    public class FreightTemplate
    {
        public int  TemplateId      { get; set; }
        public int GoodsTemplateId { get; set; }
        public string TemplateName    { get; set; }
        public string MailingMethod   { get; set; }
        public string Distribution    { get; set; }
        public decimal ExemptionWeight { get; set; }
        public decimal ExemptionMoney  { get; set; }
        public decimal InitialWeight   { get; set; }
        public decimal Freight         { get; set; }
        public decimal AddWeight       { get; set; }
        public decimal Renew           { get; set; }
    }
}
