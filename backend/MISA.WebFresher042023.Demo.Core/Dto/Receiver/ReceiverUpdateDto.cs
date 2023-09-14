using MISA.WebFresher042023.Demo.Core.Enum;
using MISA.WebFresher042023.Demo.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MISA.WebFresher042023.Demo.Core.Dto.Hanover
{
    public class ReceiverUpdateDto
    {
        public Guid ReceiverId { get; set; }
        public string FullName { get; set; }
        public string Represent { get; set; }
        public string Position { get; set; }
        public int ReceiverOrder { get; set; }
        public Guid TransferAssetId { get; set; }
        public StatusRecord StatusRecord { get; set; }
    }
}
