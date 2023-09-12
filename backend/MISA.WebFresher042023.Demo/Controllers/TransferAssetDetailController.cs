using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Core.Service;

namespace MISA.WebFresher042023.Demo.Controllers
{
    /// <summary>
    /// Lớp controller của chi tiết chứng từ
    /// </summary>
    /// CreatedBy: BATUAN (28/08/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransferAssetDetailController : BaseController<TransferAssetDetailDto, TransferAssetDetailUpdateDto, TransferAssetDetailCreateDto>
    {
        private readonly ITransferAssetDetailService _transferAssetDetailService;

        public TransferAssetDetailController(ITransferAssetDetailService transferAssetDetailService) : base(transferAssetDetailService)
        {
            _transferAssetDetailService = transferAssetDetailService;
        }
        /// <summary>
        /// Tìm kiếm và phân trang chi tiết chứng từ theo id chứng từ 
        /// </summary>
        /// <param name="assetDetailId">Id của chứng từ</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <returns>Danh sách chi tiết chứng từ</returns>
        /// CreatedBy: BATUAN (28/08/2023)
        [HttpGet]
        [Route("FilterByTransferAssetId")]
        public async Task<ActionResult> GetByDocumentId(Guid assetDetailId, int pageNumber, int pageSize)
        {
            var res = await _transferAssetDetailService.GetPropertyTransferByDocumentId(assetDetailId, pageNumber, pageSize);

            return Ok(res);
        }

    }
}
