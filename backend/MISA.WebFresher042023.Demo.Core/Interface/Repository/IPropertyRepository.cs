using MISA.WebFresher042023.Demo.Core.DtoReadonly;
using MISA.WebFresher042023.Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Repository
{
    /// <summary>
    /// Inteface của property repository
    /// </summary>
    /// CreatedBy: BATUAN (20/06/2023)
    public interface IPropertyRepository : IBaseRepository<Property>
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
        Task<object> GetByPagingAsync(int pageNumber, int pageSize, string? searchInput, string? propertyType, string? departmentName, string? excludeIds);

        /// <summary>
        /// Check mã code đã tồn tại trong db hay chưa?
        /// </summary>
        /// <param name="propertyCode">Mã code của property</param>
        /// <returns>1: Đã tồn tại|| 0: Chưa tồn tại</returns>
        /// CreatedBy: BATUAN (21/06/2023)
        Task<int> CheckDuplicatePropertyCode(string propertyCode, Guid? propertyId);

        /// <summary>
        /// Lấy danh sách tài sản với phòng ban mới
        /// </summary>
        /// <returns>Danh sách tài sản</returns>
        /// CreatedBy: BATUAN (21/08/2023)
        Task<List<PropertyReadonly>> GetCurrenPropertyInfo(int pageNumber, int pageSize,string? searchInput, string? excludedIds);
    }
}
