using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    public class ConfigTableService : IConfigTableService
    {
        private readonly IConfigTableRepository _configTableRepository;
        public ConfigTableService(IConfigTableRepository configTableRepository)
        {
            _configTableRepository = configTableRepository;
        }
        public async Task<int> AddNewAsync(TableConfig tableConfig)
        {
            tableConfig.ConfigId = Guid.NewGuid();

            var res = await _configTableRepository.AddNewAsync(tableConfig);

            return res;
        }

        public async Task<TableConfig> GetAllAsync(string tableName)
        {
            var res = await _configTableRepository.GetAllAsync(tableName);

            return res;
        }

        public async Task<int> UpdateAsync(TableConfig tableConfig)
        {
            var tableConfigRecord = await _configTableRepository.GetAllAsync(tableConfig.TableName);

            if (tableConfigRecord == null)
            {
                var res = await AddNewAsync(tableConfig);
                return res;
            }
            else
            {
                var res = await _configTableRepository.UpdateAsync(tableConfig);
                return res;
            }
        }
    }
}
