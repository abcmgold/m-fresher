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
        //[Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        //[Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.ReceiverId))]
        public Guid ReceiverId { get; set; }

        //[Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        //[Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.ReceiverId))]
        public string FullName { get; set; }

        //[Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        //[Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Represent))]
        public string Represent { get; set; }

        //[Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.FieldRequired))]
        //[Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Positon))]
        public string Position { get; set; }
        public int ReceiverOrder { get; set; }
        public StatusRecord StatusRecord { get; set; }
    }
}
