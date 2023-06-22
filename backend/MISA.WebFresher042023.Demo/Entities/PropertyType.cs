using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher042023.Demo.Entities
{
    /// <summary>
    /// Thực thể của Mã tài sản
    /// </summary>
    /// Created by: BATUAN (13/06/2023)
    public class PropertyType 
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
