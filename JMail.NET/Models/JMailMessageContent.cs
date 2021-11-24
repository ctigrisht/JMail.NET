using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMail.NET.Models
{
    public record JMailMessageContent
    {
        public string Header = string.Empty;
        public string Footer = string.Empty;
        public string Message = string.Empty;
        public bool EnableHtml = true;
        public string[] Files = new string[0];
    }
}
