using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer
{
    public class PropertyTransferUpdateDto
    {
        public Guid PropertyTransferId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid DepartmentTranferId { get; set; }
        public Guid PropertyId { get; set; }
        public Guid DocumnentId { get; set; }
        public string reason { get; set; }
    }
}
