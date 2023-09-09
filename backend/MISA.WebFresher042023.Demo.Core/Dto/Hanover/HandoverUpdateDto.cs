using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Dto.Hanover
{
    public class HandoverUpdateDto
    {
        public Guid HandoverId { get; set; }
        public string FullName { get; set; }
        public string Represent { get; set; }
        public string Position { get; set; }
        public string Order { get; set; }
        public Guid DocumentId { get; set; }
    }
}
