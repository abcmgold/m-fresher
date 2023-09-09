using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Infrastructure.Interface
{
    /// <summary>
    /// Giao diện định nghĩa các phương thức quản lý giao dịch và kết nối cơ sở dữ liệu
    /// </summary>
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        DbConnection Connection { get; }
        DbTransaction Transaction { get; }

        void BeginTransaction();
        Task BeginTransactionAsync();

        void Commit();
        Task CommitAsync();

        void Rollback();
        Task RollbackAsync();
    }

}
