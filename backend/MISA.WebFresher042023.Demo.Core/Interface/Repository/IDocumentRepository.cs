using MISA.WebFresher042023.Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Repository
{
    public interface IDocumentRepository : IBaseRepository<Document>
    {
        public Task<object> GetByPaging(int pageNumber, int pageSize);
    }

}
