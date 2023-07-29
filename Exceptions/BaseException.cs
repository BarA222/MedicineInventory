using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineInventory.Exceptions
{
    public abstract class BaseException : Exception
    {
          private readonly int? errorCode;

        public abstract int HttpStatusCode { get; }

        public BaseException() { }

        public BaseException(string message) : base(message) { }

        public BaseException(int errorCode) : base()
        {
            this.errorCode = errorCode;
        }

        public BaseException(int errorCode, string message) : base(message)
        {
            this.errorCode = errorCode;
        }

        public int? ErrorCode => errorCode;
    }
}