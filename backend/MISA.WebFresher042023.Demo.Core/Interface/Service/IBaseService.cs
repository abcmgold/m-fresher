using static Dapper.SqlMapper;

namespace MISA.WebFresher042023.Demo.Core.Interface.Service
{
    /// <summary>
    /// Interface base service
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// CreatedBy: BATUAN (20/06/2023)
    public interface IBaseService<TEntityDto, TEntityUpdateDto, TEntityCreateDto>
    {
        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns>list records</returns>
        /// CreatedBy: BATUAN (19/06/2023)
        Task<List<TEntityDto>> GetAsync();

        /// <summary>
        /// Xóa 1 hoặc nhiều bản ghi
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: BATUAN (19/06/2023)
        Task<int> DeleteAsync(List<Guid> listId); 


        /// <summary>
        /// Get one record by id
        /// </summary>
        /// <returns>record</returns>
        /// CreatedBy: BATUAN (19/06/2023)
        Task<TEntityDto> GetByIdAsync(Guid id);
        /// <summary>
        /// Chỉ sửa bản ghi
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: BATUAN (19/06/2023)
        Task<int> UpdateAsync(List<TEntityUpdateDto> entity);

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: BATUAN (19/06/2023)
        Task<int> InsertAsync(List<TEntityCreateDto> entityCreates);

    }
}
