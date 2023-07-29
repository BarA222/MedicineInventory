using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineInventory.Exceptions
{
    public class ConflictException : BaseException
    {
        public override int HttpStatusCode => 409;
        public ConflictException() { }
        public ConflictException(string message) : base(message) { }
        public ConflictException(int errorCode, string message) : base(errorCode, message) { }
    }
}