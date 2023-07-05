using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Dto.Department
{
    /// <summary>
    /// Lớp đại diện cho dữ liệu department trả về
    /// </summary>
    /// CreateBy: BATUAN(20/06/2023)
    public class DepartmentCreateDto
    {
        /// <summary>
        /// Id Department
        /// </summary>
        [Required]
        [MaxLength(36)]
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
        [MaxLength(255)]
        public string DepartmentName { get; set; }
    }
}
