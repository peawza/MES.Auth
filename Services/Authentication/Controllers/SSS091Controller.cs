using Microsoft.AspNetCore.Mvc;
using Authentication.Services;
using Authentication.Constants;
using Application.Models;
using Utils.Extensions;
using Authentication.Models;
using Utils.Services;
using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Authentication.Controllers
{
    [Route("api/v1/auth/[controller]")]
    public class SSS091Controller : ControllerBase
    {
        private readonly ISSS091Service service;
        private readonly ILogger<SSS070Controller> logger;

        public SSS091Controller(
            ISSS091Service service,
            ILogger<SSS070Controller> logger
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