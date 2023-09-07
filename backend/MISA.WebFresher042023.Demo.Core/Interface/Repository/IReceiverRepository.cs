using MISA.WebFresher042023.Demo.Core.Dto.Hanover;
using MISA.WebFresher042023.Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Repository
{
    /// <summary>
    /// Lớp trừu tượng của người nhận tài sản
    /// </summary>
    public interface IReceiverRepository : IBaseRepository<Receiver>
    {
        /// <summary>
        /// Lấy danh sách người nhận theo id của chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetId">Id chứng từ điều chuyển</param>
        /// <returns>Danh sách người nhận</returns>
        public Task<List<Receiver>> GetByTransferAssetId(Guid transferAssetId);
        /// <summary>
        /// Lấy danh sách người nhận của chứng từ mới nhất
        /// </summary>
        /// <returns>Danh sách người nhận</returns>
        public Task<IEnumerable<Receiver>> GetLastestReceivers();

    }
}
