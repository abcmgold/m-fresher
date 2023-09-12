using MISA.WebFresher042023.Demo.Core.DtoReadonly;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.HandleException;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;

namespace MISA.WebFresher042023.Demo.Core.Manager
{
    /// <summary>
    /// Lớp kiểm tra các nghiệp vụ khi thao tác với tài sản
    /// </summary>
    /// CreatedBy: BATUAN (28/08/2023)
    public class PropertyManager
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly ITransferAssetRepository _transferAssetRepository;

        public PropertyManager(IPropertyRepository propertyRepository, ITransferAssetRepository transferAssetRepository)
        {
            _propertyRepository = propertyRepository;
            _transferAssetRepository = transferAssetRepository;
        }

        /// <summary>
        /// Check trùng code
        /// </summary>
        /// <param name="property">Tài sản thêm mới</param>
        /// <returns>False: không trùng | True: trùng code</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public async Task<bool> CheckDuplicateCode(Property property)
        {
            var result = await _propertyRepository.CheckDuplicatePropertyCode(propertyCode: property.PropertyCode, property.PropertyId);

            if (result == 1) 
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check các nghiệp vụ của bài toán
        /// </summary>
        /// <param name="property">Tài sản được thêm</param>
        /// <returns>Lỗi nếu có hoặc null (không có lỗi)</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public ErrorInfo? CheckMajor(Property property)
        {
            if (property.OriginalPrice < property.WearRateValue)
            {
                return new ErrorInfo
                {
                    Error = Resources.ResourceVN.PriceError,
                    ErrorField = "wearRateValueInput"
                };
            }
            else if (property.NumberYearUse != Math.Round(1 / property.WearRate * 100, 2))
            {
                return new ErrorInfo
                {
                    Error = Resources.ResourceVN.WearRateError,
                    ErrorField = "wearRateInput"
                };
            }
            else if (property.PurchaseDate > property.FollowDate)
            {
                return new ErrorInfo
                {
                    Error = Resources.ResourceVN.DateError,
                    ErrorField = "followDateInput"
                };
            }
            else return null;
        }
        /// <summary>
        /// Kiểm tra tài sản có chứa trong chứng từ nào không
        /// </summary>
        /// <param name="propertyId">Id của tài sản</param>
        /// CreatedBy: BATUAN (30/08/2023)
        public async Task CheckExistInTransferAsset(List<Guid> propertyIds)
        {
            var stringId = string.Join(", ", propertyIds);
            // Lấy ra các chứng từ điều chuyển có id của property này
            var res = await _transferAssetRepository.GetByPropertyId(stringId);

            List<TransferAssetPropertyReadonly>? transferAssets = new();

            foreach (var propertyId in propertyIds)
            {
                foreach (var data in res)
                {
                    if (data.PropertyId == propertyId) {
                        transferAssets.Add(data);
                    }
                }
                if (transferAssets.Count > 0) break;

            }

            if (transferAssets.Count > 0)
            {
                var infoTransferAssets = new List<string>();

                foreach (var transferAsset in transferAssets)
                {
                    string formattedDate = transferAsset.TransferDate.ToString("dd/MM/yyyy");

                    
                    infoTransferAssets.Add(string.Format(Resources.ResourceVN.InfoTransferAsset, transferAsset.TransferAssetCode, formattedDate));
                }

                throw new UserException(string.Format(Resources.ResourceVN.NotDeleteProperty, transferAssets[0].PropertyCode), (int)Enum.StatusCode.BadRequest, infoTransferAssets);
            }
        }

        /// <summary>
        /// Kiểm tra tài sản có được chứa trong chứng từ nào có ngày điều chuyển lớn hơn ngày điều chuyển của chứng từ hiện tại không
        /// </summary>
        /// <param name="transferAssetId">id của chứng từ điều chuyển</param>
        /// <param name="propertyId">List Id của tài sản</param>
        /// <returns>Danh sách các chứng từ chứa tài sản</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public async Task CheckGreaterTransferDate(Guid transferAssetId, List<Guid> propertyId)
        {
            var listIdProperty = string.Join(", ", propertyId);

            var transferAsset = await _transferAssetRepository.GetByIdAsync(transferAssetId);

            var res = await _transferAssetRepository.CheckExist(listIdProperty, transferAsset.TransferDate);

                if (res.Count > 0)
                {
                    List<string>? infoTransferAssets = new();

                    foreach (var transfer in res)
                    {
                        string formattedDate = transfer.TransferDate.ToString("dd/MM/yyyy");

                        infoTransferAssets.Add(string.Format(Resources.ResourceVN.InfoTransferAsset, transfer.TransferAssetCode, formattedDate));
                    }
                    throw new UserException(string.Format(Resources.ResourceVN.NotUpdateOrDelete, res[0].PropertyCode), (int)Enum.StatusCode.BadRequest, infoTransferAssets);
                }
            
        }
    }
}
