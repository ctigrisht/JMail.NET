﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMail.NET.Models
{
    public record JMailLetterDecrypted
    {
        public string Sender = string.Empty;
        public string Recipient = string.Empty;
        public DateTime DateReceived;
        public JMailMessage Message = null;
    }
}