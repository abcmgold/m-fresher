using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.Hanover;
using MISA.WebFresher042023.Demo.Core.Dto.Property;
using MISA.WebFresher042023.Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Mapper
{
    public class HandoverProfile : Profile
    {
        public HandoverProfile()
        {
            CreateMap<Receiver, ReceiverDto>();
            CreateMap<ReceiverCreateDto, Receiver>();
            CreateMap<ReceiverUpdateDto, Receiver>();
        }
    }
}
