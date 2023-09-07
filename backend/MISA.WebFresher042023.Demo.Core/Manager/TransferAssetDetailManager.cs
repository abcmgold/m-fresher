using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.HandleException;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Manager
{
    /// <summary>
    /// Lớp check nghiệp vụ khi thao tác với tài sản điều chuyển
    /// </summary>
    public class TransferAssetDetailManager
    {
        private readonly ITransferAssetDetailRepository _transferAssetDetailRepository;
        private readonly ITransferAssetRepository _transferAssetRepository;
        public TransferAssetDetailManager(ITransferAssetDetailRepository transferAssetDetailRepository, ITransferAssetRepository transferAssetRepository)
        {
            _transferAssetDetailRepository = transferAssetDetailRepository;
            _transferAssetRepository = transferAssetRepository;
        }

        public void CheckListDeparment(List<TransferAssetDetailUpdateDto> transferAssetDetaiList)
        {
            foreach (var transferAsset in transferAssetDetaiList)
            {
                if (transferAsset.DepartmentTransferId == transferAsset.DepartmentId)
                {
                    throw new UserException("Bộ phận chuyển đi phải khác bộ phận hiện tại", 400);
                }
            }
        }

        public async Task CheckExistTransferAssetDetail(List<TransferAssetDetailUpdateDto> listUpdate, List<TransferAssetDetailUpdateDto> listDelete)
        {
            var listId = new List<Guid>();
            listUpdate.ForEach(elementUpdate =>
            {
                listId.Add(elementUpdate.TransferAssetDetailId);
            });
            listDelete.ForEach(elementUpdate =>
            {
                listId.Add(elementUpdate.TransferAssetDetailId);
            });

            var numberRecord = await _transferAssetDetailRepository.CountRecord(string.Join(",", listId));

            if (numberRecord != listId.Count)
            {
                throw new UserException("Danh sách tài sản điều chuyển tồn tại tài sản điều chuyển không có trong hệ thống !", 400);
            }
        }
    }
}
