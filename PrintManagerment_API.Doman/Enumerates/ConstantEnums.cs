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
            Pending = 0, //chờ xử lý
            Unpaid = 1, // chưa thanh toán
            Paid = 2,   // Đã thanh toán
        }

        public enum DeliveryStatus
        {
            Pending = 1,     // Chờ xử lý
            InTransit = 2,   // Đang giao hàng
            Delivered = 3,   // Giao hàng thành công
            Refused = 4 ,    // Từ chối nhận
            Returned = 5     // Đã trả lại
        }

        public enum ResourceType
        {
            Paper = 1,       // Giấy
            Supplies = 2,    // Vật liệu
            Equipment = 3,   // Thiết bị
            Software = 4     // Phần mềm
        }

        public enum ResourceStatus
        {
            Available = 1,   // Sẵn sàng sử dụng
            Maintenance = 2, // Cần bảo trì
        }

        public enum ProjectStatus
        {
            Designing = 1,   // Đang thiết kế
            Designed = 2,    // Đã thiết kế - 25%
            ConfirmPrint = 3,// Xác nhận in ấn - 50%
            Printing = 4,    // Đang in - 75%
            Completed = 5    // Đã Hoàn thành - 100%
        }

        public enum DesignStatus
        {
            Completed = 1,      // Đã hoàn thành
            NotYetApproved = 2, // Chưa được phê duyệt
            Approved = 3,       // Đã phê duyệt
            Rejected = 4        // Bị từ chối
        }

        public enum PrintJobStatus
        {
            Pending = 1,        // Chờ xử lý
            InProgress = 2,     // Đang in
            Completed = 3,      // Đã hoàn thành
            Cancelled = 4       // Đã hủy
        }
    }
}
