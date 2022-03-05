using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PollyClientASP.Policies;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace PollyClientASP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IPolicy _policy;
        public ClientController(IPolicy policy)
        {
            _policy = policy;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var url = "https://localhost:5001/messages";
            var data = "";
            try
            {
                return await _policy.InternalServerErrorPolicy.ExecuteAsync(async () =>
                {
                    var request = WebRequest.Create(url);
                    request.Method = "GET";
                    using var webResponse = (HttpWebResponse)request.GetResponse();
                    using var webStream = webResponse.GetResponseStream();
                    using var reader = new StreamReader(webStream);
                    data = reader.ReadToEnd();
                    data += "\n" + reader.ReadToEnd();
                    Console.WriteLine("Ok from service");
                    return Ok(data);
                });
            }
            catch
            {
                return StatusCode(500);
            }
            return StatusCode(400);
        }
    }
}
