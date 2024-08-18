using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBData.Task.Core.Main
{
    //this is returned when there is a possible error exception. Implicit operators to make usage simplier
    public readonly struct Result<T, E>
    {
        private readonly bool _success;
        public readonly T? Value;
        public readonly E? Error;

        private Result(T? v, E? e, bool success)
        {
            Value = v;
            Error = e;
            _success = success;
        }

        public bool IsOk => _success;

        public static implicit operator Result<T, E>(T v) => new(v, default, true);
        public static implicit operator Result<T, E>(E e) => new(default, e, false);
    }
}
