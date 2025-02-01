using Authentication.Models;
using Authentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utils.Extensions;

namespace Authentication.Controllers
{
    [Authorize]
    [Route("api/v1/auth/[controller]")]
    public class SSS060Controller : ControllerBase
    {
        private readonly ISSS060Service service;
        private readonly ILogger<SSS060Controller> logger;

        public SSS060Controller(
            ISSS060Service service,
            ILogger<SSS060Controller> logger
        )
        {
            this.service = service;
            this.logger = logger;
        }

        [HttpPost]
        [Route("SearchScreen")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> SearchScreen([FromBody] ScreenSearchCriteriaDo oCriteria)
        {
            return Ok(await Task.FromResult(this.service.SearchScreen(oCriteria)));
        }
        [HttpPost]
        [Route("GetScreen")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> GetScreen([FromBody] ScreenCriteriaDo oCriteria)
        {
            return Ok(await Task.FromResult(this.service.GetScreen(oCriteria)));
        }
        [HttpPost]
        [Route("AddScreen")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> AddScreen([FromBody] ScreenUpdateDo oScreen)
        {
            oScreen.CreateDate = Utils.Extensions.IOUtil.GetCurrentDateTime;
            oScreen.CreateBy = ClaimHelper.GetUserNumber(User.Claims);

            return Ok(await Task.FromResult(this.service.AddScreen(oScreen)));
        }
        [HttpPost]
        [Route("UpdateScreen")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> UpdateScreen([FromBody] ScreenUpdateDo oScreen)
        {
            oScreen.UpdateDate = Utils.Extensions.IOUtil.GetCurrentDateTime;
            oScreen.UpdateBy = ClaimHelper.GetUserNumber(User.Claims);

            return Ok(await Task.FromResult(this.service.UpdateScreen(oScreen)));
        }
        [HttpPost]
        [Route("DeleteScreen")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> DeleteScreen([FromBody] ScreenUpdateDo oScreen)
        {
            return Ok(await Task.FromResult(this.service.DeleteScreen(oScreen)));
        }

        [HttpPost]
        [Route("UpdateScreenSeq")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> UpdateScreenSeq([FromBody] ScreenSeqUpdateDo oScreen)
        {
            oScreen.UpdateDate = Utils.Extensions.IOUtil.GetCurrentDateTime;
            oScreen.UpdateBy = ClaimHelper.GetUserNumber(User.Claims);

            await Task.Run(() => { this.service.UpdateScreenSeq(oScreen); });
            return Ok(true);
        }
    }
}