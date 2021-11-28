using Application.Service.Setting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.EndPoint.Controllers.V1
{
    [ApiVersion("1")]//[ApiVersion("1", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public virtual IEnumerable<string> Get(string get)
        {
            return new string[] { "value1", "value2" };
        }
    }
}
