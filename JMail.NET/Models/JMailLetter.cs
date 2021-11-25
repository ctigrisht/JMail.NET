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
        public JMailLetter(
            string recipient, 
            string sender, 
            string header, 
            string message, 
            string footer, 
            
            string name = null, 
            bool html = true, 
            JMailMessageFile[]? files = null, 
            short priority = 10)
        {
            var senderSplit = sender.Split('#');
            var recipientSplit = recipient.Split('#');

            Origin = senderSplit[1];
            Sender = sender;

            Target = recipientSplit[1];
            Recipient = recipient;

            Message = new JMailMessage
            {
                Sender = new JMailMessageSender
                {
                    Name = name ?? senderSplit[0],
                    User = senderSplit[0],
                    Domain = senderSplit[1],
                },
                Recipient = new JMailMessageRecipient
                {
                    User = recipientSplit[0],
                    Domain = recipientSplit[1],
                },
                Content = new JMailMessageContent
                {
                    EnableHtml = html,
                    Files = files ?? new JMailMessageFile[0],
                    Header = header,
                    Body = message,
                    Footer = footer
                }
            };

        }
        public JMailLetter()
        {

        }



        public string Origin = string.Empty;
        public string Target = string.Empty;

        public string Sender = string.Empty;
        public string Recipient = string.Empty;
        public DateTime Date;
        public short Priority = 10;
        public JMailMessage Message = null;
    }
}
