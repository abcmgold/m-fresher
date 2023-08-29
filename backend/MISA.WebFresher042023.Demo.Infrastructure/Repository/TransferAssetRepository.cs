using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;
using MySqlConnector;
using System.Net.WebSockets;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    public class TransferAssetRepository : BaseRepository<TransferAsset>, ITransferAssetRepository
    {
        public TransferAssetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        /// <summary>
        /// Lấy ra danh sách các chứng từ chứa tài sản ở trong đó và có ngày chứng từ lớn hơn ngày chứng từ của tài sản hiện tại
        /// </summary>
        /// <param name="propertyId">Id của tài sản</param>
        /// <param name="transactionDate">Ngày chứng từ</param>
        /// <returns>Danh sách các chứng từ điều chuyển thỏa mãn</returns>
        public async Task<List<TransferAsset>> CheckExist(Guid propertyId, DateTime transactionDate)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@PropertyId", propertyId);
            parameters.Add("@TransactionDate", transactionDate);

            var result = await _unitOfWork.Connection.QueryAsync<TransferAsset>(
                sql: "CALL Proc_TransferAsset_CheckInTransferAsset(@PropertyId, @TransactionDate)",
                param: parameters);
            return result.ToList();
        }

        public async Task<List<TransferAsset>> GetByDetail(Guid transferAssetDetailId, Guid transferAssetId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TransferAssetDetailId", transferAssetDetailId);
            parameters.Add("@TransferAssetId", transferAssetId);

            var res = await _unitOfWork.Connection.QueryAsync<TransferAsset>(
                sql: "CALL Proc_TransferAsset_GetByDetail(@TransferAssetDetailId, @TransferAssetId)",
                param: parameters
             );

            return res.ToList();
        }

        public async Task<object> GetByPaging(int pageNumber, int pageSize)
        {
            
                var parameters = new DynamicParameters();
                parameters.Add("PageNumber", pageNumber);
                parameters.Add("PageSize", pageSize);
        
                // Lấy tổng số bản ghi
                var total = await _unitOfWork.Connection.QueryAsync<object>(
                    sql: "CALL Proc_TransferAsset_Total()",
                    param: parameters
                 );

                //Lấy dữ liệu phân trang
                var result = await _unitOfWork.Connection.QueryAsync<TransferAsset>(
                     sql: "CALL Proc_TransferAsset_Paging(@PageNumber, @PageSize)",
                     param: parameters
                 );

                return new { Data = result, Total = total };
        }

        public async Task<List<TransferAsset>> GetByPropertyId(Guid propertyId)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@PropertyId", propertyId); 

            var res = await _unitOfWork.Connection.QueryAsync<TransferAsset>(sql: "CALL Proc_TransferAsset_GetByPropertyId(@PropertyId)", param: parameters, transaction: _unitOfWork.Transaction);

            return res.ToList();
        }
    }
}
