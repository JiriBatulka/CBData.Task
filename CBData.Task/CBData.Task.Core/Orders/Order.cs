using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CBData.Task.Core.Orders
{
    //sealed classes are faster since a .net version 6 or so
    public sealed record Order
    {
        public required string ProductId { get; init; }
        public required int Quantity { get; set; }
    }
}
