using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CBData.Task.Core.Orders
{
    public sealed record OrderDTO
    {
        [JsonPropertyName("productId")]
        public string? ProductId { get; init; }

        [JsonPropertyName("quantity")]
        public int? Quantity { get; init; }
    }
}
