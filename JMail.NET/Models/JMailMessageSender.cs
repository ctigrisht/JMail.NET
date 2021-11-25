using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMail.NET.Models
{
    public record JMailMessageSender
    {
        public string Domain = string.Empty;
        public string User = string.Empty;
        public string Name = string.Empty;
    }
}
