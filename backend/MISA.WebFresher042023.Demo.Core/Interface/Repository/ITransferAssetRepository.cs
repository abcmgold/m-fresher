using MISA.WebFresher042023.Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Repository
{
    public interface ITransferAssetRepository : IBaseRepository<TransferAsset>
    {
        public Task<object> GetByPaging(int pageNumber, int pageSize);
        Task<List<TransferAsset>> GetByDetail(Guid transferAssetDetailId, Guid transferAssetId);
        Task<List<TransferAsset>> GetByPropertyId(Guid propertyId);
        Task<List<TransferAsset>> CheckExist(Guid transferAssetDetailId, DateTime transactionDate);

    }

}
