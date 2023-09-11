using MISA.WebFresher042023.Demo.Core.Dto.Document;
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
        /// Check chứng từ có được phép xóa hay không
        /// </summary>
        /// <param name="DocumentId">Mã id của chứng từ điều chuyển</param>
        /// <returns>True: được xóa, nếu không gửi về exception</returns>
        /// CreatedBy: BATUAN (05/09/2023)
        public async Task<bool> CheckTransferAsset(Guid transferAssetId)
        {
            // kiểm tra xem transferAssetId có phải là id của 1 chứng từ điều chuyển nào không
            var transferAsset = await _transferAssetRepository.GetByIdAsync(transferAssetId);

            if (transferAsset != null)
            {
                var transferAssetDetailList = await _transferAssetDetailRepository.GetByTransferAssetId(transferAssetId);

                // 2. Kiểm tra từng tài sản, nếu tài sản nào có tồn tại trong 1 chứng từ khác
                // mà có thời gian điều chuyển hơn thời gian tạo của tài sản hiện tại
                // thì trả về false(không được xóa), nếu không trả về true (được xóa)

                foreach (var transferAssetDetail in transferAssetDetailList)
                {
                    var res = await _transferAssetRepository.CheckExist(transferAssetDetail.PropertyId, transferAsset.TransferDate);

                    var transferAssetCurrent = res.FirstOrDefault(t => t.TransferAssetId == transferAssetId);

                    res.RemoveAll(element => element.TransferAssetId ==  transferAssetId);

                    if (res.Count > 0)
                    {
                        var infoTransferAssets = new List<string>();

                        foreach (var transfer in res)
                        {
                            string formattedDate = transfer.TransferDate.ToString("dd/MM/yyyy");

                            infoTransferAssets.Add(string.Format(Resources.ResourceVN.InfoTransferAsset, transfer.TransferAssetCode, formattedDate));
                        }
                        throw new UserException(string.Format(Resources.ResourceVN.ErrorMesageDeleteTransferAsset, transferAsset.TransferAssetCode, transferAssetDetail.PropertyCode), (int)Enum.StatusCode.BadRequest, infoTransferAssets);
                    }
                }

                return true;
            }

            else throw new NotFoundException();
        }
        /// <summary>
        /// kiểm tra ngày chứng từ có nhỏ hơn ngày điều chuyển hay không
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
        /// Kiểm tra mã tài sản có bị trùng hay không khi thêm mới
        /// </summary>
        /// <param name="code">Mã tài sản</param>
        /// <returns></returns>
        /// CreatedBy: BATUAN (05/09/2023)
        public async Task CheckDuplicateTransferAssetInsertCodeAsync(TransferAssetCreateDto transferAssetCreateDto)
        {
            var res = await _transferAssetRepository.GetTransferAssetByCodeAsync(transferAssetCreateDto.TransferAssetCode);

            if (res != null)
            {
                throw new UserException(Resources.ResourceVN.ExistTransferAssetCode, (int)Enum.StatusCode.BadRequest, "transferAssetIdInput");
            }
        }

        /// <summary>
        /// Kiểm tra mã tài sản có bị trùng hay không khi chỉnh sửa
        /// </summary>
        /// <param name="code">Mã tài sản</param>
        /// <returns></returns>
        /// CreatedBy: BATUAN (05/09/2023)
        public async Task CheckDuplicateTransferAssetUpdateCode(TransferAssetUpdateDto transferAssetUpdateDto)
        {
            var res = await _transferAssetRepository.GetTransferAssetByCodeAsync(transferAssetUpdateDto.TransferAssetCode);

            if (res != null && res.TransferAssetId != transferAssetUpdateDto.TransferAssetId)
            {
                throw new UserException(Resources.ResourceVN.ExistTransferAssetCode, (int)Enum.StatusCode.BadRequest, "transferAssetIdInput");
            }
        }
        /// <summary>
        /// Check chứng từ có được phép thêm mới hoặc update không (check ngày điều chuyển của chứng từ có lớn hơn ngày điều chuyển
        /// của các chứng từ chứa các tài sản trong chứng từ hiện tại không)
        /// </summary>
        /// <param name="transferAssetDetails">Các chi tiết chứng từ</param>
        /// <param name="transferDate">Ngày điều chuyển của chứng từ hiện tại</param>
        /// <returns></returns>
        /// <exception cref="UserException"></exception>
        /// CreatedBy: BATUAN (07/09/2023)
        public async Task CheckTransferAssetUpdateOrInsert(List<TransferAssetDetail> transferAssetDetails, DateTime transferDate)
        {
            foreach (var transferAssetDetail in transferAssetDetails)
            {
                var res = await _transferAssetRepository.CheckExist(transferAssetDetail.PropertyId, transferDate);

                if (res.Count > 0)
                {
                    var property = await _propertyRepository.GetByIdAsync(transferAssetDetail.PropertyId);

                    var infoTransferAssets = new List<string>();

                    foreach (var transfer in res)
                    {
                        string formattedDate = transfer.TransferDate.ToString("dd/MM/yyyy");

                        infoTransferAssets.Add(string.Format(Resources.ResourceVN.InfoTransferAsset, transfer.TransferAssetCode, formattedDate));
                    }
                    throw new UserException(string.Format(Resources.ResourceVN.CheckTransferAsset, property.PropertyCode), (int)Enum.StatusCode.BadRequest, infoTransferAssets);
                }
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
                var res = await _transferAssetRepository.CheckExistRange(transferAssetDetail.PropertyId,oldTransferDate,  transferDate);

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
