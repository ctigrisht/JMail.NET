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
    public class RelayController : ControllerBase
    {
        [HttpPost("info")]
        public IActionResult Info()
        {
            return Ok(new Dictionary<string, dynamic>
            {
                {"Domains", RelayData.Domains },
                {"MaxLetterSize", RelayData.MaxLetterSize }
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
            var decryptedResult = letter.Receive(ip);
            var decrypted = decryptedResult.Letter;

            // do something with the letter
            decrypted.IncomingJMailHandler();

            return Ok();
        }
    }
}
