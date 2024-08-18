using CBData.Task.Core.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBData.Task.Core.Main
{
    //this would probably access the database or redis irl
    public sealed class DataAccess : IDataAccess
    {
        public void AddOrder(Order order)
        {
            if (Data.Orders.TryAdd(order.ProductId, order) is false)
            {
                Data.Orders[order.ProductId].Quantity += order.Quantity;
            }
        }

        public List<Order> GetOrders()
        {
            List<Order> orders = [];
            foreach (var order in Data.Orders)
            {
                orders.Add(order.Value);
            }
            return orders;
        }
    }
}
