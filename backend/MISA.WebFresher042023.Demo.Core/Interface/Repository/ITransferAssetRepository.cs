using MISA.WebFresher042023.Demo.Core.Dto.Document;
using MISA.WebFresher042023.Demo.Core.DtoReadonly;
using MISA.WebFresher042023.Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Repository
{
    /// <summary>
    /// Lớp trừu tượng của tài sản điều chuyển
    /// </summary>
    /// CreatedBy: BATUAN (30/08/2023)

    public interface ITransferAssetRepository : IBaseRepository<TransferAsset>
    {
        /// <summary>
        /// Phân trang danh sách chứng từ điều chuyển
        /// </summary>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trong một trang</param>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public Task<object> GetByPaging(int pageNumber, int pageSize);
        /// <summary>
        /// Lấy chứng từ thông qua id của tài sản điều chuyển và id chứng từ
        /// </summary>
        /// <param name="transferAssetDetailId">Id tài sản điều chuyển</param>
        /// <param name="transferAssetId">Id chứng từ</param>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        Task<List<TransferAsset>> GetByDetail(Guid transferAssetDetailId, Guid transferAssetId);
        /// <summary>
        /// Lấy danh sách chứng từ thông qua Id của tài sản
        /// </summary>
        /// <param name="listId">Chuỗi chứa id của các tài sản</param>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        Task<List<TransferAssetPropertyReadonly>> GetByPropertyId(string listId);
        /// <summary>
        /// Check tài sản trong chứng từ có tồn tại trong tài sản khác có ngày điều chuyển lớn hơn không
        /// </summary>
        /// <param name="listPropertyId">Danh sách Id của tài sản</param>
        /// <param name="transferDate">Ngày điều chuyển chứng từ</param>
        /// <returns>Danh sách tài sản</returns>
        /// CreatedBy: BATUAN (28/08/2023)
        public Task<List<TransferAseetCheckDeleteDto>> CheckExist(string listPropertyId, DateTime transferDate);
        /// <summary>
        /// Danh sách các chứng từ dựa theo Id của chứng từ và thời gian chứng từ
        /// </summary>
        /// <param name="propertyId">Id tài sản</param>
        /// <param name="oldTransferDate">Ngày chứng từ cũ</param>
        /// <param name="transferDate">Ngày chứng từ</param>
        /// <returns>Trả về danh sách các bản ghi có chứa tài sản điều chuyển theo id và có thời gian điều chuyển lớn hơn TransferDate</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        Task<List<TransferAsset>> CheckExistRange(Guid propertyId, DateTime oldTransferDate, DateTime transferDate);

        /// <summary>
        /// Lấy mã chứng từ điều chuyển lớn nhất
        /// </summary>
        /// <returns></returns>
        Task<string> GetGreatestCode();
        
        /// <summary>
        /// Lấy ra danh sách các chứng từ chứa tài sản ở trong đó và có ngày chứng từ lớn hơn ngày chứng từ của tài sản hiện tại
        /// </summary>
        /// <param name="propertyId">Id của tài sản</param>
        /// <param name="transactionDate">Ngày chứng từ</param>
        /// <returns>Danh sách các chứng từ điều chuyển thỏa mãn</returns>
        public Task<List<TransferAseetCheckDeleteDto>> CheckExistGreater(string listId);
        /// <summary>
        /// Check trùng code
        /// </summary>
        /// <param name="transferAssetCode">Mã code chứng từ</param>
        /// <param name="transferAssetId">Id của chứng từ (nếu có)</param>
        /// <returns>1: Trùng code || 0: Không trùng code</returns>
        /// CreatedBy: BATUAN (28/08/2023)
        public Task<int> CheckDuplicatePropertyCode(string transferAssetCode, Guid? transferAssetId);

    }

}
