using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyType;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.HandleException;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher042023.Demo.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class PropertyTypeController : BaseController<PropertyTypeDto, PropertyTypeUpdateDto, PropertyTypeCreateDto>
    {
        private readonly IPropertyTypeService _propertyTypeService;
        public PropertyTypeController(IPropertyTypeService propertyTypeService) : base(propertyTypeService)
        {
            _propertyTypeService = propertyTypeService;
        }

        //// POST: api/v1/<PropertyTypeController>
        //[HttpPost]
        //public Task<ActionResult> Post(PropertyType pro)
        //{
        //    // 1. Kết nối với Database
        //    var mySqlConnection = new MySqlConnection(_connectionString);
        //    // 2. Thực hiện câu lệnh truy vấn
        //    var res = mySqlConnection.Execute(sql: "CALL Proc_PropertyType_AddNew(@PropertyTypeId, @PropertyTypeCode, @PropertyTypeName, @CreatedBy,@ModifiedBy)", param: pro);
        //    // 3. Trả về kết quả
        //    return Task.FromResult<ActionResult>(StatusCode(201, res));
        //}

      

        // UPDATE: api/v1/<PropertyTypeController>
        //[HttpPut("{id}")]
        //public IActionResult Update(PropertyType pro)
        //{

        //    // 1. Kết nối với Database
        //    var mySqlConnection = new MySqlConnection(_connectionString);
        //    // 2. Tìm kiếm trong DB có id trên không 
        //    var parameters = new DynamicParameters();
        //    parameters.Add("PropertyTypeId", pro.PropertyTypeId);
        //    var result = mySqlConnection.QueryFirstOrDefault<Department>("CALL Proc_PropertyType_GetById(@DepartmentId)", parameters);
        //    if (result == null)
        //    {
        //        throw new NotFoundException();
        //    }
        //    // 3.Thực hiện câu lệnh truy vấn
        //    parameters.Add("PropertyTypeCode", pro.PropertyTypeCode);
        //    parameters.Add("PropertyTypeName", pro.PropertyTypeName);
        //    parameters.Add("ModifiedBy", pro.ModifiedBy);
        //    var res = mySqlConnection.Execute(sql: "CALL Proc_Property_Update(@DepartmentId, @DepartmentName, @DepartmentCode, @ModifiedBy)", parameters);
        //    // 4.Trả về kết quả
        //    return StatusCode(200, res);

        //}
        /// <summary>
        /// Lấy ra mã loại tài sản bằng id
        /// </summary>
        /// <param name="id">id loại tài sản</param>
        /// <returns></returns>
        /// Create By: BATUAN (14/06/2023)
    }
}

