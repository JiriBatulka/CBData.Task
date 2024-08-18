using CBData.Task.Core.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;

namespace CBData.Task.Core.Orders
{
    //the only resposibility is to access a static collection so this whole class might be static, we need only one instance
    public sealed class OrdersSender : IDisposable
    {
        private readonly System.Timers.Timer _sendOrdersTimer;
        private readonly IDataAccess _dataAccess;

        public OrdersSender(IDataAccess dataAccess)
        {
            _sendOrdersTimer = new(20000);
            _sendOrdersTimer.Elapsed += SendOrders;
            _sendOrdersTimer.AutoReset = true;
            _sendOrdersTimer.Enabled = true;
            _dataAccess = dataAccess;
        }

        private void SendOrders(Object? source, ElapsedEventArgs e)
        {
            Console.WriteLine(JsonSerializer.Serialize(_dataAccess.GetOrders()));
        }

        public void Dispose()
        {
            _sendOrdersTimer.Stop();
            _sendOrdersTimer.Dispose();
        }
    }
}
