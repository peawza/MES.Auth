using Authentication.Models;
using Authentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utils.Extensions;

namespace Authentication.Controllers
{
    [Authorize]
    [Route("api/v1/auth/[controller]")]
    public class SSS070Controller : ControllerBase
    {
        private readonly ISSS070Service service;
        private readonly ILogger<SSS070Controller> logger;

        public SSS070Controller(
            ISSS070Service service,
            ILogger<SSS070Controller> logger
        )
        {
            this.service = service;
            this.logger = logger;
        }

        [HttpPost]
        [Route("SearchMenu")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> SearchMenu([FromBody] MenuSearchCriteriaDo oCriteria)
        {
            return Ok(await Task.FromResult(this.service.SearchMenu(oCriteria)));
        }
        [HttpPost]
        [Route("SearchScreenForMenu")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> SearchScreenForMenu([FromBody] ScreenSearchCriteriaDo oCriteria)
        {
            return Ok(await Task.FromResult(this.service.SearchScreenForMenu(oCriteria)));
        }
        [HttpPost]
        [Route("GetMenu")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> GetMenu([FromBody] MenuCriteriaDo oCriteria)
        {
            return Ok(await Task.FromResult(this.service.GetMenu(oCriteria)));
        }
        [HttpPost]
        [Route("AddMenu")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> AddMenu([FromBody] MenuUpdateDo oMenu)
        {
            oMenu.CreateDate = Utils.Extensions.IOUtil.GetCurrentDateTime;
            oMenu.CreateBy = ClaimHelper.GetUserNumber(User.Claims);

            return Ok(await Task.FromResult(this.service.AddMenu(oMenu)));
        }
        [HttpPost]
        [Route("UpdateMenu")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> UpdateMenu([FromBody] MenuUpdateDo oMenu)
        {
            oMenu.UpdateDate = Utils.Extensions.IOUtil.GetCurrentDateTime;
            oMenu.UpdateBy = ClaimHelper.GetUserNumber(User.Claims);

            return Ok(await Task.FromResult(this.service.UpdateMenu(oMenu)));
        }
        [HttpPost]
        [Route("DeleteMenu")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> DeleteMenu([FromBody] MenuUpdateDo oMenu)
        {
            return Ok(await Task.FromResult(this.service.DeleteMenu(oMenu)));
        }

        [HttpPost]
        [Route("UpdateMenuSeq")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> UpdateMenuSeq([FromBody] MenuSeqUpdateDo oMenu)
        {
            oMenu.UpdateDate = Utils.Extensions.IOUtil.GetCurrentDateTime;
            oMenu.UpdateBy = ClaimHelper.GetUserNumber(User.Claims);

            await Task.Run(() => { this.service.UpdateMenuSeq(oMenu); });
            return Ok(true);
        }
    }
}