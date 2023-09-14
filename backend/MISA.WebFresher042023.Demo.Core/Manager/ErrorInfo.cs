using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Manager
{
    /// <summary>
    /// Lớp khai báo entity thông báo lỗi người dùng
    /// </summary>
    /// CreatedBy: BATUAN (30/06/2023)
    public class ErrorInfo
    {
        public string? Error { get; set; }
        public string? ErrorField { get; set; }
    }
}
