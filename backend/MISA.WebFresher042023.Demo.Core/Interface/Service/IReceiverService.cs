using MISA.WebFresher042023.Demo.Core.Dto.Hanover;
using MISA.WebFresher042023.Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Service
{
    /// <summary>
    /// Lớp trừu tượng của Receiver Service
    /// </summary>
    /// Created By: BATUAN (29/08/2023)
    public interface IReceiverService
    {
        /// <summary>
        /// Lấy danh sách người nhận ở chứng từ điều chuyển mới nhất
        /// </summary>
        /// <returns>Danh sách người nhận</returns>
        /// Created By: BATUAN (29/08/2023)
        public Task<List<ReceiverDto>> GetLastestReceivers();

    }
}
