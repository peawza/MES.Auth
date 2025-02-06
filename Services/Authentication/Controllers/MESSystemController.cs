using Authentication.Services;
using Microsoft.AspNetCore.Mvc;
using static Authentication.Models.MESScreenModel;

namespace Authentication.Controllers
{
    [Route("messystem")]
    public class MESSystemController : ControllerBase
    {
        private readonly IMESScreenService service;
        private readonly ILogger<MESSystemController> logger;

        public MESSystemController(
            IMESScreenService service,
            ILogger<MESSystemController> logger
        )
        {
            this.service = service;
            this.logger = logger;
        }


        [HttpGet]
        [Route("getscreen")]
        //[TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> GetScreen(MESScreenCriteria criteria)
        {
            // LocalizedMessagesCriteria localizedMessagesCriteria = new LocalizedMessagesCriteria();
            var results = await service.getMesScreen(criteria);
            return Ok(results);
        }



    }
}