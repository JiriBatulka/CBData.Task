using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CBData.Task.Core.Main
{
    //passed in result type as error so I can get message and http status code later
    public class ResultException()
    {
        public required string Message { get; init; }
        public required HttpStatusCode HttpStatusCode { get; init; }
    }
}
