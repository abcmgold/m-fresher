using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Dto.Hanover
{
    public class ReceiverDto
    {
        public Guid ReceiverId { get; set; }
        public string FullName { get; set; }
        public string Represent { get; set; }
        public string Position { get; set; }
        public int ReceiverOrder { get; set; }
        public Guid TransferAssetId { get; set; }
    }
}
