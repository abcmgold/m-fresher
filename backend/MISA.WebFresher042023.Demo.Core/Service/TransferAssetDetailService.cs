using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    public class TransferAssetDetailService : BaseService<TransferAssetDetail, TransferAssetDetailDto, TransferAssetDetailUpdateDto, TransferAssetDetailCreateDto>, ITransferAssetDetailService
    {
        private readonly ITransferAssetDetailRepository _propertyTransferRepository;

        public TransferAssetDetailService(ITransferAssetDetailRepository propertyTransferRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(propertyTransferRepository, mapper, unitOfWork)
        {
            _propertyTransferRepository = propertyTransferRepository;
        }

        /// <summary>
        /// Phân trang bản ghi lấy theo Id của chứng từ
        /// </summary>
        /// <param name="documentId">Id chứng từ</param>
        /// <param name="pageNumber">Trang số mấy?</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <returns>Danh sách chi tiết chứng từ</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public async Task<List<TransferAssetDetailDto>> GetPropertyTransferByDocumentId(Guid documentId, int pageNumber, int pageSize)
        {
            List<TransferAssetDetail> propertyTransferList = await _propertyTransferRepository.GetPropertyTransferByDocumentId(documentId, pageNumber, pageSize);
            List<TransferAssetDetailDto> propertyTransferDtoList = new();
            foreach (var propertyTransfer in propertyTransferList)
            {
                var propertyTransferDto = _mapper.Map<TransferAssetDetailDto>(propertyTransfer);
                propertyTransferDtoList.Add(propertyTransferDto);

            }
            return propertyTransferDtoList;
        }
    }
}
