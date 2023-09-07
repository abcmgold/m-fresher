using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Dto.Property;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MySqlConnector;
using Newtonsoft.Json;

namespace MISA.WebFresher042023.Demo.Controllers
{
    /// <summary>
    /// Api của Tài sản
    /// </summary>
    /// CreatedBy: BATUAN (20/06/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PropertyController : BaseController<PropertyDto, PropertyUpdateDto, PropertyCreateDto>
    {
        #region Properties
        private readonly IPropertyService _propertyService;
        #endregion

        #region Constructor
        public PropertyController(IPropertyService propertyService) : base(propertyService)
        {
            _propertyService = propertyService;
        }
        #endregion

        #region Methods
        ///// <summary>
        ///// Thêm tài sản
        ///// </summary>
        ///// <param name="property">tài sản được thêm</param>
        ///// <returns></returns>
        ///// CreatedBy: BATUAN (14/06/2023)
        //// POST api/<PropertiesController>
        //[HttpPost]
        //public async Task<ActionResult> Post(PropertyCreateDto property)
        //{

        //    var res = await _propertyService.InsertPropertyAsync(property);
            
        //    return StatusCode(201, res);
        //}
        ///// <summary>
        ///// Sửa tài sản
        ///// </summary>
        ///// <param name="property">Dữ liệu tài sản đã được sửa</param>
        ///// <returns></returns>
        ///// CreatedBy: BATUAN (14/06/2023)
        //// PUT api/<PropertiesController>/5
        //[HttpPut("{id}")]
        //public async Task<ActionResult> Update(Guid id, PropertyUpdateDto property)
        //{
        //    var res = await _propertyService.UpdatePropertyAsync(id, property);

        //    return StatusCode(200, res);

        //}

        /// <summary>
        /// API phân trang + tìm kiếm
        /// </summary>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageSize">Kích cỡ trang</param>
        /// <param name="searchInput">Giá trị tìm kiếm</param>
        /// <param name="propertyType">Giá trị lọc</param>
        /// <param name="departmentName">Giá trị lọc</param>
        /// <returns></returns>
        /// Created by: BATUAN (16/06/2023)
        [HttpGet]
        [Route("Filter")]
        public async Task<ActionResult> GetByPaging(int pageNumber, int pageSize, string? searchInput, string? propertyType, string? departmentName, string? excludeIds)
        {
            var res = await _propertyService.GetByPagingAsync(pageNumber, pageSize, searchInput, propertyType, departmentName, excludeIds);

            return Ok(value: res);
        }
        /// <summary>
        /// Lấy mã code mới được thêm vào gần nhất
        /// </summary>
        /// <returns></returns>
        /// Created by: BATUAN (16/06/2023)
        [HttpGet]
        [Route("GetLastestCode")]
        public async Task<ActionResult> GetLastestCodeAsync()
        {
            var result = await _propertyService.GetAutoIdAsync();

            return StatusCode(200, result);
        }
        [HttpGet]
        [Route("CurrentInfo")]
        public async Task<ActionResult> GetCurrentInfo(int pageNumber, int pageSize, string? excludedIds)
        {
            var res = await _propertyService.GetCurrenPropertyInfo(pageNumber, pageSize, excludedIds);

            return Ok(value: res);
        }
        #endregion
    }
}
