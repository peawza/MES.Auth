using Authentication.Models;
using Authentication.Services;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/v1/auth/[controller]")]
    public class ResourcesController : ControllerBase
    {
        private readonly IResouresService service;
        private readonly ILogger<ResourcesController> logger;

        public ResourcesController(
            IResouresService service,
            ILogger<ResourcesController> logger
        )
        {
            this.service = service;
            this.logger = logger;
        }
        [HttpGet]
        [Route("getresources")]
        //[TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> GetResources()
        {

            LocalizedResourcesCriteria oCriteria = new LocalizedResourcesCriteria();
            return Ok(await Task.FromResult(this.service.LocalizedResources(oCriteria)));

        }

        [HttpGet]
        [Route("getmessages")]
        //[TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> GetMessages()
        {
            LocalizedMessagesCriteria localizedMessagesCriteria = new LocalizedMessagesCriteria();

            return Ok(await Task.FromResult(this.service.LocalizedMessages(localizedMessagesCriteria)));

        }



    }
}