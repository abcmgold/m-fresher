using MISA.WebFresher042023.Demo.Core.Dto.Property;

namespace MISA.WebFresher042023.Demo.Core.Interface.Service
{
    /// <summary>
    /// Inteface làm việc với controller của property
    /// </summary>
    public interface IPropertyService : IBaseService<PropertyDto, PropertyUpdateDto, PropertyCreateDto>
    {  
        /// <summary>
        /// Get lastest code in proprety
        /// </summary>
        /// <returns>Lastest code</returns>
        /// CreatedBy: BATUAN (19/06/2023)
        Task<string> GetAutoIdAsync();
        /// <summary>
        /// Paging
        /// </summary>
        /// <returns>list property</returns>
        /// CreatedBy: BATUAN (19/06/2023)
        Task<object> GetByPagingAsync(int pageNumber, int pageSize, string? searchInput, string? propertyType, string? departmentName);

        /// <summary>
        /// Check mã code đã tồn tại trong db hay chưa
        /// </summary>
        /// <param name="propertyCode">Mã code của property</param>
        /// <returns>true or false</returns>
        /// CreatedBy: BATUAN (21/06/2023)
        Task<bool> CheckDuplicatePropertyCode(string propertyCode);
    }
}
