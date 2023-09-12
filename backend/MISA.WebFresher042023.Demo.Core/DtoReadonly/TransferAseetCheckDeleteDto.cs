using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.DtoReadonly
{

    public class TransferAseetCheckDeleteDto
    {
        public string? TransferAssetCode { get; set; }
        public string? PropertyCode { get; set; }
        public DateTime TransferDate { get; set; }
        public string? TransferAssetCheckedCode { get; set; }
    }
}
