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
    public class DepartmentDto 
    {
        /// <summary>
        /// Id Department
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
    }
}
