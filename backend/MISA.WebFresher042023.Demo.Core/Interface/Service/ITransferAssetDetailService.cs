using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Service
{
    /// <summary>
    /// Lớp trừu tượng của tài sản điều chuyển service
    /// </summary>
    /// CreatedBy: BATUAN (30/08/2023)
    public interface ITransferAssetDetailService : IBaseService<TransferAssetDetailDto, TransferAssetDetailUpdateDto, TransferAssetDetailCreateDto>
    {
        public Task<List<TransferAssetDetailDto>> GetPropertyTransferByDocumentId(Guid documentId, int pageNumber, int pageSize);
    }
}
