using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMail.NET.Models
{
    public record JMailMessageRecipient
    {
        public string Domain = string.Empty;
        public string User = string.Empty;
    }
}
