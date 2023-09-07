using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using MISA.WebFresher042023.Demo.Core.Dto.Property;
using MISA.WebFresher042023.Demo.Core.DtoReadonly;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.HandleException;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Core.Manager;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    /// <summary>
    /// Thực thi IPropertyService
    /// </summary>
    public class PropertyService : BaseService<Property, PropertyDto, PropertyUpdateDto, PropertyCreateDto>, IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly ITransferAssetRepository _transferAssetRepository;

        private readonly PropertyManager propertyManager;
        public PropertyService(IPropertyRepository propertyRepository, ITransferAssetRepository transferAssetRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(propertyRepository, mapper, unitOfWork)
        {
            _propertyRepository = propertyRepository;
            _transferAssetRepository = transferAssetRepository;
            propertyManager = new PropertyManager(_propertyRepository, _transferAssetRepository);
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

        public override async Task<int> InsertAsync(List<PropertyCreateDto> propertyCreate)
        {
            var propertyInsertDto = propertyCreate[0];

            var property = _mapper.Map<List<Property>>(propertyCreate);

            // Check trùng code hay không
            var checkDuplicateCode = await propertyManager.CheckDuplicateInsertPropertyCode(propertyInsertDto);

            if (checkDuplicateCode == false)
            {
                throw new UserException(Resources.ResourceVN.DuplicateDepartmentCode, 409, "propertyCodeInput");
            }

            // Check nghiệp vụ
            var checkMajor = propertyManager.CheckMajor(property[0]);

            if (checkMajor != null)
            {
                throw new UserException(checkMajor.Error, 400, checkMajor.ErrorField);

            }

            // OK Insert thôi nào

            var result = await _propertyRepository.InsertAsync(property);

            return result;
        }

        public override async Task<int> UpdateAsync(List<PropertyUpdateDto> propertyUpdateDtos)
        {

            var propertyUpdateDto = propertyUpdateDtos[0];

            var property = _mapper.Map<List<Property>>(propertyUpdateDtos);

            // check có tồn tại hay không
            var pro = await _propertyRepository.GetByIdAsync(propertyUpdateDto.PropertyId);

            if (pro == null)
            {
                throw new NotFoundException();
            }

            // Check trùng code hay không
            var checkDuplicateCode = await propertyManager.CheckDuplicateUpdatePropertyCode(propertyUpdateDto);

            if (checkDuplicateCode == true)
            {
                throw new UserException(Resources.ResourceVN.DuplicateDepartmentCode, 409, "propertyCodeInput");
            }

            // Check nghiệp vụ
            var checkMajor = propertyManager.CheckMajor(property[0]);

            if (checkMajor != null)
            {
                throw new UserException(checkMajor.Error, 400, checkMajor.ErrorField);

            }

            // OK. Update thôi nào
            var res = await _propertyRepository.UpdateAsync(property);

            return res;
        }

        public override async Task<int> DeleteAsync(List<Guid> propertyId)
        {
            await propertyManager.CheckExistInTransferAsset(propertyId);

            string concatenatedIds = string.Join(separator: ", ", propertyId);

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _propertyRepository.DeleteAsync(concatenatedIds);

                await _unitOfWork.CommitAsync();

                return 1;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();

                throw;
            }
        }

        public async Task<List<PropertyReadonly>> GetCurrenPropertyInfo(int pageNumber, int pageSize, string? excludedIds)
        {
            var res = await _propertyRepository.GetCurrenPropertyInfo(pageNumber, pageSize, excludedIds);

            return res;
        }
    }
}
