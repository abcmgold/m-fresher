using Dapper;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;
using System.Data;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        protected readonly IUnitOfWork _unitOfWork;
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<TEntity>> GetAsync()
        {
            var tableName = typeof(TEntity).Name;

            var result = await _unitOfWork.Connection.QueryAsync<TEntity>($"CALL Proc_{tableName}_GetAll()", transaction: _unitOfWork.Transaction);

            return (List<TEntity>)result;
        }
        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            var tableName = typeof(TEntity).Name;

            var parameters = new DynamicParameters();

            parameters.Add("id", id);

            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(sql: $"CALL Proc_{tableName}_GetById(@id)", parameters, transaction: _unitOfWork.Transaction);

            return result;
        }
        public async Task<int> DeleteAsync(string id)
        {
            var tableName = typeof(TEntity).Name;

            var idArray = id.Split(new[] { ", " }, options: StringSplitOptions.RemoveEmptyEntries);
            var idList = string.Join(separator: ",", idArray.Select(x => "\"" + x + "\""));

            var parameters = new DynamicParameters();

            parameters.Add("@idList", idList);

            var res = await _unitOfWork.Connection.ExecuteAsync(sql: $"CALL Proc_{tableName}_MultiDelete(@idList)", parameters, transaction: _unitOfWork.Transaction);

            return res;

        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            var listEntity = new List<TEntity>() { entity };

            var result = await UpdateAsync(listEntity);

            return result;
        }

        public async Task<int> UpdateAsync(List<TEntity> entity)
        {
            var name = typeof(TEntity).Name;

            string storedProcedureName = $"Proc_{name}_Update";

            int result = await _unitOfWork.Connection.ExecuteAsync(sql: storedProcedureName, entity, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return result;
        }

        public async Task<int> InsertAsync(List<TEntity> entityCreates)
        {

            var name = typeof(TEntity).Name;

            string storedProcedureName = $"Proc_{name}_AddNew";

            int result = await _unitOfWork.Connection.ExecuteAsync(storedProcedureName, entityCreates, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return result;

        }
    }
}
