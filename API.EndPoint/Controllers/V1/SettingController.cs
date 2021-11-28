using Application.Interface.Setting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.EndPoint.Controllers.V1
{
    [ApiVersion("1")]
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly IAppSettingService _appSettingService;

        public SettingController(IAppSettingService appSettingService)
        {
            _appSettingService = appSettingService;
        }
        [HttpGet]
        public IActionResult Setting(string key)
        {
            var result=  _appSettingService.Excecute(key);
            return Ok(result);
        }
    }
}
