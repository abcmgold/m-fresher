using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Core.Service;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransferAssetDetailController : BaseController<TransferAssetDetailDto, TransferAssetDetailUpdateDto, TransferAssetDetailCreateDto>
    {
        private readonly ITransferAssetDetailService _transferAssetDetailService;

        public TransferAssetDetailController(ITransferAssetDetailService transferAssetDetailService) : base(transferAssetDetailService)
        {
            _transferAssetDetailService = transferAssetDetailService;
        }
        [HttpGet]
        [Route("FilterByTransferAssetId")]
        public async Task<ActionResult> GetByDocumentId(Guid assetDetailId, int pageNumber, int pageSize)
        {
            var res = await _transferAssetDetailService.GetPropertyTransferByDocumentId(assetDetailId, pageNumber, pageSize);

            return Ok(res);
        }

    }
}
