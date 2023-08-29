using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Core.Entities;
using MySqlConnector;
using Dapper;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using System.Text.RegularExpressions;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    /// <summary>
    /// Thực thi IPropertyRepository
    /// </summary>
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Property> CheckDuplicatePropertyCode(string propertyCode)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PropertyCode", propertyCode);

                // Mở kết nối
                await mySqlConnection.OpenAsync();

                // Thực hiện truy vấn và lấy kết quả đầu tiên
                var result = await mySqlConnection.QueryFirstOrDefaultAsync<Property>(
                    sql: "CALL Proc_Property_GetByCode(@PropertyCode)",
                    param: parameters
                );

                return result;
            }
        }


        public async Task<object> GetByPagingAsync(int pageNumber, int pageSize, string? searchInput, string? propertyType, string? departmentName, string? excludeIds)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("PageNumber", pageNumber);
                parameters.Add("PageSize", pageSize);
                parameters.Add("PropertyType", propertyType);
                parameters.Add("DepartmentName", departmentName);
                parameters.Add("SearchInput", searchInput);
                parameters.Add("p_ExcludedIds", excludeIds);

                // Lấy tổng số bản ghi
                var total = await mySqlConnection.QueryAsync<object>(
                    sql: "CALL Proc_Property_CountRecords(@PropertyType, @DepartmentName, @SearchInput)",
                    param: parameters
                 );

                //Lấy dữ liệu phân trang
                var result = await mySqlConnection.QueryAsync<Property>(
                     sql: "CALL Proc_Property_PagingAndFilter(@PageNumber, @PageSize, @PropertyType, @DepartmentName, @SearchInput, @p_ExcludedIds)",
                     param: parameters
                 );

                return new { Data = result, Total = total };
            }
        }



        public async Task<string> GetAutoIdAsync()
        {
            MySqlConnection mySqlConnection = new(_connectionString);

            var result = await mySqlConnection.QueryFirstOrDefaultAsync<string>(sql: "CALL Proc_Property_GetLastestCode()");

            if (result == null) return "TS00000";

            return result;
        }
    }
}
