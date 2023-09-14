using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Service;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConfigTableController : ControllerBase
    {
        private readonly IConfigTableService _configTableService;
        public ConfigTableController(IConfigTableService configTableService)
        {
            _configTableService = configTableService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TableConfig>>> GetAsync(string tableName)
        {
            var res = await _configTableService.GetAllAsync(tableName);

            return Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdateAsync(TableConfig tableConfig)
        {
            var res = await _configTableService.UpdateAsync(tableConfig);

            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<int>> InsertAsync(TableConfig tableConfig)
        {
            var res = await _configTableService.AddNewAsync(tableConfig);

            return Ok(res);
        }
    }
}
