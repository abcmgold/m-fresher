using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Entities;
using MySqlConnector;

namespace MISA.WebFresher042023.Demo.Controllers
{
    /// <summary>
    /// Api của Depearment 
    /// </summary>
    /// CreatedBy: BATUAN (14/06/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly string _connectionString;
        public DepartmentController(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration["ConnectionStrings"];
        }
        /// <summary>
        /// Lấy tất cả danh sách phòng ban
        /// </summary>
        /// <returns>200: Lấy thành công 500: Lỗi server</returns>
        /// CreatedBy: BATUAN (14/06/2023)
        // GET: api/v1/<DepartmentController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> Get()
        {
            try
            {
                // 1. Kết nối DB
                var mySqlConnection = new MySqlConnection(_connectionString);
                // 2. Thực hiện truy vấn
                var result = await mySqlConnection.QueryAsync<Department>("CALL Proc_Department_GetAll();");
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
        /// <summary>
        /// Sửa phòng ban
        /// </summary>
        /// <param name="department">dữ liệu chỉnh sửa</param>
        /// <returns></returns>
        /// CreatedBy: BATUAN (14/06/2023)
        // POST: api/v1/<DepartmentController>
        [HttpPost]
        public async Task<ActionResult> Post(Department department)
        {
            try
            {
                var errorData = "";
                var error = new Error();
                // 1. Validate dữ liệu
                // 1.1. Mã phòng ban bắt buộc nhập
                if (string.IsNullOrEmpty(department.DepartmentCode))
                {
                    errorData += "Mã bộ phận không được phép để trống";
                }
                // 1.2. Tên phòng ban bắt buộc nhập
                if (string.IsNullOrEmpty(department.DepartmentCode))
                {
                    errorData += "|Tên bộ phận không được phép để trống";
                }

                if (!errorData.Equals(""))
                {
                    error.UserMsg = errorData;
                    return BadRequest(error);
                }

                // 2. Kết nối với Database
                var mySqlConnection = new MySqlConnection(_connectionString);
                // 3. Thực hiện câu lệnh truy vấn
                var res = mySqlConnection.Execute(sql: "CALL Proc_Department_AddNew(@DepartmentId, @DepartmentCode, @DepartmentName, @CreatedBy,@ModifiedBy)", param: department);
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
        /// <summary>
        /// Xóa phòng ban theo id
        /// </summary>
        /// <param name="id">id phòng ban</param>
        /// <returns></returns>
        /// CreatedBy: BATUAN (14/06/2023)
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
                parameters.Add("DepartmentId", id);
                var res = mySqlConnection.Execute(sql: "CALL Proc_Department_Delete(@DepartmentId)", parameters);
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
        /// <summary>
        /// Sửa phòng ban
        /// </summary>
        /// <param name="department">id phòng ban</param>
        /// <returns></returns>
        /// CreatedBy: BATUAN (14/06/2023)
        // UPDATE: api/v1/<DepartmentController>
        [HttpPut("{id}")]
        public IActionResult Update(Department department)
        {
            try
            {
                var error = new Error();
                // 1. Kết nối với Database
                MySqlConnection? mySqlConnection = new MySqlConnection(_connectionString);
                // 2. Tìm kiếm trong DB có id trên không 
                var parameters = new DynamicParameters();
                parameters.Add("DepartmentId", department.DepartmentId);
                var result = mySqlConnection.QueryFirstOrDefault<Department>(sql: "CALL Proc_Department_GetById(@DepartmentId)", parameters);
                if (result == null)
                {
                    error.UserMsg = "Mã bộ phận không tồn tại";
                    return BadRequest(error);
                }
                // 3.Thực hiện câu lệnh truy vấn
                parameters.Add("DepartmentCode", department.DepartmentCode);
                parameters.Add("DepartmentName", department.DepartmentName);
                parameters.Add("ModifiedBy", department.ModifiedBy);
                var res = mySqlConnection.Execute(sql: "CALL Proc_Department_Update(@DepartmentId, @DepartmentName, @DepartmentCode, @ModifiedBy)", parameters);
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
        /// Lấy ra phòng ban bằng id
        /// </summary>
        /// <param name="id">id phòng ban</param>
        /// <returns></returns>
        /// Create By: BATUAN (14/06/2023)
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var error = new Error();
            MySqlConnection mySqlConnection = new MySqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("DepartmentId", id);
            var result = mySqlConnection.QueryFirstOrDefault<Department>(sql: "CALL Proc_Department_GetById(@DepartmentId)", parameters);
            if (result == null)
            {
                error.UserMsg = "Phòng ban không tồn tại!";
                return BadRequest(error);
            }
            return StatusCode(200, result);
        }
    }
}
