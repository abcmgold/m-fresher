using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Entities
{
    public class TableConfig
    {
        public Guid ConfigId { get; set; }
        public string TableName { get; set;}
        public string ConfigContent { get; set;}
    }
}
