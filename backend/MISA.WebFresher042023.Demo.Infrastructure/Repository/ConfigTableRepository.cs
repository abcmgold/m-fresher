using Dapper;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    public class ConfigTableRepository : IConfigTableRepository
    {
        protected readonly IUnitOfWork _unitOfWork;
        public ConfigTableRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddNewAsync(TableConfig tableConfig)
        {
            var result = await _unitOfWork.Connection.ExecuteAsync($"Proc_ConfigTableColumn_AddNew", param: tableConfig, commandType: System.Data.CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return result;
        }

        public async Task<TableConfig> GetAllAsync(string tableName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("TableName", tableName);

            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TableConfig>("Proc_ConfigTableColumn_GetByTableName", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }


        public async Task<int> UpdateAsync(TableConfig tableConfig)
        {
            var result = await _unitOfWork.Connection.ExecuteAsync($"Proc_ConfigTableColumn_Update", param: tableConfig, commandType: System.Data.CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return result;
        }
    }
}
