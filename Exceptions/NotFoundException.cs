using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineInventory.Exceptions
{
    public class NotFoundException: BaseException
    {
        private readonly object data;

        public override int HttpStatusCode => 404;
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, object data) : base(message)
        {
            this.data = data;
        }
    }
}