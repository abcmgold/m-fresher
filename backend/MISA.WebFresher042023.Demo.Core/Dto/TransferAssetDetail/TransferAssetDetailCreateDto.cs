using MISA.WebFresher042023.Demo.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer
{
    public class TransferAssetDetailCreateDto
    {
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.DepartmentId))]
        public Guid DepartmentId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.DepartmentId))]
        public Guid DepartmentTransferId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.PropertyId))]
        public Guid PropertyId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.DepartmentTransferName))]
        public string? DepartmentTransferName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.DepartmentName))] 
        public string? DepartmentName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.TransferAssetId))]
        public Guid TransferAssetId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.OriginalPrice))]
        public decimal OriginalPrice { get; set; }
        public string? Reason { get; set; }

    }
}
