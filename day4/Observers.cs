using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day4
{
    public class Kitchen
    {
        public void OnOrderCreated(object sender, OrderEventArgs e)
        {
            if (e.NewStatus == "Mới")
                Console.WriteLine($"[Bếp] Đơn mới: {e.Order}");
        }
    }

    public class Delivery
    {
        public void OnOrderReadyForDelivery(object sender, OrderEventArgs e)
        {
            if (e.NewStatus == "Đang giao")
                Console.WriteLine($"[Shipper] Cần giao: {e.Order}");
        }
    }

    public class CustomerService
    {
        public void OnOrderFailedOrCanceled(object sender, OrderEventArgs e)
        {
            if (e.NewStatus == "Hủy" || e.NewStatus == "Thất bại")
                Console.WriteLine($"[CSKH] Sự cố đơn hàng: {e.Order}");
        }
    }
}
