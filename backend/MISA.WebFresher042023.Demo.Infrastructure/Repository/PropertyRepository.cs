using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Core.Entities;
using MySqlConnector;
using Dapper;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using System.Text.RegularExpressions;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;
using MISA.WebFresher042023.Demo.Core.DtoReadonly;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    /// <summary>
    /// Thực thi IPropertyRepository
    /// </summary>
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Property> CheckDuplicatePropertyCode(string propertyCode)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@PropertyCode", propertyCode);

            // Thực hiện truy vấn và lấy kết quả đầu tiên
            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<Property>(
                sql: "CALL Proc_Property_GetByCode(@PropertyCode)",
                param: parameters
            );

            return result;

        }


        public async Task<object> GetByPagingAsync(int pageNumber, int pageSize, string? searchInput, string? propertyType, string? departmentName, string? excludeIds)
        {

            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);
            parameters.Add("PropertyType", propertyType);
            parameters.Add("DepartmentName", departmentName);
            parameters.Add("SearchInput", searchInput);
            parameters.Add("p_ExcludedIds", excludeIds);

            // Lấy tổng số bản ghi
            var total = await _unitOfWork.Connection.QueryAsync<object>(
                sql: "CALL Proc_Property_CountRecords(@PropertyType, @DepartmentName, @SearchInput)",
                param: parameters
             );

            //Lấy dữ liệu phân trang
            var result = await _unitOfWork.Connection.QueryAsync<Property>(
                 sql: "CALL Proc_Property_PagingAndFilter(@PageNumber, @PageSize, @PropertyType, @DepartmentName, @SearchInput, @p_ExcludedIds)",
                 param: parameters
             );

            return new { Data = result, Total = total };
        }



        public async Task<string> GetAutoIdAsync()
        {

            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<string>(sql: "CALL Proc_Property_GetLastestCode()");

            if (result == null) return "TS00000";

            return result;
        }

        public async Task<List<PropertyReadonly>> GetCurrenPropertyInfo(int pageNumber, int pageSize, string? excludedIds)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PageNumber", pageNumber);
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@ExcludedIds", excludedIds);
            var result = await _unitOfWork.Connection.QueryAsync<PropertyReadonly>(sql: "CALL Proc_Property_GetCurrent(@PageNumber, @PageSize, @ExcludedIds)", parameters);

            return result.ToList();
        }
    }
}
