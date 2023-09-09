using MISA.WebFresher042023.Demo.Core.Dto.Document;
using MISA.WebFresher042023.Demo.Core.Entities;

namespace MISA.WebFresher042023.Demo.Core.Interface.Service
{
    public interface IDocumentService : IBaseService<DocumentDto, DocumentUpdateDto, DocumentCreateDto>
    {
        public Task<object> GetByPaging(int pageNumber, int pageSize);
    }
}
