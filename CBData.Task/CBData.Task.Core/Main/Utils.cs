using CBData.Task.Core.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CBData.Task.Core.Main
{
    public static class Utils
    {
        //In case something unexpected is wrong with the json, it is most probably a client exception
        public static async Task<Result<List<T>, ResultException>> DeserializeJsonFromClientAsync<T>(Stream json, CancellationToken ct = default)
        {
            try
            {
                List<T>? result = await JsonSerializer.DeserializeAsync<List<T>>(json, cancellationToken: ct);
                if (result is not null)
                {
                    return result;
                }
                else
                {
                    return new ResultException()
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "There was a problem with a json body"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultException()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "There was a problem with a json body: " + ex.Message
                };
            }
        }
    }
}
