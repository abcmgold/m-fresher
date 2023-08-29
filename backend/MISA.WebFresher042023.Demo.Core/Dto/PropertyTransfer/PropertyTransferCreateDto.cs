using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer
{
    public class PropertyTransferCreateDto
    {
        public Guid DepartmentId { get; set; }
        public Guid DepartmentTransferId { get; set; }
        public Guid PropertyId { get; set; }
        public string DepartmentTransferName { get; set; }
        public string DepartmentName { get; set; }
        public Guid DocumentId { get; set; }
        public decimal OriginalPrice { get; set; }
        public string Reason { get; set; }

    }
}
