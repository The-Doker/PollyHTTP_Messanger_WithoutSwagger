using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PollyHTTP_Messanger_WithoutSwagger.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Random rnd = new Random();
            var digit = rnd.Next(1, 10);
            if (digit < 5)
                return StatusCode(500);
            return Ok("Digit >= 5");
        }
    }
}
