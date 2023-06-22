using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Entities;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher042023.Demo.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class PropertyTypeController : ControllerBase
    {
        #region Fields
        private readonly string _connectionString;
        #endregion

        #region Constructor 
        public PropertyTypeController(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration["ConnectionStrings"];
        }

        #endregion

        #region Methods
        // GET: api/v1/<PropertyTypeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyType>>> Get()
        {
            try
            {
                // 1. Kết nối database
                var mySqlConnection = new MySqlConnection(_connectionString);
                // 2. Thực hiện query
                var result = await mySqlConnection.QueryAsync<PropertyType>("CALL Proc_PropertyType_GetAll();");
                // 3. Trả về kết quả
                return Ok(result);
            }
            catch (Exception ex)
            {
                var error = new Error();
                error.DevMsg = ex.Message;
                error.UserMsg = "Có lỗi xảy ra, vui lòng liên hệ bộ phận hỗ trợ!";
                // Trả về lỗi HTTP 500 và thông báo lỗi
                return StatusCode(500, error);
            }
        }
        // POST: api/v1/<PropertyTypeController>
        [HttpPost]
        public async Task<ActionResult> Post(PropertyType pro)
        {
            try
            {
                var errorData = "";
                var error = new Error();
                // 1. Validate dữ liệu
                // 1.1. Mã phòng ban bắt buộc nhập
                if (string.IsNullOrEmpty(pro.PropertyTypeCode))
                {
                    errorData += "Mã loại tài sản không được phép để trống";
                }
                // 1.2. Tên phòng ban bắt buộc nhập
                if (string.IsNullOrEmpty(pro.PropertyTypeName))
                {
                    errorData += "|Tên loại tài sản không được phép để trống";
                }

                if (!errorData.Equals(""))
                {
                    error.UserMsg = errorData;
                    return BadRequest(error);
                }

                // 2. Kết nối với Database
                var mySqlConnection = new MySqlConnection(_connectionString);
                // 3. Thực hiện câu lệnh truy vấn
                var res = mySqlConnection.Execute(sql: "CALL Proc_PropertyType_AddNew(@PropertyTypeId, @PropertyTypeCode, @PropertyTypeName, @CreatedBy,@ModifiedBy)", param: pro);
                // 4. Trả về kết quả
                return StatusCode(201, res);

            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    DevMsg = ex.Message,
                    UserMsg = "Có lỗi xảy ra, vui lòng liên hệ bộ phận hỗ trợ!"
                };
                // Trả về lỗi HTTP 500 và thông báo lỗi
                return StatusCode(500, error);
            }
        }

        // DELETE: api/v1/<DepartmentController>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                // 1. Kết nối với Database
                var mySqlConnection = new MySqlConnection(_connectionString);
                // 2. Thực hiện câu lệnh truy vấn
                var parameters = new DynamicParameters();
                parameters.Add("PropertyTypeId", id);
                var res = mySqlConnection.Execute(sql: "CALL Proc_PropertyType_Delete(@PropertyTypeId)", parameters);
                // 3. Trả về kết quả
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    DevMsg = ex.Message,
                    UserMsg = "Có lỗi xảy ra, vui lòng liên hệ bộ phận hỗ trợ!"
                };
                // Trả về lỗi HTTP 500 và thông báo lỗi
                return StatusCode(500, error);
            }
        }
        // UPDATE: api/v1/<PropertyTypeController>
        [HttpPut("{id}")]
        public IActionResult Update(PropertyType pro)
        {
            try
            {
                var error = new Error();
                // 1. Kết nối với Database
                var mySqlConnection = new MySqlConnection(_connectionString);
                // 2. Tìm kiếm trong DB có id trên không 
                var parameters = new DynamicParameters();
                parameters.Add("PropertyTypeId", pro.PropertyTypeId);
                var result = mySqlConnection.QueryFirstOrDefault<Department>("CALL Proc_PropertyType_GetById(@DepartmentId)", parameters);
                if (result == null)
                {
                    error.UserMsg = "Mã bộ phận không tồn tại";
                    return BadRequest(error);
                }
                // 3.Thực hiện câu lệnh truy vấn
                parameters.Add("PropertyTypeCode", pro.PropertyTypeCode);
                parameters.Add("PropertyTypeName", pro.PropertyTypeName);
                parameters.Add("ModifiedBy", pro.ModifiedBy);
                var res = mySqlConnection.Execute(sql: "CALL Proc_Property_Update(@DepartmentId, @DepartmentName, @DepartmentCode, @ModifiedBy)", parameters);
                // 4.Trả về kết quả
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    DevMsg = ex.Message,
                    UserMsg = "Có lỗi xảy ra, vui lòng liên hệ bộ phận hỗ trợ!"
                };
                // Trả về lỗi HTTP 500 và thông báo lỗi
                return StatusCode(500, error);
            }
        }
        /// <summary>
        /// Lấy ra mã loại tài sản bằng id
        /// </summary>
        /// <param name="id">id loại tài sản</param>
        /// <returns></returns>
        /// Create By: BATUAN (14/06/2023)
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var error = new Error();
            MySqlConnection mySqlConnection = new MySqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("PropertyTypeId", id);
            var result = mySqlConnection.QueryFirstOrDefault<PropertyType>(sql: "CALL Proc_PropertyType_GetById(@PropertyTypeId)", parameters);
            if (result == null)
            {
                error.UserMsg = "Mã tài sản không tồn tại!";
                return BadRequest(error);
            }
            return StatusCode(200, result);
        }
        #endregion
    }
}

