using Authentication.Models;
using Authentication.Services;
using Microsoft.AspNetCore.Mvc;
using Utils.Extensions;

namespace Authentication.Controllers
{
    [Route("[controller]")]
    public class SSS091Controller : ControllerBase
    {
        private readonly ISSS091Service service;
        private readonly ILogger<SSS091Controller> logger;

        public SSS091Controller(
            ISSS091Service service,
            ILogger<SSS091Controller> logger
        )
        {
            this.service = service;
            this.logger = logger;
        }
        [HttpPost]
        [Route("userlogtrail")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> SearchUserLOgtrail([FromBody] UserLogTrailCriteriaDo oCriteria)
        {
            return Ok(await Task.FromResult(this.service.getUserLogTrail(oCriteria)));
        }

    }
}