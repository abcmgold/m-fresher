using MISA.WebFresher042023.Demo.Core.Enum;
using MISA.WebFresher042023.Demo.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Entities
{
    public class Receiver : BaseEntity
    {
        /// <summary>
        /// Id người nhận
        /// </summary>
        public Guid ReceiverId { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        [Required]
        public string? FullName { get; set; } 
        /// <summary>
        /// Phòng ban đại diện
        /// </summary>
        [Required]
        public string? Represent { get; set; }
        /// <summary>
        /// Vị trí 
        /// </summary>
        [Required]
        public string? Position { get; set; }
        /// <summary>
        /// Thứ tự của người nhận
        /// </summary>
        [Required]
        public int ReceiverOrder { get; set; }
        /// <summary>
        /// Id của chứng từ
        /// </summary>
        [Required]
        public Guid TransferAssetId { get; set; }

    }
}
