using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.Property;
using MISA.WebFresher042023.Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Mapper
{
    /// <summary>
    /// Lớp khai báo mapper của property
    /// </summary>
    /// CreatedBy: BATUAN(20/06/2023)
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<Property, PropertyDto>();
            CreateMap<PropertyCreateDto, Property>();
            CreateMap<PropertyUpdateDto, Property>();
        }
    }
}
