using Microsoft.AspNetCore.Http.Features;
using MISA.WebFresher042023.Demo.Core.Dto.Property;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.HandleException;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Manager
{
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
        /// Check trùng code không khi thêm mới
        /// </summary>
        /// <param name="propertyCreateDto">Tài sản thêm mới</param>
        /// <returns>True: không trùng| False: trùng code</returns>
        public async Task<Boolean> CheckDuplicateInsertPropertyCode(PropertyCreateDto propertyCreateDto)
        {
            var result = await _propertyRepository.CheckDuplicatePropertyCode(propertyCreateDto.PropertyCode);

            if (result != null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check trùng code không khi update
        /// </summary>
        /// <param name="propertyUpdateDto">Tài sản cập nhật</param>
        /// <returns>false: không trùng| true: trùng code</returns>
        public async Task<Boolean> CheckDuplicateUpdatePropertyCode(PropertyUpdateDto propertyUpdateDto)
        {
            var result = await _propertyRepository.CheckDuplicatePropertyCode(propertyUpdateDto.PropertyCode);

            if (result != null && result.PropertyId != propertyUpdateDto.PropertyId)
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
        public ErrorInfo? CheckMajor(Property property)
        {
            if (property.OriginalPrice < property.WearRateValue)
            {
                return new ErrorInfo
                {
                    Error = "Nguyên giá phải lớn hơn hoặc bằng giá trị hao mòn năm!",
                    ErrorField = "wearRateValueInput"
                };
            }
            else if (property.NumberYearUse != 1 / property.WearRate * 100)
            {
                return new ErrorInfo
                {
                    Error = "Giá trị hao mòn phải bằng 1/Số năm sử dụng!",
                    ErrorField = "wearRateInput"
                };
            }
            else if (property.PurchaseDate > property.FollowDate)
            {
                return new ErrorInfo
                {
                    Error = "Ngày mua phải nhỏ hơn ngày bắt đầu sử dụng!",
                    ErrorField = "followDateInput"
                };
            }
            else return null;
        }
        /// <summary>
        /// Lấy ra danh sách các chứng từ có tài sản này xuất hiện trong đó
        /// </summary>
        /// <param name="propertyId">id của tài sản</param>
        /// <returns>Danh sách các chứng từ chứa tài sản</returns>
        public async Task<List<TransferAsset>> CheckExistInTransferAsset(Guid propertyId)
        {
            // Lấy ra các chứng từ điều chuyển có id của property này
            var res = await _transferAssetRepository.GetByPropertyId(propertyId);
            if (res.Count > 0)
            {
                var property = await _propertyRepository.GetByIdAsync(propertyId);

                var infoTransferAssets = new List<string>();

                foreach (var transferAsset in res)
                {
                    string formattedDate = transferAsset.TransactionDate.ToString("dd/MM/yyyy");

                    infoTransferAssets.Add($"- Chứng từ điều chuyển <strong>{transferAsset.TransferAssetCode}</strong> <strong>({formattedDate})</strong>");
                }
                throw new UserException($"Tài sản <strong>{property.PropertyCode}</strong> đã phát sinh chứng từ. Bạn không thể xóa chứng từ này!", 400, infoTransferAssets);
            }
            // trả về danh sách này hoặc null nếu không có  
            return res;
        }
    }
}
