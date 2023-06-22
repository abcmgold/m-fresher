namespace MISA.WebFresher042023.Demo.Entities
{
     /// <summary>
     /// Class lưu lỗi trả về
     /// </summary>
     /// Created by: BATUAN (13/06/2023)
    public class Error
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// Message cho dev
        /// </summary>
        public string DevMsg { get; set; }
        /// <summary>
        /// Message cho user
        /// </summary>
        public string UserMsg { get; set; }
        /// <summary>
        /// Thông tin thêm
        /// </summary>
        public string MoreInfo { get; set; }
        /// <summary>
        /// Trace Id
        /// </summary>
        public string TraceId { get; set; }
    }
}
