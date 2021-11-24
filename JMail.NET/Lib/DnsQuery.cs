using DnsClient;
using DnsClient.Protocol;
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
        private static LookupClient _dnsClient = new LookupClient(NameServer.Cloudflare);

        public static async Task<bool> VerifyMessageSender(this JMailLetter letter, string ip)
        {

            return default;
        }
        private static (HostString, IPAddress) _getPair()
        {
            return default;
        }

        private static IEnumerable<string> _parseTxtRecord(IEnumerable<TxtRecord> records)
        {
            string[] ret = new string[0];

            bool found = false;
            foreach (var record in records)
            {
                var text = record.Text.FirstOrDefault();
                if (string.IsNullOrWhiteSpace(text)) continue;
                if (!text.Contains("JMAIL:")) continue;
                found = true;
                ret = text.Split('=', StringSplitOptions.RemoveEmptyEntries)[1].Split(',');

                var bak = ret;
                ret = new string[0];
                foreach (var item in bak)
                    ret.Append(item.Trim());
                
                break;
            }
            if (!found) throw new TxtRecordNotFoundException();
            return ret;
        }
        private static IEnumerable<TxtRecord> _getTxtRecords(string domain) => _dnsClient.Query(domain, QueryType.TXT).Answers.TxtRecords();
    }

    [Serializable]
    public class TxtRecordNotFoundException : Exception
    {

    }
}
