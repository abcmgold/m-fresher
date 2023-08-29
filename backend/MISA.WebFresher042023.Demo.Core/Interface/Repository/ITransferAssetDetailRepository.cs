using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Repository
{
    public interface ITransferAssetDetailRepository : IBaseRepository<TransferAssetDetail>
    {
        Task<List<TransferAssetDetail>> GetPropertyTransferByDocumentId(Guid documentId, int pageNumber, int pageSize);
        Task<List<TransferAssetDetail>> GetByTransferAssetId(Guid transferAssetId);
    }
}
