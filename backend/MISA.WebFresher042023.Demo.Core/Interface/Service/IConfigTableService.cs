using MISA.WebFresher042023.Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Service
{
    public interface IConfigTableService
    {
        /// <summary>
        /// Thêm mới thông tin config cột của bảng
        /// </summary>
        /// CreatedBy: BATUAN (10/09/2023)
        Task<int> AddNewAsync(TableConfig tableConfig);
        /// <summary>
        /// Update thông tin config
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: BATUAN (10/09/2023)
        Task<int> UpdateAsync(TableConfig tableConfig);
        /// <summary>
        /// Lấy thông tin config của bảng
        /// </summary>
        /// <param name="tableName">Tên bảng</param>
        /// <returns>Thông tin cài đặt</returns>
        /// CreatedBy: BATUAN (10/09/2023)
        Task<TableConfig> GetAllAsync(string tableName);
    }
}
