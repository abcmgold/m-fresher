using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Dto.Department;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MySqlConnector;

namespace MISA.WebFresher042023.Demo.Controllers
{
    /// <summary>
    /// Api của Depearment 
    /// </summary>
    /// CreatedBy: BATUAN (14/06/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController<DepartmentDto, DepartmentUpdateDto, DepartmentCreateDto>
    {

        #region Properties
        private readonly IDepartmentService _departmentService;

        #endregion

        #region Constructor
        public DepartmentController(IDepartmentService departmentService) : base(departmentService)
        {
            _departmentService = departmentService;
        } 
        #endregion
    }
}
