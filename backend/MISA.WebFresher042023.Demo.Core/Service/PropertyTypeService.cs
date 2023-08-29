﻿using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyType;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    public class PropertyTypeService : BaseService<PropertyType, PropertyTypeDto, PropertyTypeUpdateDto, PropertyTypeCreateDto>, IPropertyTypeService
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        public PropertyTypeService(IPropertyTypeRepository propertyTypeRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(propertyTypeRepository, mapper, unitOfWork)
        {
            _propertyTypeRepository = propertyTypeRepository;
        }
    }
}
