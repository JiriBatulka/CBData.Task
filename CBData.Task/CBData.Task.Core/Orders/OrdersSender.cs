using CBData.Task.Core.Main;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;

namespace CBData.Task.Core.Orders
{
    //this can be singleton as we need only one instance to access data and write them to console
    //IDisposable just to be sure
    public sealed class OrdersSender : IDisposable
    {
        private readonly System.Timers.Timer _sendOrdersTimer;
        private readonly IDataAccess _dataAccess;
        private readonly ILogger<OrdersSender> _logger;

        public OrdersSender(IDataAccess dataAccess, ILogger<OrdersSender> logger)
        {
            _logger = logger;
            _sendOrdersTimer = new(20000);
            _sendOrdersTimer.Elapsed += SendOrders;
            _sendOrdersTimer.AutoReset = true;
            _sendOrdersTimer.Enabled = true;
            _dataAccess = dataAccess;
        }

        private void SendOrders(Object? source, ElapsedEventArgs e)
        {
            try
            {
                Console.WriteLine(JsonSerializer.Serialize(_dataAccess.GetOrders()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in SendOrders. Exception message: {ExceptionMessage}", ex.Message);
            }
        }

        public void Dispose()
        {
            _sendOrdersTimer.Stop();
            _sendOrdersTimer.Dispose();
        }
    }
}
