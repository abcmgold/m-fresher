using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MySqlConnector;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    public class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<object> GetByPaging(int pageNumber, int pageSize)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("PageNumber", pageNumber);
                parameters.Add("PageSize", pageSize);
        
                // Lấy tổng số bản ghi
                var total = await mySqlConnection.QueryAsync<object>(
                    sql: "CALL Proc_Document_Total()",
                    param: parameters
                 );

                //Lấy dữ liệu phân trang
                var result = await mySqlConnection.QueryAsync<Document>(
                     sql: "CALL Proc_Document_Paging(@PageNumber, @PageSize)",
                     param: parameters
                 );

                return new { Data = result, Total = total };
            }
        }
    }
}
