using Dapper;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    /// <summary>
    /// Lớp triển khai của IReceiverRepository
    /// </summary>
    /// CreatedBy: BATUAN(28/09/2023)
    public class ReceiverRepository : BaseRepository<Receiver>, IReceiverRepository
    {
        public ReceiverRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Lấy danh sách người nhận theo id của chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetId">Id chứng từ điều chuyển</param>
        /// <returns>Danh sách người nhận</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public async Task<List<Receiver>> GetByTransferAssetId(Guid transferAssetId)
        {
            DynamicParameters? parameters = new DynamicParameters();

            parameters.Add("@Id", transferAssetId);

            // Thực hiện truy vấn và lấy kết quả đầu tiên
            var result = await _unitOfWork.Connection.QueryAsync<Receiver>(
                sql: "CALL Proc_Receiver_GetByTransferAssetId(@Id)",
                param: parameters
            );

            return (List<Receiver>)result;
        }

        /// <summary>
        /// Lấy danh sách người nhận của chứng từ mới nhất
        /// </summary>
        /// <returns>Danh sách người nhận</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public async Task<IEnumerable<Receiver>> GetLastestReceivers()
        {
            var receivers = await _unitOfWork.Connection.QueryAsync<Receiver>("CALL Proc_Receiver_GetLastest()");

            return receivers.ToList();
        }
    }
}
