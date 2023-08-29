﻿using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;
using MySqlConnector;
using System.Data;

namespace MISA.WebFresher042023.Demo.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        protected readonly string? _connectionString;

        public UnitOfWork(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings"];
            _connection = new MySqlConnection(_connectionString);
        }

        public IDbConnection Connection => _connection;
        public IDbTransaction Transaction => _transaction;

        public void BeginTransaction()
        {
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            _connection.Close();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _connection.Close();
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
            if (_connection != null)
            {
                _connection.Dispose();
            }
        }
    }
}
