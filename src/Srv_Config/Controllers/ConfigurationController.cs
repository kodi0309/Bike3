using Srv_Config.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Srv_Config.Controllers
{
    [ApiController]
    [Route("api/configurations")]
    public class ConfigurationController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> SaveConfiguration([FromBody] SelectedConfiguration config)
        {
            if (config == null)
            {
                return BadRequest("Configuration data is required.");
            }

            await DB.SaveAsync(config);
            return Ok(new { message = "Configuration saved successfully" });
        }

        [HttpGet]
        public async Task<ActionResult<List<SelectedConfiguration>>> GetConfigurations()
        {
            var configurations = await DB.Find<SelectedConfiguration>().ExecuteAsync();
            return Ok(configurations);
        }
    }
}
