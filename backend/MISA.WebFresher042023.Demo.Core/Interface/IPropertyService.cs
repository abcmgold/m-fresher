using MISA.WebFresher042023.Demo.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface
{
    public interface IPropertyService
    {
        Task<List<PropertyDto>> Get();
        Task<PropertyDto> InsertProperty(PropertyCreateDto property);
        Task<PropertyDto> UpdateProperty(Guid propertyId, PropertyUpdateDto property);
        Task<PropertyDto> DeleteMultiProperty(string id);
        Task<string> GetLastestCode();
        Task GetByPaging(int pageNumber, int pageSize, string? searchInput, string? propertyType, string? departmentName);

    }
}
