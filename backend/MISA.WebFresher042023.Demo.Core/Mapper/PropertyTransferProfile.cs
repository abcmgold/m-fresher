using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.Entities;

namespace MISA.WebFresher042023.Demo.Core.Mapper
{
    /// <summary>
    /// Lớp khai báo mapper của tài sản điều chuyển
    /// </summary>
    /// CreatedBy: BATUAN(20/06/2023)
    public class PropertyTransferProfile : Profile
    {
        public PropertyTransferProfile()
        {
            CreateMap<TransferAssetDetail, TransferAssetDetailDto>();
            CreateMap<TransferAssetDetailCreateDto, TransferAssetDetail>();
            CreateMap<TransferAssetDetailUpdateDto, TransferAssetDetail>();
        }

    }
}
