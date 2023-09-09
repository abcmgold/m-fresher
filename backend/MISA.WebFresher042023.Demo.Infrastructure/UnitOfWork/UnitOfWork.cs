using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;
using MySqlConnector;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace MISA.WebFresher042023.Demo.Infrastructure.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DbConnection _connection;
        private DbTransaction? _transaction;
        private readonly string? _connectionString;

        public UnitOfWork(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings"];
            _connection = new MySqlConnection(_connectionString);
        }

        public DbConnection Connection => _connection;

        public DbTransaction? Transaction => _transaction;

        public void BeginTransaction()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _transaction = _connection.BeginTransaction();
            }
            else
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();

            }
        }

        public async Task BeginTransactionAsync()
        {
            if (_connection.State == GetOpen())
            {
                _transaction = await _connection.BeginTransactionAsync();
            }
            else
            {
                await _connection.OpenAsync();
                _transaction = await _connection.BeginTransactionAsync();

            }
        }

        private static ConnectionState GetOpen()
        {
            return System.Data.ConnectionState.Open;
        }

        public void Commit()
        {
            _transaction?.Commit();
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }

            await DisposeAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _transaction = null;

            _connection.Close();
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
            }
            _transaction = null;
            await _connection.CloseAsync();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            Dispose();
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
            await DisposeAsync();

        }
    }
}
