using MISA.WebFresher042023.Demo.Core.Dto.Hanover;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MISA.WebFresher042023.Demo.Core.Dto.Document
{
    public class TransferAssetUpdateDto
    {
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.TransferAssetId))]
        public Guid TransferAssetId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.TransferAssetCode))]
        public string? TransferAssetCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.TransferDate))]
        public DateTime TransferDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.TransactionDate))]
        public DateTime TransactionDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.OriginalPrice))]
        public decimal OriginalPrice { get; set; }
        public string? Note { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.TransferAssetDetailList))]
        public List<TransferAssetDetailUpdateDto>? TransferAssetDetailList { get; set; }
        public List<ReceiverUpdateDto>? ReceiverList { get; set; }
    }
}
