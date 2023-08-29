using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Manager
{
    public class TransferAssetDetailManager
    {
        private readonly ITransferAssetDetailRepository _transferAssetDetailRepository;
        private readonly ITransferAssetRepository _transferAssetRepository;
        public TransferAssetDetailManager(ITransferAssetDetailRepository transferAssetDetailRepository, ITransferAssetRepository transferAssetRepository)
        {
            _transferAssetDetailRepository = transferAssetDetailRepository;
            _transferAssetRepository = transferAssetRepository;
        }
        /// <summary>
        /// Check nghiệp vụ tài sản có được phép xóa ở form thêm hay không
        /// </summary>
        /// <param name="transferAssetDetailId"></param>
        /// <param name="transferAssetId"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, DateTime>> CheckTransferAssetDetail(Guid transferAssetDetailId, Guid transferAssetId)
        {
            // lấy ra danh sách các chứng từ có xuất hiện tài sản hiện tại và có thời gian chứng từ lớn hơn thời gian chứng từ của
            // chứng từ hiện tại
            var transferAssetList = await _transferAssetRepository.GetByDetail(transferAssetDetailId, transferAssetId);

            Dictionary<string, DateTime> infoTransferAsset = new Dictionary<string, DateTime>();

            if (transferAssetList.Count > 0)
            {
                foreach( var transferAsset in transferAssetList)
                {
                    infoTransferAsset.Add(transferAsset.TransferAssetCode, transferAsset.TransactionDate);
                }
            }
            return infoTransferAsset;
        }
    }
}
