using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.HandleException;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;

namespace MISA.WebFresher042023.Demo.Core.Manager
{
    /// <summary>
    /// Lớp check nghiệp vụ khi thao tác với chứng từ điều chuyển tài sản
    /// </summary>
    /// CreatedBy: BATUAN (05/09/2023)
    public class TransferAssetManager
    {
        private readonly ITransferAssetDetailRepository _transferAssetDetailRepository;
        private readonly ITransferAssetRepository _transferAssetRepository;
        private readonly IPropertyRepository _propertyRepository;
        public TransferAssetManager(ITransferAssetDetailRepository transferAssetDetailRepository, ITransferAssetRepository transferAssetRepository, IPropertyRepository propertyRepository)
        {
            _transferAssetDetailRepository = transferAssetDetailRepository;
            _transferAssetRepository = transferAssetRepository;
            _propertyRepository = propertyRepository;
        }

        /// <summary>
        /// Check chứng từ được xóa hay không
        /// </summary>
        /// <param name="transferAssetIdList">Danh sách Id của các chứng từ cần check</param>
        /// <exception cref="UserException">Exception thông báo chứng từ không được xóa</exception>
        /// CreatedBy: BATUAN (05/09/2023)
        public async Task CheckTransferAssetAllowDelete(List<Guid> transferAssetIdList)
        {
            string joinedIds = string.Join(", ", transferAssetIdList);

            var res = await _transferAssetRepository.CheckExistGreater(joinedIds);

            if (res.Count > 0)
            {
                var infoTransferAssets = new List<string>();

                foreach (var transfer in res)
                {
                    string formattedDate = transfer.TransferDate.ToString("dd/MM/yyyy");

                    infoTransferAssets.Add(string.Format(Resources.ResourceVN.InfoTransferAsset, transfer.TransferAssetCode, formattedDate));
                }
                throw new UserException(string.Format(Resources.ResourceVN.ErrorMesageDeleteTransferAsset, res[0].TransferAssetCheckedCode, res[0].PropertyCode), (int)Enum.StatusCode.BadRequest, infoTransferAssets);
            }

        }

        /// <summary>
        /// Kiểm tra ngày chứng từ có nhỏ hơn ngày điều chuyển hay không
        /// </summary>
        /// <param name="transferDate">Ngày điều chuyển</param>
        /// <param name="transactionDate">Ngày chứng từ</param>
        /// <returns>true: nếu hợp lệ</returns>
        /// <exception cref="UserException">Exception nếu không hợp lệ</exception>
        /// CreatedBy: BATUAN (05/09/2023)
        public bool ValidateTransferDate(DateTime transferDate, DateTime transactionDate)
        {
            if (transferDate < transactionDate)
            {
                throw new UserException(Resources.ResourceVN.ErrorDateInTransferAsset, (int)Enum.StatusCode.BadRequest, "transferDateInput");
            }
            else return true;
        }

        /// <summary>
        /// Check mã code có trùng hay không
        /// </summary>
        /// <param name="transferAsset">Tài sản cần kiểm tra mã code</param>
        /// <exception cref="">Thong báo lỗi trùng code cho người dùng</exception>
        /// CreatedBy: BATUAN (05/09/2023)
        public async Task CheckDuplicateCode(TransferAsset transferAsset)
        {
            var res = await _transferAssetRepository.CheckDuplicatePropertyCode(transferAsset.TransferAssetCode, transferAsset.TransferAssetId);

            if (res == 1)
            {
                throw new UserException(Resources.ResourceVN.ExistTransferAssetCode, (int)Enum.StatusCode.BadRequest, "transferAssetIdInput");
            }
        }

        /// <summary>
        /// Check tài sản được chỉnh sửa hoặc xóa hay không
        /// </summary>
        /// <param name="propertyListId">Danh sách Id của các tài sản</param>
        /// <param name="transferDate">Ngày điều chuyển của chứng từ</param>
        /// <exception cref="UserException">Lỗi thông báo tài sản không được xóa</exception>
        /// CreatedBy: BATUAN (28/08/2023)
        public async Task CheckTransferAssetUpdateOrInsert(List<Guid> propertyListId, DateTime transferDate)
        {
                var listStringId = string.Join(", ", propertyListId);
                var res = await _transferAssetRepository.CheckExist(listStringId, transferDate);

                if (res.Count > 0)
                {
                    var infoTransferAssets = new List<string>();

                    foreach (var transfer in res)
                    {
                        string formattedDate = transfer.TransferDate.ToString("dd/MM/yyyy");

                        infoTransferAssets.Add(string.Format(Resources.ResourceVN.InfoTransferAsset, transfer.TransferAssetCode, formattedDate));
                    }
                    throw new UserException(string.Format(Resources.ResourceVN.CheckTransferAsset, res[0].PropertyCode), (int)Enum.StatusCode.BadRequest, infoTransferAssets);
                }
            
        }

        /// <summary>
        /// Check chứng từ có được phép thêm mới hoặc update không khi thực hiện thay đổi thời gian điều chuyển
        /// </summary>
        /// <param name="transferAssetDetails">Các chi tiết chứng từ</param>
        /// <param name="oldTransferDate">Ngày điều chuyển của chứng từ hiện tại</param>
        /// <param name="transferDate">Ngày điều chuyển của chứng từ khi chỉnh sửa</param>
        /// <exception cref="UserException"></exception>
        /// CreatedBy: BATUAN (07/09/2023)
        public async Task CheckTransferAssetChangeTransferDate(List<TransferAssetDetailUpdateDto> transferAssetDetails, DateTime oldTransferDate, DateTime transferDate)
        {
            foreach (var transferAssetDetail in transferAssetDetails)
            {
                var res = await _transferAssetRepository.CheckExistRange(transferAssetDetail.PropertyId, oldTransferDate, transferDate);

                if (res.Count > 0)
                {
                    var property = await _propertyRepository.GetByIdAsync(transferAssetDetail.PropertyId);

                    var infoTransferAssets = new List<string>();

                    foreach (var transfer in res)
                    {
                        string formattedDate = transfer.TransferDate.ToString("dd/MM/yyyy");

                        infoTransferAssets.Add(string.Format(Resources.ResourceVN.InfoTransferAsset, transfer.TransferAssetCode, formattedDate));
                    }
                    throw new UserException(string.Format(Resources.ResourceVN.UpdateFail, property.PropertyCode), (int)Enum.StatusCode.BadRequest, infoTransferAssets);
                }
            }
        }
    }
}
