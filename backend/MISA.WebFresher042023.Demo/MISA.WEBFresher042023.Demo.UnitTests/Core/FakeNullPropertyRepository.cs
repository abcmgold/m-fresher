using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEBFresher042023.Demo.UnitTests.Core
{
    public class FakeNullPropertyRepository : IPropertyRepository
    {
        public Task<Property> checkDuplicateProeprtyCode(string propertyCode)
        {
            throw new NotImplementedException();
        }

        public Task<Property> CheckDuplicatePropertyCode(string propertyCode)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteMultiAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Property>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAutoIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Property> GetByIdAsync(Guid id)
        {
            return Task.FromResult<Property>(null);
        }

        public Task<object> GetByPagingAsync(int pageNumber, int pageSize, string? searchInput, string? propertyType, string? departmentName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetLastestCodeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(Property entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertPropertyAsync(Property property)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Guid id, Property entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatePropertyAsync(Guid propertyId, Property property)
        {
            throw new NotImplementedException();
        }
    }
}
