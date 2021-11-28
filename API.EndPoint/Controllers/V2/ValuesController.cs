using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.EndPoint.Controllers.V2
{
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesController : V1.ValuesController
    {
        public override IEnumerable<string> Get(string get)
        {
               return new string[] { "V2 value1", "V2 value2", "V2 value3" };
        }
    }
}
