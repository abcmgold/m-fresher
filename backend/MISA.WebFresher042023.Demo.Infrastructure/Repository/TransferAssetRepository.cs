using Dapper;
using MISA.WebFresher042023.Demo.Core.DtoReadonly;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    public class TransferAssetRepository : BaseRepository<TransferAsset>, ITransferAssetRepository
    {
        public TransferAssetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
       
        public async Task<List<TransferAseetCheckDeleteDto>> CheckExist(string listPropertyId, DateTime transferDate)
        {
                var parameters = new DynamicParameters();

            parameters.Add("@ListId", listPropertyId);
            parameters.Add("@TransferDate", transferDate);

            var result = await _unitOfWork.Connection.QueryAsync<TransferAseetCheckDeleteDto>(
                sql: "CALL Proc_TransferAssetDetail_GetTransferAssetGreaterDate(@ListId, @TransferDate)",
                param: parameters);
            return result.ToList();
        }

        /// <summary>
        /// Lấy ra danh sách các chứng từ chứa tài sản ở trong đó và có ngày chứng từ lớn hơn ngày chứng từ của tài sản hiện tại
        /// </summary>
        /// <param name="propertyId">Id của tài sản</param>
        /// <param name="transactionDate">Ngày chứng từ</param>
        /// <returns>Danh sách các chứng từ điều chuyển thỏa mãn</returns>
        public async Task<List<TransferAseetCheckDeleteDto>> CheckExistGreater(string listId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@listId", listId);

            var result = await _unitOfWork.Connection.QueryAsync<TransferAseetCheckDeleteDto>(
                sql: "CALL Proc_TransferAsset_CheckDelete(@listId)",
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

        public async Task<int> CheckDuplicatePropertyCode(string transferAssetCode, Guid? transferAssetId)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@TransferAssetCode", transferAssetCode);
            parameters.Add("@TransferAssetId", transferAssetId);

            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<int>(
                sql: "CALL Proc_TransferAsset_CheckDuplicateCode(@TransferAssetCode, @TransferAssetId)",
                param: parameters
            );
            return result;
        }

        public async Task<int> CheckGreaterTransferDate(DateTime transferDate)
        {
            DynamicParameters parameters = new();

            parameters.Add("@TransferDate", transferDate);

            var res = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<int>(sql: "CALL Proc_TransferAsset_IsHasGreaterTransferDate(@TransferDate)", param: parameters);
            
            return res;
        }

        public async Task<List<TransferAsset>> CheckExistRange(Guid propertyId, DateTime oldTransferDate, DateTime transferDate)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@PropertyId", propertyId);
            parameters.Add("@OldTransferDate", oldTransferDate);
            parameters.Add("@TransferDate", transferDate);

            var result = await _unitOfWork.Connection.QueryAsync<TransferAsset>(
                sql: "CALL Proc_TransferAsset_CheckInRangeTransferAsset(@PropertyId,@OldTransferDate, @TransferDate)",
                param: parameters);
            return result.ToList();
        }
    }
}
