using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Dto.Document;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Core.Service;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DocumentController : BaseController<DocumentDto, DocumentUpdateDto, DocumentCreateDto>
    {
        private readonly IDocumentService _documentService;
        public DocumentController(IDocumentService documentService) : base(documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        [Route("Paging")]
        public async Task<ActionResult> GetByPaging(int pageNumber, int pageSize)
        {
            var res = await _documentService.GetByPaging(pageNumber, pageSize);

            return Ok(res);
        }
    }
}
