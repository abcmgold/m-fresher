using MISA.WebFresher042023.Demo.Core.Dto.PropertyType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Service
{
    /// <summary>
    /// Interface của propertyType làm việc với controller
    /// </summary>
    public interface IPropertyTypeService : IBaseService<PropertyTypeDto, PropertyTypeUpdateDto, PropertyTypeCreateDto>
    {
    }
}
