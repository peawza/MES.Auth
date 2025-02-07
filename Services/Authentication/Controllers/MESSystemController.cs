using Authentication.Services;
using Microsoft.AspNetCore.Mvc;
using static Authentication.Models.MESScreenModel;

namespace Authentication.Controllers
{
    [Route("messystem")]
    public class MESSystemController : ControllerBase
    {
        private readonly IMESScreenService service_Screen;
        private readonly IResouresService service_Resoures;
        private readonly ILogger<MESSystemController> logger;

        public MESSystemController(
            IMESScreenService service_Screen,
            IResouresService service_Resoures,
            ILogger<MESSystemController> logger
        )
        {
            this.service_Screen = service_Screen;
            this.service_Resoures = service_Resoures;
            this.logger = logger;
        }


        [HttpGet]
        [Route("getscreen")]
        //[TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> GetScreen(MESScreenCriteria criteria)
        {
            // LocalizedMessagesCriteria localizedMessagesCriteria = new LocalizedMessagesCriteria();
            var results = await service_Screen.getMesScreen(criteria);
            return Ok(results);
        }

        [HttpGet]
        [Route("getresources")]
        //[TypeFilter(typeof(ActionExceptionFilter))]
        public IActionResult getResources()
        {
            // LocalizedMessagesCriteria localizedMessagesCriteria = new LocalizedMessagesCriteria();
            var results = service_Resoures.LocalizedResources(null);
            return Ok(results);
        }
        [HttpGet]
        [Route("getmessages")]
        //[TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> getMessages()
        {
            // LocalizedMessagesCriteria localizedMessagesCriteria = new LocalizedMessagesCriteria();
            var results = await service_Resoures.LocalizedMessages(null);
            return Ok(results);
        }


    }
}