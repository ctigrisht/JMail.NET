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
        //public static string IPAddress = "123.456.789.123";
        //public static int Port = 6051;
        public static RelayAddress Address = new RelayAddress()
        {
            Address = "123.456.789.123",
            Port = 6051
        };
    }
}
