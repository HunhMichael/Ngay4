using day4;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var orders = new List<Order>
        {
            new Order(1, "Nguyễn Văn A"),
            new Order(2, "Trần Thị B"),
            new Order(3, "Lê Văn C")
        };

        var kitchen = new Kitchen();
        var delivery = new Delivery();
        var cskh = new CustomerService();

        // Đăng ký observer bằng EventHandler
        foreach (var order in orders)
        {
            order.OrderStatusChanged += kitchen.OnOrderCreated;
            order.OrderStatusChanged += delivery.OnOrderReadyForDelivery;
            order.OrderStatusChanged += cskh.OnOrderFailedOrCanceled;

            // Đăng ký bằng lambda expression
            order.OrderStatusChanged += (sender, e) =>
            {
                Console.WriteLine($"[LOG] Trạng thái đơn {e.Order.Id} đổi thành '{e.NewStatus}'");
            };
        }

        // Func: chuyển thông tin đơn thành chuỗi
        Func<Order, string> orderFormatter = o => $"#{o.Id} - {o.CustomerName} ({o.Status})";

        // Action: ghi log
        Action<string> logToConsole = msg => Console.WriteLine($"[Console] {msg}");

        // Predicate: kiểm tra đơn đang giao
        Predicate<Order> isDelivering = o => o.Status == "Đang giao";

        Console.WriteLine("\n--- Cập nhật trạng thái ---");
        orders[0].UpdateStatus("Mới");
        orders[1].UpdateStatus("Mới");

        orders[0].UpdateStatus("Đang giao");
        orders[1].UpdateStatus("Hủy");
        orders[2].UpdateStatus("Thất bại");

        orders[0].UpdateStatus("Hoàn tất");

        Console.WriteLine("\n--- Kiểm tra đơn đang giao ---");
        foreach (var o in orders.Where(o => isDelivering(o)))
        {
            logToConsole(orderFormatter(o));
        }

        Console.WriteLine("\n--- Thống kê đơn ---");
        int giaoThanhCong = orders.Count(o => o.Status == "Hoàn tất");
        int huy = orders.Count(o => o.Status == "Hủy" || o.Status == "Thất bại");

        Console.WriteLine($"Tổng đơn giao thành công: {giaoThanhCong}");
        Console.WriteLine($"Tổng đơn bị huỷ/thất bại: {huy}");
    }
}
