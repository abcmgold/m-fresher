using MISA.WebFresher042023.Demo.Core.Dto.Hanover;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Dto.Document
{
    public class TransferAssetDto
    {
        public Guid TransferAssetId { get; set; }
        public string TransferAssetCode { get; set; }
        public DateTime TransferDate { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal OriginalPrice { get; set; }
        public string? Note { get; set; }
        public decimal? ResidualPrice { get; set; }
        public decimal? TotalResidualPrice { get; set; }
    }
}
