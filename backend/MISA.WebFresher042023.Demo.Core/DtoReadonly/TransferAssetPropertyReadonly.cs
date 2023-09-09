using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.DtoReadonly
{
    /// <summary>
    /// Lớp đại diện cho lấy dữ liệu chứng từ phục vụ cho việc check xóa tài sản hay không
    /// </summary>
    /// CreatedBy: BATUAN(06/09/2023)
    public class TransferAssetPropertyReadonly
    {
        public Guid TransferAssetId { get; set; }
        public string? TransferAssetCode { get; set; }
        public DateTime TransferDate { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid PropertyId { get; set; }
        public string? PropertyCode { get; set; }
    }
}
