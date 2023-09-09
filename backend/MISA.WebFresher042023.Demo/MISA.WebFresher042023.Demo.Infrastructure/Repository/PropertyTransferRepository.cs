using Dapper;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MySqlConnector;
using System.Reflection.Metadata;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    public class PropertyTransferRepository : BaseRepository<PropertyTransfer>, IPropertyTransferRepository
    {
        public PropertyTransferRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<PropertyTransfer>> GetPropertyTransferByDocumentId(Guid documentId, int pageNumber, int pageSize)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                DynamicParameters? parameters = new DynamicParameters();
                parameters.Add("@p_DocumentId", documentId);
                parameters.Add("@pageNumber", pageNumber);
                parameters.Add("@pageSize", pageSize);

                // Mở kết nối
                await mySqlConnection.OpenAsync();

                // Thực hiện truy vấn và lấy kết quả đầu tiên
                var result = await mySqlConnection.QueryAsync<PropertyTransfer>(
                    sql: "CALL Proc_PropertyTransfer_GetPagingByDocumentId(@p_DocumentId, @pageNumber, @pageSize)",
                    param: parameters
                );

                return (List<PropertyTransfer>)result;
            }
        }
    }
}
