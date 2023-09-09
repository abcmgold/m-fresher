using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Entities
{
    public class PropertyTransfer : BaseEntity
    {
        public PropertyTransfer() { 
            PropertyTransferId = Guid.NewGuid();
        }
        public Guid PropertyTransferId { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public decimal OriginalPrice { get; set; }
        public Guid DepartmentTransferId { get; set; }
        public string DepartmentTransferName { get; set; }
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyCode { get; set; }
        public Guid DocumentId { get; set; }
        public string Reason { get; set; }
    }
}
