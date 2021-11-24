using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMail.NET.Models
{
    public record JMailMessageFile
    {
        public string Base64EncodedData = string.Empty;
        public string FileName = string.Empty;
    }
}
