namespace MISA.WebFresher042023.Demo.Core.HandleException
{
    public class NotFoundException : Exception
    {
        public int ErrorCode { get; set; }
        public NotFoundException() { }
        public NotFoundException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
