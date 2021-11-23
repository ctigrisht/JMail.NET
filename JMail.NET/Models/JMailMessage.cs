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
        public JMailMessageSender Sender = null;
        public JMailMessageReceiver Receiver = null;
        public JMailMessageContent Content = null;
        public DateTime Date = DateTime.MinValue;
        /// <summary>
        /// 0 is highest priority
        /// </summary>
        public short MessagePriority = 10;

    }

    public class JMailMessageContent
    {
        /// <summary>
        /// Supports HTML format
        /// </summary>
        public string Message = string.Empty;
        public string[] Files = new string[0];
    }

    public class JMailMessageSender
    {
        public string IPAddress = string.Empty;
        public string Domain = string.Empty;
        public string User = string.Empty;
    }

    public class JMailMessageReceiver
    {
        public string Domain = string.Empty;
        public string User = string.Empty;
    }
}
