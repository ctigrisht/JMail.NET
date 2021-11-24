using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMail.NET.Models
{
    public record JMailLetter
    {
        /// <summary>
        /// The letter origin domain name, unencrypted, used for sender verification
        /// </summary>
        public string Origin = string.Empty;
        public DateTime DateSent = DateTime.MinValue;

        /// <summary>
        /// Address format: user#example.com, encrypted on transport
        /// </summary>
        public string Sender = string.Empty;
        /// <summary>
        /// Default address format, encrypted on transport
        /// </summary>
        public string Recipient = string.Empty;
        public string EncryptedContent = string.Empty;
        public short Priority = 10;
    }


}
