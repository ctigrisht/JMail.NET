using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMail.NET.Models
{
    public record JMailLetterReceiptResult
    {
        public bool Valid = false;
        public JMailLetterDecrypted Letter = null; 
    }
}
