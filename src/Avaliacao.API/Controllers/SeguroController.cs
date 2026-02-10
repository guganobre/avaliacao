using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Avaliacao.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguroController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok();
        }
    }
}