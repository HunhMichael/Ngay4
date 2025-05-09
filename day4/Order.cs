using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day4
{
    public class OrderEventArgs : EventArgs
    {
        public Order Order { get; }
        public string NewStatus { get; }

        public OrderEventArgs(Order order, string newStatus)
        {
            Order = order;
            NewStatus = newStatus;
        }
    }

    public class Order
    {
        public int Id { get; }
        public string CustomerName { get; }
        public string Status { get; private set; }

        public event EventHandler<OrderEventArgs> OrderStatusChanged;

        public Order(int id, string name)
        {
            Id = id;
            CustomerName = name;
            Status = "Khởi tạo";
        }

        public void UpdateStatus(string newStatus)
        {
            Status = newStatus;
            OrderStatusChanged?.Invoke(this, new OrderEventArgs(this, newStatus));
        }

        public override string ToString() => $"[Đơn {Id}] {CustomerName} - {Status}";
    }
    }

