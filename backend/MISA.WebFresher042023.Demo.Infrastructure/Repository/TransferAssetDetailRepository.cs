using Dapper;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;
using MySqlConnector;
using System.Reflection.Metadata;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    public class TransferAssetDetailRepository : BaseRepository<TransferAssetDetail>, ITransferAssetDetailRepository
    {
        public TransferAssetDetailRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<TransferAssetDetail>> GetPropertyTransferByDocumentId(Guid transferAssetId, int pageNumber, int pageSize)
        {

            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@p_TransferAssetId", value: transferAssetId);
            parameters.Add("@pageNumber", pageNumber);
            parameters.Add("@pageSize", pageSize);


            // Thực hiện truy vấn và lấy kết quả đầu tiên
            var result = await _unitOfWork.Connection.QueryAsync<TransferAssetDetail>(
                sql: "CALL Proc_TransferAssetDetail_GetPagingByTransferAssetId(@p_TransferAssetId, @pageNumber, @pageSize)",
                param: parameters
            );

            return (List<TransferAssetDetail>)result;

        }

        public async Task<List<TransferAssetDetail>> GetByTransferAssetId(Guid transferAssetId)
        {
            DynamicParameters? parameters = new DynamicParameters();

            parameters.Add("@Id", transferAssetId);

            // Thực hiện truy vấn và lấy kết quả đầu tiên
            var result = await _unitOfWork.Connection.QueryAsync<TransferAssetDetail>(
                sql: "CALL Proc_TransferAssetDetail_GetByTransferAssetId(@Id)",
                param: parameters
            );

            return (List<TransferAssetDetail>)result;
        }

        public async Task<int> CountRecord(string listId)
        {
            DynamicParameters? parameters = new DynamicParameters();

            parameters.Add("@listId", listId);

            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<int>(
                sql: "CALL Proc_TransferAssetDetail_CountRecord(@listId)", param: parameters);

            return result;
        }
    }
}
