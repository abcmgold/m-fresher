using Dapper;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    public class ReceiverRepository : BaseRepository<Receiver>, IReceiverRepository
    {
        public ReceiverRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

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

        public async Task<IEnumerable<Receiver>> GetLastestReceivers()
        {
            var receivers = await _unitOfWork.Connection.QueryAsync<Receiver>("CALL Proc_Receiver_GetLastest()");

            return receivers.ToList();
        }
    }
}
