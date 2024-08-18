using CBData.Task.Core.Orders;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBData.Task.Core.Main
{
    //this would probably be database or redis or something
    internal static class Data
    {
        internal static ConcurrentDictionary<string, Order> Orders { get; } = new();
    }
}
