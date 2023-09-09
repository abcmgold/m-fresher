using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    public class PropertyTypeRepository : BaseRepository<PropertyType>, IPropertyTypeRepository
    {
        public PropertyTypeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
