using MISA.WebFresher042023.Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Repository
{
    /// <summary>
    /// Interface base của repository
    /// </summary>
    /// <typeparam name="TEntity">Sematic type cho entity</typeparam>
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy: BATUAN (19/06/2023)
        Task<List<TEntity>> GetAsync();

        /// <summary>
        /// Lấy bản ghi theo id
        /// </summary>
        /// <returns>Bản ghi</returns>
        /// CreatedBy: BATUAN (19/06/2023)
        Task<TEntity> GetByIdAsync(Guid id);

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: BATUAN (19/06/2023)
        Task<int> DeleteMultiAsync(string id);

        /// <summary>
        /// Chỉ sửa bản ghi
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: BATUAN (19/06/2023)
        Task<int> UpdateAsync(Guid id, TEntity entity);

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: BATUAN (19/06/2023)
        Task<int> InsertAsync(TEntity entity);

    }
}
