using CBData.Task.Core.Main;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Timers;

namespace CBData.Task.Core.Orders
{
    //other orders logic might be implemented here in the future
    public sealed class OrdersRepo(IDataAccess _dataAccess)
    {
        public Result<bool, ResultException> AddOrders(List<OrderDTO> ordersDTO)
        {
            Result<List<Order>, ResultException> result = OrdersMapper.Map(ordersDTO);
            return AddData(result);
        }

        public Result<bool, ResultException> AddData(Result<List<Order>, ResultException> data)
        {
            if (data.IsOk)
            {
                for (int i = 0; i < data.Value!.Count; i++)
                {
                    _dataAccess.AddOrder(data.Value![i]);
                }
                return true;
            }
            else
            {
                return data.Error!;
            }
        }
    }
}
