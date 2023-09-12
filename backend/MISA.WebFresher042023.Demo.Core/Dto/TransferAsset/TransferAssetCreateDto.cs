using MISA.WebFresher042023.Demo.Core.Dto.Hanover;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Dto.Document
{
    public class TransferAssetCreateDto
    {
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
        public List<TransferAssetDetailCreateDto>? TransferAssetDetailList { get; set; }
        public List<ReceiverCreateDto>? ReceiverList { get; set; }
    }
}
