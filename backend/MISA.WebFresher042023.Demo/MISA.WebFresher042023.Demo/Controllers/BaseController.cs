using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Dto.Property;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Core.Service;

namespace MISA.WebFresher042023.Demo.Controllers
{
    /// <summary>
    /// Lớp base controller
    /// </summary>
    /// <typeparam name="TEntityDto">Sematic type tượng trưng cho các entityDto</typeparam>
    public abstract class BaseController<TEntityDto, TEntityUpdateDto, TEntityCreateDto> : ControllerBase
    {
        protected readonly IBaseService<TEntityDto, TEntityUpdateDto, TEntityCreateDto> _baseService;

        #region Constructor
        protected BaseController(IBaseService<TEntityDto, TEntityUpdateDto, TEntityCreateDto> baseService)
        {
            _baseService = baseService;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy: BATUAN (20/06/2023)
        [HttpGet]
        public async Task<ActionResult<List<TEntityDto>>> GetAsync()
        {

            var result = await _baseService.GetAsync();

            return Ok(result);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <returns></returns>
        /// CreatedBy: BATUAN (20/06/2023)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var res = -1;
            if (id.Length == 36)
            {
                res = await _baseService.DeleteAsync(id);
            }
            else if (id.Length > 36)
            {
                res = await _baseService.DeleteMultiAsync(id);
            }

            return StatusCode(200, res);

        }

        /// <summary>
        /// Lấy ra bản ghi bằng id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns></returns>
        /// Create By: BATUAN (20/06/2023)
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntityDto>> GetById(Guid id)
        {
            var propertyDto = await _baseService.GetByIdAsync(id);

            return StatusCode(200, propertyDto);
        }

        /// <summary>
        /// Thêm tài sản
        /// </summary>
        /// <param name="property">tài sản được thêm</param>
        /// <returns></returns>
        /// CreatedBy: BATUAN (14/06/2023)
        // POST api/<PropertiesController>
        [HttpPost]
        public async Task<ActionResult> Post(TEntityCreateDto property)
        {

            var res = await _baseService.InsertAsync(property);

            return StatusCode(201, res);
        }
        /// <summary>
        /// Sửa tài sản
        /// </summary>
        /// <param name="property">Dữ liệu tài sản đã được sửa</param>
        /// <returns></returns>
        /// CreatedBy: BATUAN (14/06/2023)
        // PUT api/<PropertiesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, TEntityUpdateDto property)
        {
            var res = await _baseService.UpdateAsync(id, property);

            return StatusCode(200, res);

        }
        #endregion
    }
}
