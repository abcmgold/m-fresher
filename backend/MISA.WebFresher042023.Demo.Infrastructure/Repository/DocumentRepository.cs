using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;
using MySqlConnector;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    public class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<object> GetByPaging(int pageNumber, int pageSize)
        {
            
                var parameters = new DynamicParameters();
                parameters.Add("PageNumber", pageNumber);
                parameters.Add("PageSize", pageSize);
        
                // Lấy tổng số bản ghi
                var total = await _unitOfWork.Connection.QueryAsync<object>(
                    sql: "CALL Proc_Document_Total()",
                    param: parameters
                 );

                //Lấy dữ liệu phân trang
                var result = await _unitOfWork.Connection.QueryAsync<Document>(
                     sql: "CALL Proc_Document_Paging(@PageNumber, @PageSize)",
                     param: parameters
                 );

                return new { Data = result, Total = total };
        }
    }
}
