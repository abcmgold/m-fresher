using MISA.WebFresher042023.Demo.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer
{
    public class TransferAssetDetailUpdateDto
    {
        public Guid TransferAssetDetailId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid DepartmentTransferId { get; set; }
        public string DepartmentTransferName { get; set; }
        public string DepartmentName { get; set; }
        public Guid PropertyId { get; set; }
        public Guid TransferAssetId { get; set; }
        public decimal OriginalPrice { get; set; }
        public string Reason { get; set; }
        public StatusRecord StatusRecord { get; set; }
    }
}
