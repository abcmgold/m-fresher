using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.Department;
using MISA.WebFresher042023.Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Mapper
{
    /// <summary>
    /// Lớp khai báo mapper của department
    /// </summary>
    /// CreatedBy: BATUAN(20/06/2023)
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>();
        }
    }
}
