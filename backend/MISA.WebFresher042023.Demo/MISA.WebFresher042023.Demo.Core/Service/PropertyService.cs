using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using MISA.WebFresher042023.Demo.Core.Dto.Property;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.HandleException;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using System.Text.RegularExpressions;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    /// <summary>
    /// Thực thi IPropertyService
    /// </summary>
    public class PropertyService : BaseService<Property, PropertyDto, PropertyUpdateDto, PropertyCreateDto>, IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        public PropertyService(IPropertyRepository propertyRepository, IMapper mapper) : base(propertyRepository, mapper)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<bool> CheckDuplicatePropertyCode(string propertyCode)
        {
            var res = await _propertyRepository.CheckDuplicatePropertyCode(propertyCode);

            if (res != null)
            {
                throw new UserException(Resources.ResourceVN.DuplicateDepartmentCode, 409, "propertyCodeInput");
            }

            return true;
        }

        public async Task<Object> GetByPagingAsync(int pageNumber, int pageSize, string? searchInput, string? propertyType, string? departmentName, string? excludeIds)
        {
            var property = await _propertyRepository.GetByPagingAsync(pageNumber, pageSize, searchInput, propertyType, departmentName, excludeIds);

            return property;
        }

        public async Task<string> GetAutoIdAsync()
        {
            var id = await _propertyRepository.GetAutoIdAsync();
            while (await this._propertyRepository.CheckDuplicatePropertyCode(id) != null)
            {

                string numberPart = Regex.Match(input: id, @"\d+$").Value;

                int lastIndex = id.LastIndexOf(numberPart);

                string textPart = id.Substring(startIndex: 0, lastIndex);


                if (string.IsNullOrEmpty(numberPart))
                {
                    id = textPart + 1;
                }
                else
                {
                    int number = int.Parse(numberPart) + 1;

                    string countedNumber = number.ToString("D" + numberPart.Length);

                    id = textPart + countedNumber;
                }

            }

            return id;

        }

        public override async Task<int> InsertAsync(PropertyCreateDto propertyCreate)
        {
            var res = await _propertyRepository.CheckDuplicatePropertyCode(propertyCreate.PropertyCode);

            if (res != null)
            {
                throw new UserException(Resources.ResourceVN.DuplicateDepartmentCode, 409, "propertyCodeInput");
            }
            var property = _mapper.Map<Property>(propertyCreate);

            CheckValidate(property);

            var result = await _propertyRepository.InsertAsync(property);

            return result;
        }

        public override async Task<int> UpdateAsync(Guid propertyId, PropertyUpdateDto propertyUpdateDto)
        {
            var checkProperty = await _propertyRepository.CheckDuplicatePropertyCode(propertyUpdateDto.PropertyCode);

            if (checkProperty != null && checkProperty.PropertyId != propertyId)
            {
                throw new ExistedPropertyCodeException();
            }

            var pro = await _propertyRepository.GetByIdAsync(propertyId);

            if (pro == null)
            {
                throw new NotFoundException();
            }

            var property = _mapper.Map<Property>(propertyUpdateDto);

            var res = await _propertyRepository.UpdateAsync(propertyId, property);

            return res;
        }

        private void CheckValidate(Property property)
        {
            if (property.WearRateValue > property.OriginalPrice)
            {
                throw new UserException(Resources.ResourceVN.PriceError, 400);
            }
            if (property.FollowDate < property.PurchaseDate)
            {
                throw new UserException(Resources.ResourceVN.DateError, 400);
            }
        }
    }
}
