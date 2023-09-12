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
    /// <summary>
    /// Lớp controller của chứng từ điều chuyển
    /// </summary>
    /// CreatedBy: BATUAN (20/08/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransferAssetController : ControllerBase
    {
        private readonly ITransferAssetService _documentService;
        public TransferAssetController(ITransferAssetService documentService)
        {
            _documentService = documentService;
        }

        /// <summary>
        /// Api phân trang chứng từ
        /// </summary>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <returns>Danh sách chứng từ</returns>
        /// CreatedBy: BATUAN (28/08/2023)
        [HttpGet]
        [Route("Paging")]
        public async Task<ActionResult> GetByPaging(int pageNumber, int pageSize)
        {
            var res = await _documentService.GetByPaging(pageNumber, pageSize);

            return Ok(res);
        }
        /// <summary>
        /// Thêm mới 1 chứng từ
        /// </summary>
        /// <param name="documentCreateDto">Dto của chứng từ được thêm mới</param>
        /// <returns></returns>
        /// CreatedBy: BATUAN (28/08/2023)
        [HttpPost]
        [Route("Insert")]
        public async Task<ActionResult> Post(TransferAssetCreateDto documentCreateDto)
        {

            var res = await _documentService.AddDocumentAsync(documentCreateDto);

            return StatusCode(201, res);
        }

        /// <summary>
        /// Api lấy danh sách thông tin chứng từ theo id
        /// </summary>
        /// <param name="transferAssetId">Id chứng từ</param>
        /// <returns>Thông tin của chứng từ</returns>
        /// CreatedBy: BATUAN (28/08/2023)
        [HttpGet]
        [Route("GetInfo")]
        public async Task<ActionResult> GetInfo(Guid transferAssetId)
        {

            var res = await _documentService.GetInfoTransferAsset(transferAssetId);

            return StatusCode(201, res);
        }
        /// <summary>
        /// Check tài sản trong chứng từ có được xóa ở form sửa hay không
        /// </summary>
        /// <param name="transferAssetId">Id chứng từ</param>
        /// <param name="propertyIds">Danh sách Id tài sản cần kiểm tra</param>
        /// CreatedBy: BATUAN (28/08/2023)
        [HttpPost]
        [Route("CheckTransferAsset")]
        public async Task<ActionResult> CheckTransferAsset(Guid transferAssetId, List<Guid> propertyIds)
        {
                await _documentService.CheckDeleteOrNot(transferAssetId, propertyIds);
           
            return Ok();
        }
        /// <summary>
        /// Update chứng từ
        /// </summary>
        /// <param name="transferAssetUpdateDtos">Chứng từ update</param>
        /// <returns></returns>
        /// CreatedBy: BATUAN (28/08/2023)
        [HttpPut]
        public virtual async Task<ActionResult> Update(List<TransferAssetUpdateDto> transferAssetUpdateDtos)
        {
            var res = await _documentService.UpdateDocumentAsync(documentUpdateDto: transferAssetUpdateDtos[0]);

            return StatusCode(200, res);

        }
        /// <summary>
        /// Xóa nhiều chứng từ
        /// </summary>
        /// <param name="listId">Danh sách Id chứng từ cần xóa</param>
        /// <returns>Số bản ghi được xóa</returns>
        /// CreatedBy: BATUAN (28/08/2023)
        [HttpDelete]
        public async Task<IActionResult> Delete(List<Guid> listId)
        {
            var res = await _documentService.DeleteAsync(listId);

            return StatusCode(200, res);

        }
        /// <summary>
        /// Sinh mã code chứng từ tự động
        /// </summary>
        /// <returns>Chuỗi code được sinh ra</returns>
        /// CreatedBy: BATUAN (28/08/2023)
        [HttpGet]
        [Route("GetAutoCode")]
        public async Task<IActionResult> GetAutoCode()
        {
            var res = await _documentService.GetAutoTransferAssetCode();

            return StatusCode(200, res);

        }
    }
}
