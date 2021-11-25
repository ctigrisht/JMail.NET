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
        [NotNull] public JMailMessageRecipient Recipient { get; set; }
        [NotNull] public JMailMessageContent Content { get; set; }
    }






}
