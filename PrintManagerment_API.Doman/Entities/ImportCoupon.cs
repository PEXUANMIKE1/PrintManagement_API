using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public  class ImportCoupon : BaseEnities
    {
        public Decimal TotalMoney { get; set; }
        public int ResourcePropertyDetailId { get; set;}
        public ResourcePropertyDetail? ResourcePropertyDetail { get; set;}
        public int EmployeeId { get; set;}
        public User? Employee {  get; set;}
        public string TradingCode { get;set;} = string.Empty;
        public DateTime CreateTime { get; set;}
        public DateTime? UpdateTime { get; set; }

    }
}
