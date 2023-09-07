using MISA.WebFresher042023.Demo.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Dto.Hanover
{
    public class ReceiverCreateDto
    {
        public string FullName { get; set; }
        public string Represent { get; set; }
        public string Position { get; set; }
        public int ReceiverOrder { get; set; }
        public StatusRecord StatusRecord { get; set; }
    }
}
