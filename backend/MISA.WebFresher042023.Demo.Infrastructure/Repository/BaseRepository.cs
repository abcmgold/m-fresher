using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        protected readonly string? _connectionString;
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings"];
        }
        public async Task<List<TEntity>> GetAsync()
        {
            var tableName = typeof(TEntity).Name;

            MySqlConnection mySqlConnection = new MySqlConnection(_connectionString);

            await mySqlConnection.OpenAsync();

            //int numberRecords = await mySqlConnection.ExecuteScalarAsync<int>("CALL Proc_Property_CountRecords();");

            var result = await mySqlConnection.QueryAsync<TEntity>($"CALL Proc_{tableName}_GetAll()");

            await mySqlConnection.CloseAsync();

            return (List<TEntity>)result;
        }
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var tableName = typeof(TEntity).Name;

            MySqlConnection mySqlConnection = new MySqlConnection(_connectionString);

            await mySqlConnection.OpenAsync();

            var parameters = new DynamicParameters();

            parameters.Add("id", id);

            var result = await mySqlConnection.QueryFirstOrDefaultAsync<TEntity>(sql: $"CALL Proc_{tableName}_GetById(@id)", parameters);

            await mySqlConnection.CloseAsync();

            return result;
        }
        public async Task<int> DeleteMultiAsync(string id)
        {
            var tableName = typeof(TEntity).Name;

            var mySqlConnection = new MySqlConnection(_connectionString);

            await mySqlConnection.OpenAsync();

            var idArray = id.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            var idList = string.Join(",", idArray.Select(x => "\"" + x + "\""));

            var parameters = new DynamicParameters();

            parameters.Add("idList", idList);

            int res = mySqlConnection.Execute(sql: $"CALL Proc_{tableName}_MultiDelete(@idList)", parameters);

            await mySqlConnection.CloseAsync();

            return res;
        }

        public async Task<int> UpdateAsync(Guid id, TEntity entity)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var name = typeof(TEntity).Name;
                var parameters = new DynamicParameters();

                foreach (var prop in entity.GetType().GetProperties())
                {
                    if (string.Equals(prop.Name, "PropertyId"))
                    {
                        parameters.Add($"@p_{name}Id", id);
                    }
                    else
                    {
                        parameters.Add("@p_" + prop.Name, prop.GetValue(entity, null));
                    }
                }

                string storedProcedureName = "Proc_Property_Update";

                int result = await connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var name = typeof(TEntity).Name;
                var parameters = new DynamicParameters();
                var guid = Guid.NewGuid();

                foreach (var prop in entity.GetType().GetProperties())
                {
                    if (string.Equals(prop.Name, "PropertyId"))
                    {
                        parameters.Add($"@p_{name}Id", guid);
                    }
                    else
                    {
                        parameters.Add("@p_" + prop.Name, prop.GetValue(entity, null));
                    }
                }

                string storedProcedureName = "Proc_Property_AddNew";

                int result = await connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }  

    }
}
