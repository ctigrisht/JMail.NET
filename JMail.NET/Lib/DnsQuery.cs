using JMail.NET.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JMail.NET.Lib
{
    public static class DnsQuery
    {
        public static async Task<bool> VerifyMessageSender(this JMailLetter letter, string ip)
        {
            return default;
        }
        private static (HostString, IPAddress) GetPair()
        {
            return default;
        }
        private static string _parseJMailTxtRecord(string[] txtRecords)
        {
            return default;
        }
        private static string[] _getTxtRecords(string domain)
        {
            return default;
        }
    }
}
