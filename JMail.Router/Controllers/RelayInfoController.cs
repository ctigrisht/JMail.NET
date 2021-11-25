using JMail.NET.Datastore;
using JMail.NET.Lib;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using JMail.NET.Models;

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
            
            try
            {
                var ip = HttpContext.Request.Host.Host;
                DnsQuery.VerifyMessageSender(letter.Origin, ip);


            }
            catch (UnauthorizedJMailSenderException e)
            {
                return BadRequest("Unauthorized Sender");
            }
            catch (InvalidJMailAddressException e)
            {
                return BadRequest("Invalid Address Format");
            }
            catch (Exception e)
            {
                return StatusCode(500, "An internal server error has occured");
            }

            

            return Ok();
        }
    }
}
