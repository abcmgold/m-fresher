using MISA.WebFresher042023.Demo.Core.Dto.Hanover;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Dto.Document
{
    public class DocumentCreateDto
    {
        public string DocumentCode { get; set; }
        public DateTime TransferDate { get; set; }
        public DateTime DocumentDate { get; set; }
        public decimal OriginalPrice { get; set; }
        public string? Note { get; set; }
        public List<PropertyTransferCreateDto> PropertyTransferCreateList { get; set; }
        public List<HandoverCreateDto>? HandoverCreateList { get; set; }
    }
}
