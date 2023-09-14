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
    /// <summary>
    /// Lớp triển khai của ITransferAssetDetailRepository
    /// </summary>
    /// CreatedBy: BATUAN (20/08/2023)
    public class TransferAssetDetailRepository : BaseRepository<TransferAssetDetail>, ITransferAssetDetailRepository
    {
        public TransferAssetDetailRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Lấy danh sách tài sản điều chuyển theo id của chứng từ theo phân trang
        /// </summary>
        /// <param name="documentId">Id chứng từ</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="pageSize">Số lượng bản ghi lấy về</param>
        /// <returns>Danh sách tài sản điều chuyển trong chứng từ</returns>
        /// CreatedBy: BATUAN (30/08/2023)
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

        /// <summary>
        /// Lấy danh sách tài sản điều chuyển theo id của chứng từ (không phân trang)
        /// </summary>
        /// <param name="transferAssetId">Id chứng từ</param>
        /// <returns>Danh sách tài điều chuyển nằm trong chứng từ có id là documentId</returns>
        /// CreatedBy: BATUAN (30/08/2023)
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

        /// <summary>
        /// Đếm số bản ghi nằm trong listId
        /// </summary>
        /// <param name="listId">Danh sách các Id</param>
        /// <returns>Số lượng bản ghi</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public async Task<int> CountRecord(string listId)
        {
            DynamicParameters? parameters = new DynamicParameters();

            parameters.Add("@ListId", listId);

            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<int>(
                sql: "CALL Proc_TransferAssetDetail_CountRecord(@ListId)", param: parameters);

            return result;
        }
    }
}
