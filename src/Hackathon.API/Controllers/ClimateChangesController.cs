using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Hackathon.API.Controllers
{
    [Route("ClimateChanges")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ClimateChangesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
    }
}