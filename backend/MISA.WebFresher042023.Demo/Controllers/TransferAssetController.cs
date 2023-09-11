using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Dto.Document;
using MISA.WebFresher042023.Demo.Core.Dto.Hanover;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Core.Service;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransferAssetController : ControllerBase
    {
        private readonly ITransferAssetService _documentService;
        public TransferAssetController(ITransferAssetService documentService)
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
        [HttpPost]
        [Route("Insert")]
        public async Task<ActionResult> Post(TransferAssetCreateDto documentCreateDto)
        {

            var res = await _documentService.AddDocumentAsync(documentCreateDto);

            return StatusCode(201, res);
        }

        [HttpGet]
        [Route("GetInfo")]
        public async Task<ActionResult> GetInfo(Guid transferAssetId)
        {

            var res = await _documentService.GetInfoTransferAsset(transferAssetId);

            return StatusCode(201, res);
        }
        [HttpPost]
        [Route("CheckTransferAsset")]
        public async Task<ActionResult> CheckTransferAsset(Guid transferAssetId, List<Guid> propertyIds)
        {
            if (propertyIds.Count == 0)
            {
                return BadRequest();
            }
            else if (propertyIds.Count == 1)
            {
                await _documentService.CheckDeleteOrNot(transferAssetId, propertyIds[0]);
            }
            else if (propertyIds.Count > 1)
            {
                await _documentService.CheckDeleteMultiOrNot(transferAssetId, propertyIds);
            }
            return Ok();
        }
        [HttpPut]
        public virtual async Task<ActionResult> Update(List<TransferAssetUpdateDto> transferAssetUpdateDtos)
        {
            var res = await _documentService.UpdateDocumentAsync(documentUpdateDto: transferAssetUpdateDtos[0]);

            return StatusCode(200, res);

        }
        [HttpDelete]
        public async Task<IActionResult> Delete(List<Guid> listId)
        {
            var res = await _documentService.DeleteAsync(listId);

            return StatusCode(200, res);

        }
        [HttpGet]
        [Route("GetAutoCode")]
        public async Task<IActionResult> GetAutoCode()
        {
            var res = await _documentService.GetAutoTransferAssetCode();

            return StatusCode(200, res);

        }
    }
}
