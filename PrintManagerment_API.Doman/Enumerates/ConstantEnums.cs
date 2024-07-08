using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.Enumerates
{
    public class ConstantEnums
    {
        public enum KpiPeriodEnum 
        { 
            Month = 1,   //Tháng
            Quarter = 2, //Quý
            Year = 3,    //Năm
        }
        public enum BillStatus
        {
            Pending = 1,      // Chờ xử lý
            Approved = 2,     // Đã phê duyệt
            Paid = 3,         // Đã thanh toán
            Unpaid = 4,       // Chưa thanh toán
            Cancelled = 5,    // Đã hủy
            Refunded = 6,     // Đã hoàn tiền
        }
        public enum DeliveryStatus
        {
            Pending = 1,       // Chờ xử lý
            Scheduled = 2,     // Đã lên lịch
            InTransit = 3,     // Đang vận chuyển
            OutForDelivery = 4,// Đang giao hàng
            Delivered = 5,     // Đã giao hàng
            FailedDelivery = 6,// Giao hàng thất bại
            Returned = 7,      // Đã trả lại
            Cancelled  = 8     // Đã hủy
        }
        public enum ResourceType
        {
            Equipment = 1,   // Thiết bị
            Material = 2,    // Vật liệu
            Tool = 3,        // Công cụ
            Software = 4     // Phần mềm
        }

        public enum ResourceStatus
        {
            Available = 1,   // Sẵn sàng sử dụng
            Maintenance = 2, // Cần bảo trì
        }
        public enum ProjectStatus
        {
            Designing = 1,  //đang thiết kế
            Printing = 2,   //đang in
            Completed = 3,  //Đã Hoàn thành
        }
        public enum DesignStatus
        {
            Pending = 1,        // Chờ xử lý
            InProgress = 2,     // Đang thiết kế
            Completed = 3,      // Đã hoàn thành
            Approved = 4,       // Đã phê duyệt
            Rejected = 5        // Bị từ chối
        }
        public enum PrintJobStatus
        {
            Pending = 1,        // Chờ xử lý
            InProgress = 2,     // Đang in
            Completed = 3,      // Đã hoàn thành
            OnHold = 4,         // Đang tạm dừng
            Cancelled = 5       // Đã hủy
        }
    }
}
