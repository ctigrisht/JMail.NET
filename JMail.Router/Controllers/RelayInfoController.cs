using JMail.NET.Datastore;
using JMail.NET.Lib;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using JMail.NET.Models;
using JMail.Relay.Hook;

namespace JMail.Relay.Controllers
{
    [ApiController]
    [Route("jmailctl/relay")]
    public class RelayInfoController : ControllerBase
    {
        [HttpPost("info")]
        public IActionResult Info()
        {
            return Ok(new Dictionary<string, dynamic>
            {
                {"domains", RelayData.Domains },
            });
        }

        [HttpPost("getkey")]
        public IActionResult GetKey()
        {
            return Ok(Encryption.GetRsaPublicKey());
        }

        [HttpPost("mail")]
        public IActionResult Mail([FromForm] JMailLetterEncrypted letter)
        {
            var ip = HttpContext.Request.Host.Host;
            var decrypted = letter.Receive(ip);


            return Ok();
        }
    }
}
