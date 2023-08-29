using MISA.WebFresher042023.Demo.Core.Enum;
using MISA.WebFresher042023.Demo.Core.Resources;
using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher042023.Demo.Core.Dto.Property
{
    /// <summary>
    /// Dto khi thêm của tài sản
    /// </summary>
    /// CreatedBy: BATUAN (18/06/2023)
    public class PropertyCreateDto
    {
        /// <summary>
        /// Mã tài sản
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.PropertyCodeRequired))]
        [MaxLength((int)MaxLength.CodeMaxLength, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.PropertyCodeLength))]
        public string PropertyCode { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.PropertyNameRequired))]
        [MaxLength((int)MaxLength.TextMaxLength, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.PropertyNameLength))]
        public string PropertyName { get; set; }

        /// <summary>
        /// Id bộ phận sử dụng
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.DepartmentIdRequired))]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Id loại tài sản
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.PropertyTypeIdRequired))]
        public Guid PropertyTypeId { get; set; }

        /// <summary>
        /// Mã bộ phận sử dụng
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.DepartmentCodeRequired))]
        [MaxLength((int)MaxLength.CodeMaxLength, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.DepartmentCodeLength))]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên bộ phận sử dụng
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.DepartmentNameRequired))]
        [MaxLength((int)MaxLength.TextMaxLength, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.DepartmentNameLength))]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.PropertyTypeCodeRequired))]
        [MaxLength((int)MaxLength.CodeMaxLength, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.PropertyTypeCodeLength))]
        public string PropertyTypeCode { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.PropertyNameRequired))]
        [MaxLength((int)MaxLength.TextMaxLength, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.PropertyNameLength))]
        public string PropertyTypeName { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.QuantityRequired))]
        public int Quantity { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        [Range(0, 999999999999999999, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.OriginalPriceRequired))]
        public decimal OriginalPrice { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.NumberYearUseRequired))]
        public int NumberYearUse { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn
        /// </summary>
        [Range(0, 100, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.WearRateRequired))]
        public decimal WearRate { get; set; }

        /// <summary>
        /// Giá trị hao mòn năm
        /// </summary>
        [Range(0, 999999999999999999, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.WearRateValueRequired))]
        public decimal WearRateValue { get; set; }

        /// <summary>
        /// Năm theo dõi
        /// </summary>
        public int? FollowYear { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.PurchaseDateRequired))]
        public string PurchaseDate { get; set; }

        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FollowDateRequired))]
        public string FollowDate { get; set; }
    }
}
