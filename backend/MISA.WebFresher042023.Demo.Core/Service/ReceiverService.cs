using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.Hanover;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    public class ReceiverService : IReceiverService
    {
        private readonly IReceiverRepository _receiverRepository;
        private readonly IMapper _mapper;

        public ReceiverService(IReceiverRepository receiverRepository, IMapper mapper)
        {
            _receiverRepository = receiverRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Lấy danh sách người nhận ở chứng từ điều chuyển mới nhất
        /// </summary>
        /// <returns>Danh sách người nhận</returns>
        /// Created By: BATUAN (29/08/2023)
        public async Task<List<ReceiverDto>> GetLastestReceivers()
        {
            var receivers = await _receiverRepository.GetLastestReceivers();

            var receiversDto = _mapper.Map<List<ReceiverDto>>(receivers);

            return receiversDto;
        }
    }

}
