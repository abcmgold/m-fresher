using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher042023.Demo.Entities
{
    /// <summary>
    /// Thực thể của Department
    /// </summary>
    /// Created by: BATUAN (13/06/2023)
    public class Department
    {
        /// <summary>
        /// Id department
        /// </summary>
        [Required]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string DepartmentName { get; set;}
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
