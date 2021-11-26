using JMail.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMail.NET.Datastore
{
    public static class RelayData
    {
        public static string[] Domains = new[] { "example.com", "example2.com" };
        
        /// <summary>
        /// The IP Address of the relay
        /// </summary>
        public static RelayAddress Address = new RelayAddress()
        {
            IP = "123.456.789.123",
            Port = 6051
        };

        /// <summary>
        /// In KB
        /// </summary>
        public static long MaxLetterSize = 1024 * 10;
    }
}
