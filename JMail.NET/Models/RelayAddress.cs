using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMail.NET.Models
{
    public record RelayAddress
    {
        public string Address { get; set; }
        public int Port { get; set; }
    }
}
