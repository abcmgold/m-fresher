using MISA.WebFresher042023.Demo.Core.Dto.Document;
using MISA.WebFresher042023.Demo.Core.Dto.Hanover;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.Entities;

namespace MISA.WebFresher042023.Demo.Core.Interface.Service
{
    public interface ITransferAssetService : IBaseService<TransferAssetDto, TransferAssetUpdateDto, TransferAssetCreateDto>
    {
        public Task<object> GetByPaging(int pageNumber, int pageSize);
        public Task<int> AddDocumentAsync(TransferAssetCreateDto documentCreateDto);
        public Task<int> UpdateDocumentAsync(TransferAssetUpdateDto documentUpdateDto);
    }
}
