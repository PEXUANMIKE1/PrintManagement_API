using PrintManagerment_API.Doman.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Entities
{
    public class KeyPerformanceIndicators:BaseEnities
    {
        public int EmployeeId { get; set; } //KPI của nhân viên theo id user
        public User? Employee { get; set; }
        public string IndicatorName { get; set; } = string.Empty; //Tên chỉ số KPI
        public int Target { get; set;} //số project được giao đến tay khách hàng
        public int ActuallyAchieved { get; set;} //số project chốt được thực tế từ khách hàng
        public ConstantEnums.KpiPeriodEnum Period { get; set; } //Kpi theo tháng/quý/năm
        public bool AchieveKPI {  get; set;} //Có hoàn thành KPI hay không
    }
}
