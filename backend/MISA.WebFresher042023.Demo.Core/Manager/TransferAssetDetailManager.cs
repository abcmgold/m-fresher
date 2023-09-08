using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.HandleException;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;

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
        /// <summary>
        /// Kiểm tra xem các tài sản điều chuyển có tài sản nào có bộ phận mới và cũ trùng nhau không
        /// </summary>
        /// <param name="transferAssetDetaiList">Danh sách tài sản điều chuyển</param>
        /// <exception cref="UserException"></exception>
        /// CreateBy: BATUAN (30/08/2023)
        public void CheckListDeparment(List<TransferAssetDetail> transferAssetDetaiList)
        {
            foreach (var transferAsset in transferAssetDetaiList)
            {
                if (transferAsset.DepartmentTransferId == transferAsset.DepartmentId)
                {
                    throw new UserException(Resources.ResourceVN.DiffirentDepartment, (int)Enum.StatusCode.BadRequest);
                }
            }
        }

        /// <summary>
        /// Check danh sách tài sản điều chuyển được update và xóa có tài sản nào không tồn tại hay không
        /// </summary>
        /// <param name="listUpdate">Danh sách update</param>
        /// <param name="listDelete">Danh sách xóa</param>
        /// <returns></returns>
        /// <exception cref="UserException"></exception>
        /// CreateBy: BATUAN (30/08/2023)
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
                throw new UserException(Resources.ResourceVN.NotExistTransferAssetDetail, (int)Enum.StatusCode.BadRequest);
            }
        }
    }
}
