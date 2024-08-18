using CBData.Task.Core.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CBData.Task.Core.Orders
{
    //mighe have used AutoMapper library for this but custom mapper is usually faster
    public static class OrdersMapper
    {
        public static Result<List<Order>, ResultException> Map(List<OrderDTO> orders)
        {
            List<Order> result = [];
            for (int i = 0; i < orders.Count; i++) 
            {
                Result<Order, ResultException> order = Map(orders[i]);
                if (order.IsOk)
                {
                    result.Add(order.Value!);
                }
                else
                {
                    return order.Error!;
                }
            }
            return result;
        }

        private static Result<Order, ResultException> Map(OrderDTO order)
        {
            //it would also be possible to implement custom exception with http status code and message or something and catch it in exception handling middleware,
            //but communication through exceptions is slow
            if (string.IsNullOrEmpty(order.ProductId) || order.Quantity is null)
            {
                return new ResultException()
                {
                    Message = "ProductId or Quantity is missing from one of the json body items",
                    HttpStatusCode = HttpStatusCode.BadRequest
                };
            }
            else
            {
                return new Order()
                {
                    ProductId = order.ProductId,
                    Quantity = order.Quantity.Value
                };
            }
        }
    }
}
