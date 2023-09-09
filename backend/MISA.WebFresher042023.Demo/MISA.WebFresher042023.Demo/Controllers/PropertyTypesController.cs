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
    }
}

