using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Entities;
using MISA.WebFresher042023.Demo.HandleException;
using MySqlConnector;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace MISA.WebFresher042023.Demo.Controllers
{
    /// <summary>
    /// Api của Tài sản
    /// </summary>
    /// CreatedBy: BATUAN (14/06/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly string _connectionString;
        public PropertiesController(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration["ConnectionStrings"];
        }
        /// <summary>
        /// Lấy danh sách tất cả tài sản
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: BATUAN (14/06/2023)
        // GET: api/<PropertiesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Properties>>> Get()
        {

            // 1. Kết nối DB
            using (var mySqlConnection = new MySqlConnection(_connectionString))
            {
                await mySqlConnection.OpenAsync();
                // 2. Thực hiện truy vấn đếm số bản ghi
                int numberRecords = await mySqlConnection.ExecuteScalarAsync<int>("CALL Proc_Property_CountRecords();");

                // 3. Thực hiện truy vấn lấy tất cả bản ghi
                var result = await mySqlConnection.QueryAsync<Properties>("CALL Proc_Property_GetAll();");

                // 4. Trả về kết quả
                return Ok(new { Data = result, NumberRecords = numberRecords });
            }

        }

        /// <summary>
        /// Thêm tài sản
        /// </summary>
        /// <param name="property">tài sản được thêm</param>
        /// <returns></returns>
        /// CreatedBy: BATUAN (14/06/2023)
        // POST api/<PropertiesController>
        [HttpPost]
        public async Task<ActionResult> Post(Properties property)
        {

            var error = new Error();
            // 1. Kết nối với Database
            var mySqlConnection = new MySqlConnection(_connectionString);
            // 2. Thực hiện câu lệnh truy vấn
            property.PropertyId = Guid.NewGuid();
            var res = mySqlConnection.Execute(sql: "CALL Proc_Property_AddNew(@PropertyId, @PropertyCode, @PropertyName, @DepartmentId, @PropertyTypeId, @Quantity, @OriginalPrice, @NumberYearUse, @WearRate, @WearRateValue, @FollowYear, @PurchaseDate, @FollowDate, @CreatedBy, @ModifiedBy)", param: property);
            // 3. Trả về kết quả
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
        public IActionResult Update(Properties property)
        {
            var error = new Error();
            // 1. Kết nối với Database
            var mySqlConnection = new MySqlConnection(_connectionString);
            // 2. Tìm kiếm trong DB có id trên không 
            var parameters = new DynamicParameters();
            parameters.Add("PropertyId", property.PropertyId);
            var result = mySqlConnection.QueryFirstOrDefault<Department>("CALL Proc_Property_GetById(@PropertyId)", parameters);
            if (result == null)
            {
                error.UserMsg = "Tài sản không tồn tại";
                return BadRequest(error);
            }
            // 3.Thực hiện câu lệnh truy vấn
            //parameters.Add("DepartmentCode", department.DepartmentCode);
            //parameters.Add("DepartmentName", department.DepartmentName);
            //parameters.Add("ModifiedBy", department.ModifiedBy);
            var res = mySqlConnection.Execute(sql: "CALL Proc_Property_Update(@PropertyId, @PropertyCode, @PropertyName, @DepartmentId, @PropertyTypeId, @Quantity, @OriginalPrice, @NumberYearUse, @WearRate, @WearRateValue, @FollowYear, @PurchaseDate, @FollowDate, @CreatedBy, @ModifiedBy)", property);
            // 4.Trả về kết quả
            return StatusCode(200, res);

        }
        /// <summary>
        /// Xóa tài sản
        /// </summary>
        /// <param name="id">id tài sản</param>
        /// <returns></returns>
        /// CreatedBy: BATUAN (14/06/2023)
        // DELETE api/<PropertiesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(String id)
        {

            // 1. Kết nối với Database
            var mySqlConnection = new MySqlConnection(_connectionString);
            // 2. Thực hiện câu lệnh truy vấn
            var idArray = id.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            var idList = string.Join(",", idArray.Select(x => "\"" + x + "\""));

            var parameters = new DynamicParameters();
            parameters.Add("idList", idList);
            var res = mySqlConnection.Execute(sql: "CALL Proc_Property_MultiDelete(@idList)", parameters);
            // 3. Trả về kết quả
            return StatusCode(200, res);

        }

        /// <summary>
        /// Lấy ra tài sản bằng id
        /// </summary>
        /// <param name="id">id tài sản</param>
        /// <returns></returns>
        /// Create By: BATUAN (14/06/2023)
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var error = new Error();
            MySqlConnection mySqlConnection = new MySqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("PropertyId", id);
            var result = mySqlConnection.QueryFirstOrDefault<Properties>(sql: "CALL Pro_Property_GetById(@PropertyId)", parameters);
            if (result == null)
            {
                throw new NotFoundException();
            }
            return StatusCode(200, result);
        }
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
        [Route("filter")]
        public async Task<ActionResult> GetByPaging(int pageNumber, int pageSize, string? searchInput, string? propertyType, string? departmentName)
        {
            MySqlConnection mySqlConnection = new MySqlConnection(_connectionString);

            int numberRecords = await mySqlConnection.ExecuteScalarAsync<int>("CALL Proc_Property_CountRecords();");


            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);
            parameters.Add("SearchInput", searchInput);
            parameters.Add("PropertyType", propertyType);
            parameters.Add("DepartmentName", departmentName);

            var result = await mySqlConnection.QueryAsync<Properties>(sql: "CALL Proc_Property_PagingAndFilter(@PageNumber,@PageSize,@PropertyType,@DepartmentName,@SearchInput )", parameters);

            return Ok(new { Data = result, NumberRecords = numberRecords });
        }
        /// <summary>
        /// Lấy mã code mới được thêm vào gần nhất
        /// </summary>
        /// <returns></returns>
        /// Created by: BATUAN (16/06/2023)
        [HttpGet]
        [Route("GetLastestCode")]
        public async Task<ActionResult<string>> GetLastestCode()
        {
            // 1. Kết nối DB
            MySqlConnection mySqlConnection = new(_connectionString);
            // 2. Thực hiện truy vấn
            var result = await mySqlConnection.QueryFirstOrDefaultAsync<string>(sql: "CALL Proc_Property_GetLastestCode()");
            // 3. Trả về kết quả
            return Ok(result);
        }
    }
}
