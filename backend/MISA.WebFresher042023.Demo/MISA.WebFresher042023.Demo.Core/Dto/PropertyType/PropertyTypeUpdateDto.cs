using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Dto.PropertyType
{
    /// <summary>
    /// Lớp thực thể đại diện cho mã loại tài sản khi đưa thông tin về cho người dùng
    /// </summary>
    public class PropertyTypeUpdateDto
    {
        /// <summary>
        /// Id mã loại tài sản
        /// </summary>
        [Required]
        [MaxLength(36)]
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
        [MaxLength(255)]
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
