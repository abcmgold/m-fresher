using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.Document;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using System.Net.WebSockets;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    public class DocumentService : BaseService<Document, DocumentDto, DocumentUpdateDto, DocumentCreateDto>, IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository, IMapper mapper) : base(documentRepository, mapper)
        {
            _documentRepository = documentRepository;
        }

        public async Task<object> GetByPaging(int pageNumber, int pageSize)
        {
            var result = await _documentRepository.GetByPaging(pageNumber, pageSize);

            return result;
        }
    }
}
