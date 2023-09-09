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
        /// <summary>
        /// Ô dữ liệu bị lỗi từ phía front-end
        /// </summary>
        public string? ErrorField { get; set; }
        /// <summary>
        /// Thông tin chứng từ trả về nếu có l
        /// </summary>
        public List<string> DocumentInfo { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        #endregion

    }
}
