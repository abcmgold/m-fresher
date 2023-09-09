using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.HandleException
{
    public class UserException : Exception
    {
        public int ErrorCode { get; set; }
        public string? ErrorField { get; set; }
        public UserException() { }
        public UserException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        public UserException(string message) : base(message) { }
        public UserException(string message, int errorCode) : base(message) { 
            ErrorCode = errorCode;
        }
        public UserException(string message, int errorCode, string errorField) : base(message)
        {
            ErrorCode = errorCode;
            ErrorField = errorField;
        }
    }
}
