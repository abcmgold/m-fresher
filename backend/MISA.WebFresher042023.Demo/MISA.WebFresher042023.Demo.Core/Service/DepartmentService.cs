﻿using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.Department;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    public class DepartmentService : BaseService<Department, DepartmentDto,DepartmentUpdateDto, DepartmentCreateDto>, IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper) : base(departmentRepository, mapper)
        {
            _departmentRepository = departmentRepository;
        }
    }
}
