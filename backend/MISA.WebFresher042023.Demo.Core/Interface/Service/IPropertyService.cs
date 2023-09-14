using MISA.WebFresher042023.Demo.Core.Dto.Property;
using MISA.WebFresher042023.Demo.Core.DtoReadonly;
using MISA.WebFresher042023.Demo.Core.Entities;

namespace MISA.WebFresher042023.Demo.Core.Interface.Service
{
    /// <summary>
    /// Inteface làm việc với controller của property
    /// </summary>
    /// CreatedBy: BATUAN (30/06/2023)
    public interface IPropertyService : IBaseService<PropertyDto, PropertyUpdateDto, PropertyCreateDto>
    {  
        /// <summary>
        /// Sinh mã tự động
        /// </summary>
        /// <returns>Chuỗi mã code mới</returns>
        /// CreatedBy: BATUAN (19/06/2023)
        Task<string> GetAutoIdAsync();

        /// <summary>
        /// Phân trang và tìm kiếm tài sản
        /// </summary>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="searchInput">Thông tin tìm kiếm</param>
        /// <param name="propertyType">Thông tin tìm kiếm</param>
        /// <param name="departmentName">Thông tin tìm kiếm</param>
        /// <param name="excludeIds"></param>
        /// <returns>Danh sách tài sản</returns>
        /// CreatedBy: BATUAN (19/06/2023)
        Task<object> GetByPagingAsync(int pageNumber, int pageSize, string? searchInput, string? propertyType, string? departmentName, string? excludeIds);

        /// <summary>
        /// Lấy danh sách tài sản với phòng ban mới
        /// </summary>
        /// <returns>Danh sách tài sản</returns>
        /// CreatedBy: BATUAN (21/08/2023)
        Task<List<PropertyReadonly>> GetCurrenPropertyInfo(int pageNumber, int pageSize, string? searchInput, string? excludedIds);
    }
}
