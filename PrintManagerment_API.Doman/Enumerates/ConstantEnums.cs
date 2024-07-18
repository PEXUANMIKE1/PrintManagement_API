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
            Month,   // Tháng
            Quarter, // Quý
            Year,    // Năm
        }

        public enum BillStatus
        {
            Pending,      // Chờ xử lý
            Approved,     // Đã phê duyệt
            Paid,         // Đã thanh toán
            Unpaid,       // Chưa thanh toán
            Cancelled,    // Đã hủy
            Refunded      // Đã hoàn tiền
        }

        public enum DeliveryStatus
        {
            Pending,       // Chờ xử lý
            Scheduled,     // Đã lên lịch
            InTransit,     // Đang vận chuyển
            OutForDelivery,// Đang giao hàng
            Delivered,     // Đã giao hàng
            FailedDelivery,// Giao hàng thất bại
            Returned,      // Đã trả lại
            Cancelled      // Đã hủy
        }

        public enum ResourceType
        {
            Equipment,   // Thiết bị
            Material,    // Vật liệu
            Tool,        // Công cụ
            Software     // Phần mềm
        }

        public enum ResourceStatus
        {
            Available,   // Sẵn sàng sử dụng
            Maintenance, // Cần bảo trì
        }

        public enum ProjectStatus
        {
            Designing = 1,  // Đang thiết kế
            Printing = 2,   // Đang in
            Completed = 3   // Đã Hoàn thành
        }

        public enum DesignStatus
        {
            Pending,        // Chờ xử lý
            InProgress,     // Đang thiết kế
            Completed,      // Đã hoàn thành
            Approved,       // Đã phê duyệt
            Rejected        // Bị từ chối
        }

        public enum PrintJobStatus
        {
            Pending,        // Chờ xử lý
            InProgress,     // Đang in
            Completed,      // Đã hoàn thành
            OnHold,         // Đang tạm dừng
            Cancelled       // Đã hủy
        }
    }
}
