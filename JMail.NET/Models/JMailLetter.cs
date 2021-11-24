using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMail.NET.Models
{
    public record JMailLetter
    {
        public string Sender = string.Empty;
        public string Recipient = string.Empty;
        public string EncryptedContent = string.Empty;
    }
}
