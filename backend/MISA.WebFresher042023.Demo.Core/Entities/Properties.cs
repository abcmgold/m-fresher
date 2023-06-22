using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher042023.Demo.Core.Entities
{
    /// <summary>
    /// Thực thể của tài sản
    /// </summary>
    /// CreatedBy: BATUAN (14/06/2023)
    public class Properties
    {
        /// <summary>
        /// Id tài sản
        /// </summary>
        public Guid PropertyId { get; set; }
        /// <summary>
        /// Mã tài sản
        /// </summary>
        [Required]
        public string PropertyCode { get; set; }
        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// Id bộ phận sử dụng
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Id loại tài sản
        /// </summary>
        public Guid PropertyTypeId { get; set; }
        /// <summary>
        /// Mã bộ phận sử dụng
        /// </summary>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// Tên bộ phận sử dụng
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public string PropertyTypeCode { get; set; }
        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string PropertyTypeName { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Nguyên giá
        /// </summary>
        public decimal OriginalPrice { get; set; }
        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public int NumberYearUse { get; set; }
        /// <summary>
        /// Tỷ lệ hao mòn
        /// </summary>
        public decimal WearRate { get; set; }
        /// <summary>
        /// Giá trị hao mòn năm
        /// </summary>
        public decimal WearRateValue { get; set; }
        /// <summary>
        /// Năm theo dõi
        /// </summary>
        public int? FollowYear { get; set; }
        /// <summary>
        /// Ngày mua
        /// </summary>
        public DateTime PurchaseDate { get; set; }
        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        public DateTime FollowDate { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }
        /// <summary>
        /// Ngày sửa đổi
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string? ModifiedBy { get; set; }
    }

}
