using CBData.Task.Core.Orders;

namespace CBData.Task.Core.Main
{
    public interface IDataAccess
    {
        public void AddOrder(Order orders);
        public List<Order> GetOrders();
    }
}