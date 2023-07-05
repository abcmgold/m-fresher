using System.Text.Json;

namespace MISA.WebFresher042023.Demo.Core.HandleException
{
    /// <summary>
    /// Lớp baseException
    /// </summary>
    /// CreateBy: BATUAN (14/06/2023)
    public class BaseException
    {

        #region Properties
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// Lỗi hiển thị cho Dev
        /// </summary>
        public string? DevMessage { get; set; }
        /// <summary>
        /// Lỗi cho người dùng
        /// </summary>
        public string? UserMessage { get; set; }
        /// <summary>
        /// TraceId
        /// </summary>
        public string? TraceId { get; set; }
        /// <summary>
        /// Thông tin khác
        /// </summary>
        public string? MoreInfo { get; set; }
        /// <summary>
        /// Lỗi trả về
        /// </summary>
        public object? Errors { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        #endregion

    }
}
