using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.Document;
using MISA.WebFresher042023.Demo.Core.Dto.Property;
using MISA.WebFresher042023.Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Mapper
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<TransferAsset, TransferAssetDto>();
            CreateMap<TransferAssetCreateDto, TransferAsset>();
            CreateMap<TransferAssetUpdateDto, TransferAsset>();
        }
    }
}
