using MISA.WebFresher042023.Demo.Core.Dto.Document;
using MISA.WebFresher042023.Demo.Core.Dto.Hanover;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.DtoReadonly;
using MISA.WebFresher042023.Demo.Core.Entities;

namespace MISA.WebFresher042023.Demo.Core.Interface.Service
{
    /// <summary>
    /// Lớp trừu tượng của TransferAssetService
    /// </summary>
    /// CreatedBy: BATUAN (30/08/2023)
    public interface ITransferAssetService : IBaseService<TransferAssetDto, TransferAssetUpdateDto, TransferAssetCreateDto>
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
        /// Tạo mới một chứng từ
        /// </summary>
        /// <param name="documentCreateDto">Chứng từ được tạo mới</param>
        /// <returns>1: Thành công || Exception: Thất bại</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public Task<int> AddDocumentAsync(TransferAssetCreateDto documentCreateDto);
        /// <summary>
        /// Cập nhật một chứng từ điều chuyển
        /// </summary>
        /// <param name="documentUpdateDto">Chứng từ được cập nhật</param>
        /// <returns>1: Thành công || Exception: Thất bại</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public Task<int> UpdateDocumentAsync(TransferAssetUpdateDto documentUpdateDto);
        /// <summary>
        /// Lấy thông tin của chứng từ điều chuyển theo Id
        /// </summary>
        /// <param name="transferAssetId">Id chứng từ</param>
        /// <returns>Thông tin chứng từ kèm với danh sách tài sản điều chuyển và người nhận</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public Task<TransferAssetReadonly> GetInfoTransferAsset(Guid transferAssetId);

        /// <summary>
        /// Hàm check 1 tài sản điều chuyển có được xóa ở form chỉnh sửa hay không
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public Task CheckDeleteOrNot(Guid transferAssetId, Guid propertyId);
        /// <summary>
        /// Hàm check các tài sản điều chuyển có được xóa ở form chỉnh sửa hay không
        /// </summary>
        /// <param name="transferAssetIds"></param>
        /// <returns></returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public Task CheckDeleteMultiOrNot(Guid transferAssetId, List<Guid> propertyIds);
        /// <summary>
        /// Sinh mã code tự động
        /// </summary>
        /// <returns>Chuỗi mã code của chứng từ</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public Task<string> GetAutoTransferAssetCode();
    }
}
