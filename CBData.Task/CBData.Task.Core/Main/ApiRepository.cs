using CBData.Task.Core.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CBData.Task.Core.Main
{
    //other domain types would be here (e.g. customers)
    public sealed class ApiRepository(OrdersRepo orders)
    {
        public OrdersRepo Orders { get; } = orders;
    }
}
