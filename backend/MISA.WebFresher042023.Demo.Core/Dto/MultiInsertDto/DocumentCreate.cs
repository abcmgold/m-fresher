using MISA.WebFresher042023.Demo.Core.Dto.Document;
using MISA.WebFresher042023.Demo.Core.Dto.Hanover;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Dto.MultiInsertDto
{
    public class DocumentCreate
    {
        public List<DocumentCreateDto> DocumentCreateDtos { get; set; }
        public List<PropertyTransferCreateDto> PropertyTransferCreateDtos { get; set; }
        public List<HandoverCreateDto>? HandoverCreateDtos { get; set; }
    }
}
