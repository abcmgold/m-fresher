﻿using Microsoft.AspNetCore.Http;
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
    public class TransferAssetController : BaseController<TransferAssetDto, TransferAssetUpdateDto, TransferAssetCreateDto>
    {
        private readonly ITransferAssetService _documentService;
        public TransferAssetController(ITransferAssetService documentService) : base(documentService)
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
    }
}
