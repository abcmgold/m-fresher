using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Repository
{
    /// <summary>
    /// Lớp trừu trượng của tài sản điều chuyển
    /// </summary>
    /// CreatedBy: BATUAN (30/08/2023)
    /// 
    public interface ITransferAssetDetailRepository : IBaseRepository<TransferAssetDetail>
    {
        /// <summary>
        /// Lấy danh sách tài sản điều chuyển theo id của chứng từ theo phân trang
        /// </summary>
        /// <param name="documentId">Id chứng từ</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="pageSize">Số lượng bản ghi lấy về</param>
        /// <returns>Danh sách tài sản điều chuyển trong chứng từ</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        Task<List<TransferAssetDetail>> GetPropertyTransferByDocumentId(Guid documentId, int pageNumber, int pageSize);
        
        /// <summary>
        /// Lấy danh sách tài sản điều chuyển theo id của chứng từ (không phân trang)
        /// </summary>
        /// <param name="transferAssetId">Id chứng từ</param>
        /// <returns>Danh sách tài điều chuyển nằm trong chứng từ có id là documentId</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        Task<List<TransferAssetDetail>> GetByTransferAssetId(Guid transferAssetId);

        /// <summary>
        /// Đếm số bản ghi nằm trong listId
        /// </summary>
        /// <param name="listId">Danh sách các Id</param>
        /// <returns>Số lượng bản ghi</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        Task<int> CountRecord(string listId);

    }
}
