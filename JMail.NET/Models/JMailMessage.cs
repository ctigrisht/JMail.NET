using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMail.NET.Models
{

    public record JMailMessage
    {
        [NotNull] public JMailMessageSender Sender { get; set; }
        [NotNull] public JMailMessageRecipient Recepient { get; set; }
        [NotNull] public JMailMessageContent Content { get; set; }
        [NotNull] public DateTime Date { get; set; }
        
        /// <summary>
        /// 0 is highest priority
        /// </summary>
        public short MessagePriority { get; set; } = 10;

    }



    public record JMailMessageSender
    {
        public string IPAddress = string.Empty;
        public string Domain = string.Empty;
        public string User = string.Empty;
    }

    public record JMailMessageRecipient
    {
        public string Domain = string.Empty;
        public string User = string.Empty;
    }
}
