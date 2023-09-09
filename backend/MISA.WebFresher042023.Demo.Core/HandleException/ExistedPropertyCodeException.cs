using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.HandleException
{
    public class ExistedPropertyCodeException : Exception
    {
        public int ErrorCode { get; set; }
        public ExistedPropertyCodeException() { }
        public ExistedPropertyCodeException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        public ExistedPropertyCodeException(string message) : base(message) { }
        public ExistedPropertyCodeException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
