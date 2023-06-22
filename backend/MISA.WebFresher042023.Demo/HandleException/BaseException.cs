using System.Text.Json;

namespace MISA.WebFresher042023.Demo.HandleException
{
    public class BaseException
    {
        #region Properties
        public int ErrorCode { get; set; }
        public string? DevMessage { get; set; }
        public string? UserMessage { get; set; }
        public string? TraceId { get; set; }
        public string? MoreInfo { get; set; }
        public object? Errors { get; set; }
        #endregion

        #region Methos
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        #endregion

    }
}
