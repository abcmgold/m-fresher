using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.Department;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    /// <summary>
    /// Lớp thực thi của IDepartmentService
    /// </summary>
    /// CreatedBy: BATUAN (20/06/2023)
    public class DepartmentService : BaseService<Department, DepartmentDto,DepartmentUpdateDto, DepartmentCreateDto>, IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(departmentRepository, mapper, unitOfWork)
        {
            _departmentRepository = departmentRepository;
        }
    }
}
