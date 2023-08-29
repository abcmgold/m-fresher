using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Entities
{
    public class TransferAsset : BaseEntity
    {
        public TransferAsset()
        {
            TransferAssetId = Guid.NewGuid();
        }
        public Guid TransferAssetId { get; set; }
        public string TransferAssetCode { get; set; }
        public DateTime TransferDate { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal OriginalPrice { get; set; }
        public string? Note { get; set; }
    }
}
