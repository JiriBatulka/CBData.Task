using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CBData.Task.Core.Main
{
    public sealed record ResultDTO
    {
        [JsonPropertyName("resultCode")]
        public required ResultCode ResultCode { get; init; }

        [JsonPropertyName("resultObject")]
        public object? ResultObject { get; init; }
    }

    //more specific codes might be added if needed for the calling systems
    public enum ResultCode
    {
        Undefined = 0,
        Success = 1,
        Failure = 2
    }
}
