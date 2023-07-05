using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Core.Entities;
using MySqlConnector;
using Dapper;
using MISA.WebFresher042023.Demo.Core.Dto;
using System.Data;
using MISA.WebFresher042023.Demo.Core.HandleException;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;

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
            MySqlConnection mySqlConnection = new MySqlConnection(_connectionString);
            
            mySqlConnection.Open();

            var parameters = new DynamicParameters();

            parameters.Add("@PropertyCode", propertyCode);

            var result = await mySqlConnection.QueryFirstOrDefaultAsync<Property>(sql: "CALL Proc_Property_GetByCode(@PropertyCode)", parameters);
            
            mySqlConnection.Close();

            return result;
        }

        public async Task<Object> GetByPagingAsync(int pageNumber, int pageSize, string? searchInput, string? propertyType, string? departmentName)
        {
            MySqlConnection mySqlConnection = new MySqlConnection(_connectionString);

            var parameters = new DynamicParameters();           
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);
            parameters.Add("PropertyType", propertyType);
            parameters.Add("DepartmentName", departmentName);
            parameters.Add("SearchInput", searchInput);
                
            var parametersCount = new DynamicParameters();
            parametersCount.Add("PropertyType", propertyType);
            parametersCount.Add("DepartmentName", departmentName);
            parametersCount.Add("SearchInput", searchInput);

            var total = await mySqlConnection.QueryAsync<Object>(sql: "CALL Proc_Property_CountRecords(@PropertyType,@DepartmentName,@SearchInput )", parametersCount);

            var result = await mySqlConnection.QueryAsync<Property>(sql: "CALL Proc_Property_PagingAndFilter(@PageNumber,@PageSize,@PropertyType,@DepartmentName,@SearchInput )", parameters);

            return new { Data = result, Total = total };
        }


        public async Task<string> GetAutoIdAsync()
        {
            // 1. Kết nối DB
            MySqlConnection mySqlConnection = new(_connectionString);

            // 2. Thực hiện truy vấn
            var result = await mySqlConnection.QueryFirstOrDefaultAsync<string>(sql: "CALL Proc_Property_GetLastestCode()");

            if (result == null) return "TS00000";

            return result;
        }

        //public async Task<int> UpdatePropertyAsync(Guid propertyId, Property property)
        //{
        //    var mySqlConnection = new MySqlConnection(_connectionString);

        //    var parameters = new DynamicParameters();

        //    var res = mySqlConnection.Execute(sql: "CALL Proc_Property_Update(@PropertyId, @PropertyCode, @PropertyName, @DepartmentId, @PropertyTypeId, @Quantity, @OriginalPrice, @NumberYearUse, @WearRate, @WearRateValue, @FollowYear, @PurchaseDate, @FollowDate, @CreatedBy, @ModifiedBy)", property);
            
        //    return res;
        //}
    }
}
