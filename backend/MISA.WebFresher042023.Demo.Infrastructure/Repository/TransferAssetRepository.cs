using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Core.DtoReadonly;
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
        /// <summary>
        /// Lấy mã chứng từ điều chuyển lớn nhất
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetGreatestCode()
        {
            var greatestCode = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<string>("CALL Proc_TransferAsset_GetGreatestCode()");
            
            return greatestCode;
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

        public async Task<List<TransferAssetPropertyReadonly>> GetByPropertyId(string listId)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@ListId", listId); 

            var res = await _unitOfWork.Connection.QueryAsync<TransferAssetPropertyReadonly>(sql: "CALL Proc_TransferAsset_GetByPropertyId(@ListId)", param: parameters, transaction: _unitOfWork.Transaction);

            return res.ToList();
        }

        public async Task<TransferAsset> GetTransferAssetByCodeAsync(string transferAssetCode)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@TransferAssetCode", transferAssetCode);

            var res = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TransferAsset>(sql: "CALL Proc_TransferAsset_GetByCode(@TransferAssetCode)", param: parameters);

            return res;
        }
    }
}
