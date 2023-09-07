using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Entities
{
    public class TransferAssetDetail : BaseEntity
    {
        public TransferAssetDetail() { 
            TransferAssetDetailId = Guid.NewGuid();
        }
        public Guid TransferAssetDetailId { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public decimal OriginalPrice { get; set; }
        public Guid DepartmentTransferId { get; set; }
        public string DepartmentTransferName { get; set; }
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }
        public decimal? WearRateValue { get; set; }
        public int? TotalRecords { get; set; }
        public int? FollowYear { get; set; }
        public string PropertyCode { get; set; }
        public Guid TransferAssetId { get; set; }
        public string Reason { get; set; }
        public decimal? ResidualPrice { get; set; }
    }
}
