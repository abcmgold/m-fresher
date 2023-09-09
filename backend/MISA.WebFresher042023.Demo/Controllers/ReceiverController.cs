using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Interface.Service;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReceiverController : ControllerBase
    {
        private readonly IReceiverService _receiverService;
        public ReceiverController(IReceiverService receiverService) {
            _receiverService = receiverService;
        }
        [HttpGet]
        public async Task<ActionResult> GetLastestReceiver()
        {
            var receiver = await _receiverService.GetLastestReceivers();

            return Ok(receiver);
        }
    }
}
