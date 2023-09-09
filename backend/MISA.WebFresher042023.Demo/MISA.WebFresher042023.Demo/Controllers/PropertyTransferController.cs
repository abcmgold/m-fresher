using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Core.Service;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PropertyTransferController : BaseController<PropertyTransferDto, PropertyTransferUpdateDto, PropertyTransferCreateDto>
    {
        private readonly IPropertyTransferService _propertyTransferService;

        public PropertyTransferController(IPropertyTransferService propertyTransferService) : base(propertyTransferService)
        {
            _propertyTransferService = propertyTransferService;
        }
        [HttpGet]
        [Route("FilterByDocumentId")]
        public async Task<ActionResult> GetByDocumentId(Guid documentId, int pageNumber, int pageSize)
        {
            var res = await _propertyTransferService.GetPropertyTransferByDocumentId(documentId, pageNumber, pageSize);

            return Ok(res);
        }

    }
}
