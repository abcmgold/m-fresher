using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher042023.Demo.Core.Entities
{
    /// <summary>
    /// Thực thể của Department
    /// </summary>
    /// Created by: BATUAN (13/06/2023)
    public class Department : BaseEntity
    {
        public Department()
        {
            DepartmentId = Guid.NewGuid();
        }

        /// <summary>
        /// Id department
        [Required]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã phòng ban
        [Required]
        [MaxLength(20)]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        [Required]
        [MaxLength(20)]
        public string DepartmentName { get; set;}
    }
}
