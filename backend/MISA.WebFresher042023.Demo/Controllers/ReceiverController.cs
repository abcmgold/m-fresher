using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Interface.Service;

namespace MISA.WebFresher042023.Demo.Controllers
{
    /// <summary>
    ///  Lớp controller của ban giao nhận
    /// </summary>
    /// CreatedBy: BATUAN (20/08/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReceiverController : ControllerBase
    {
        private readonly IReceiverService _receiverService;
        public ReceiverController(IReceiverService receiverService) {
            _receiverService = receiverService;
        }
        /// <summary>
        /// Lấy ra danh sách người nhận gần nhất được thêm vào
        /// </summary>
        /// <returns>Danh sách người nhận</returns>
        /// CreatedBy: BATUAN (20/08/2023)
        [HttpGet]
        public async Task<ActionResult> GetLastestReceiver()
        {
            var receiver = await _receiverService.GetLastestReceivers();

            return Ok(receiver);
        }
    }
}
