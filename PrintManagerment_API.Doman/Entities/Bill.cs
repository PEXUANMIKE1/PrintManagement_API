using PrintManagerment_API.Doman.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class Bill :BaseEnities
    {
        public string BillName { get; set; } = string.Empty;
        public ConstantEnums.BillStatus BillStatus { get; set; } = ConstantEnums.BillStatus.Pending;
        public decimal TotalMoney { get; set; }
        public int ProjectId { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public string TradingCode { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int EmployeeId { get; set; }
        public User? Employee {  get; set; }
        
    }
}
