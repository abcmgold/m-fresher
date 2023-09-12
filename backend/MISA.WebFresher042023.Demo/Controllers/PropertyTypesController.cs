using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyType;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.HandleException;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MySqlConnector;


namespace MISA.WebFresher042023.Demo.Controllers
{
    /// <summary>
    /// Lớp controller của mã loại tài sản
    /// </summary>
    /// CreatedBy: BATUAN (16/06/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PropertyTypeController : BaseController<PropertyTypeDto, PropertyTypeUpdateDto, PropertyTypeCreateDto>
    {
        private readonly IPropertyTypeService _propertyTypeService;
        public PropertyTypeController(IPropertyTypeService propertyTypeService) : base(propertyTypeService)
        {
            _propertyTypeService = propertyTypeService;
        }
    }
}

