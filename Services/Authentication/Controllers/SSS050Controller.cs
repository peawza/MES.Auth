using Authentication.Models;
using Authentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utils.Extensions;

namespace Authentication.Controllers
{
    [Authorize]
    [Route("api/v1/auth/[controller]")]
    public class SSS050Controller : ControllerBase
    {
        private readonly ISSS050Service service;
        private readonly ILogger<SSS050Controller> logger;

        public SSS050Controller(
            ISSS050Service service,
            ILogger<SSS050Controller> logger
        )
        {
            this.service = service;
            this.logger = logger;
        }

        [HttpPost]
        [Route("SearchPermission")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> SearchRole([FromBody] PermissionSearchCriteriaDo oCriteria)
        {
            return Ok(await Task.FromResult(this.service.SearchPermission(oCriteria)));
        }
        [HttpPost]
        [Route("GetPermission")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> GetPermission([FromBody] PermissionDo oCriteria)
        {
            return Ok(await Task.FromResult(this.service.GetPermission(oCriteria)));
        }
        [HttpPost]
        [Route("AddPermission")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> AddPermission([FromBody] PermissionUpdateDo oPermission)
        {
            oPermission.CreateDate = Utils.Extensions.IOUtil.GetCurrentDateTime;
            oPermission.CreateBy = ClaimHelper.GetUserNumber(User.Claims);

            return Ok(await Task.FromResult(this.service.AddPermission(oPermission)));
        }
        [HttpPost]
        [Route("UpdatePermission")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> UpdatePermission([FromBody] PermissionUpdateDo oPermission)
        {
            oPermission.UpdateDate = Utils.Extensions.IOUtil.GetCurrentDateTime;
            oPermission.UpdateBy = ClaimHelper.GetUserNumber(User.Claims);

            return Ok(await Task.FromResult(this.service.UpdatePermission(oPermission)));
        }
        [HttpPost]
        [Route("DeletePermission")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> DeletePermission([FromBody] PermissionUpdateDo oPermission)
        {
            return Ok(await Task.FromResult(this.service.DeletePermission(oPermission)));
        }

        [HttpPost]
        [Route("UpdatePermissionSeq")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> UpdatePermissionSeq([FromBody] PermissionSeqUpdateDo oPermission)
        {
            oPermission.UpdateDate = Utils.Extensions.IOUtil.GetCurrentDateTime;
            oPermission.UpdateBy = ClaimHelper.GetUserNumber(User.Claims);

            await Task.Run(() => { this.service.UpdatePermissionSeq(oPermission); });
            return Ok(true);
        }
    }
}