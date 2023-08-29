using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher042023.Demo.Core.Entities
{
    /// <summary>
    /// Thực thể của Mã tài sản
    /// </summary>
    /// Created by: BATUAN (13/06/2023)
    public class PropertyType : BaseEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required]
        public Guid PropertyTypeId { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string PropertyTypeCode { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string PropertyTypeName { get; set; }
        /// <summary>
        /// Tỷ lệ hao mòn mặc định
        /// </summary>
        public decimal WearRate { get; set; }

        /// <summary>
        /// Số năm sử dụng mặc định
        /// </summary>
        public int NumberYearUse { get; set; }

    }
}
