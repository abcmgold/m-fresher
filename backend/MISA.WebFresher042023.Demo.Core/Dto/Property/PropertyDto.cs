using MISA.WebFresher042023.Demo.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Dto.Property
{

    /// <summary>
    /// Thực thể của tài sản khi trả về cho người dùng
    /// </summary>
    /// CreatedBy: BATUAN (18/06/2023)
    public class PropertyDto
    {
        /// <summary>
        /// Id tài sản
        /// </summary>
        [Required]
        public Guid PropertyId { get; set; }
        /// <summary>
        /// Mã tài sản
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string PropertyCode { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        [Required]
        [MaxLength((int)MaxLength.TextMaxLength)]
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
        [Required]
        [MaxLength((int)MaxLength.CodeMaxLength)]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên bộ phận sử dụng
        /// </summary>
        [Required]
        [MaxLength((int)MaxLength.TextMaxLength)]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        [Required]
        [MaxLength((int)MaxLength.CodeMaxLength)]
        public string PropertyTypeCode { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        [Required]
        [MaxLength((int)MaxLength.TextMaxLength)]
        public string PropertyTypeName { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        [Required]
        [Range(minimum: 0, 999999999999999999)]
        public int Quantity { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        [Required]
        [Range(minimum: 0, maximum: 999999999999999999)]
        public decimal OriginalPrice { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        [Required]
        [Range(minimum: 0, int.MaxValue)]
        public int NumberYearUse { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn
        /// </summary>
        [Required]
        [Range(minimum: 0, maximum: 100)]
        public decimal WearRate { get; set; }

        /// <summary>
        /// Giá trị hao mòn năm
        /// </summary>
        [Required]
        [Range(minimum: 0, maximum: 999999999999999999)]
        public decimal WearRateValue { get; set; }

        /// <summary>
        /// Năm theo dõi
        /// </summary>
        [Range(2000, 9999)]
        public int? FollowYear { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        [Required]
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        [Required]
        public DateTime FollowDate { get; set; }
    }
}
