using MISA.WebFresher042023.Demo.Core.Enum;
using MISA.WebFresher042023.Demo.Core.Resources;
using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer
{
    public class TransferAssetDetailUpdateDto
    {
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.TransferAssetDetailId))]
        public Guid TransferAssetDetailId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.DepartmentId))]
        public Guid DepartmentId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.DepartmentId))]
        public Guid DepartmentTransferId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.DepartmentTransferName))]
        public string? DepartmentTransferName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.DepartmentName))]
        public string? DepartmentName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.PropertyId))]
        public Guid PropertyId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.TransferAssetId))]
        public Guid TransferAssetId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.OriginalPrice))]
        public decimal OriginalPrice { get; set; }
        public string? Reason { get; set; }
        public StatusRecord StatusRecord { get; set; }
    }
}