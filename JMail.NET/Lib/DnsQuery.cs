using DnsClient;
using DnsClient.Protocol;
using JMail.NET.Datastore;
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
    // TXT: "JMAIL=123.456.789.123:6051,987.654.321.987:6051"
    public static class DnsQuery
    {
        private static LookupClient _dnsClient = new LookupClient(NameServer.Cloudflare);

        public static void VerifyMessageSender(this string domain, string ip)
        {
            //string sender;
            //string domain;

            ////TODO use regex to validate address
            //try
            //{
            //    var senderData = letter.Sender.Split('#');
            //    sender = senderData[0];
            //    domain = senderData[1];
            //}catch (Exception e)
            //{
            //    throw new InvalidJMailAddressException("Invalid JMail Address, please use 'user#example.com'", e);
            //}

            //first check cache
            RelayAddresses addresses = null;

            if (DnsRecordsCache.Contains(domain))
                addresses = DnsRecordsCache.Get(domain);
            else
            {
                addresses = _parseTxtRecord(_getTxtRecords(domain));
                _cacheTxtRecord(domain, addresses);
            }
            if (!_verifyIP(ref addresses, ip)) throw new UnauthorizedJMailSenderException($"'{ip}' is not an authorized relay of '{domain}'");


        }

        public static RelayAddress GetPrimaryAddressFromDomain(string domain)
        {
            try
            {
                RelayAddresses addresses = null;

                if (DnsRecordsCache.Contains(domain))
                    addresses = DnsRecordsCache.Get(domain);
                else
                {
                    addresses = _parseTxtRecord(_getTxtRecords(domain));
                    _cacheTxtRecord(domain, addresses);
                }

                return addresses.Addresses.First();
            }catch (Exception e)
            {
                throw new InvalidJMailTxtRecordException("The TXT record did not contain any valid IP(s)", e);
            }
        }

        private static void _cacheTxtRecord(string domain, RelayAddresses addresses) => DnsRecordsCache.Override(domain, addresses);
        private static bool _verifyIP(ref RelayAddresses addresses, string ip) => addresses.Addresses.Any(x => x.Address == ip);

        private static RelayAddresses _parseTxtRecord(IEnumerable<TxtRecord> records)
        {
            string[] adrs;
            var addresses = new RelayAddresses();
            bool found = false;

            foreach (var record in records)
            {
                try
                {
                    var text = record.Text.FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(text)) continue;
                    if (!text.Contains("JMAIL:")) continue;

                    found = true;
                    adrs = text.Split('=', StringSplitOptions.RemoveEmptyEntries)[1].Split(',');

                    foreach (var value in adrs)
                    {
                        var split = value.Split(':');
                        addresses.Add(new RelayAddress()
                        {
                            Address = split[0],
                            Port = int.Parse(split[1])
                        });
                    }

                }catch (Exception e)
                {
                    throw new InvalidJMailTxtRecordException("The JMAIL TXT record is not valid", e);
                }
                //var bak = ret;
                //ret = new string[0];
                //foreach (var item in bak)
                //    ret.Append(item.Trim());

                break;
            }
            if (!found) throw new TxtRecordNotFoundException($"Valid JMail TXT record was not found");
            return addresses;
        }

        private static IEnumerable<TxtRecord> _getTxtRecords(string domain)
        {
            var results = _dnsClient.Query(domain, QueryType.TXT).Answers.TxtRecords();
            if (results.Count() is 0) throw new TxtRecordNotFoundException($"No TXT records were found for {domain}");
            return results;
        } 
    }

    [Serializable]
    public class TxtRecordNotFoundException : Exception
    {
        public TxtRecordNotFoundException() : base() { }
        public TxtRecordNotFoundException(string message) : base(message) { }
        public TxtRecordNotFoundException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected TxtRecordNotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class InvalidJMailTxtRecordException : Exception
    {
        public InvalidJMailTxtRecordException() : base() { }
        public InvalidJMailTxtRecordException(string message) : base(message) { }
        public InvalidJMailTxtRecordException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected InvalidJMailTxtRecordException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class UnauthorizedJMailSenderException : Exception
    {
        public UnauthorizedJMailSenderException() : base() { }
        public UnauthorizedJMailSenderException(string message) : base(message) { }
        public UnauthorizedJMailSenderException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected UnauthorizedJMailSenderException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


    [Serializable]
    public class InvalidJMailAddressException : Exception
    {
        public InvalidJMailAddressException() : base() { }
        public InvalidJMailAddressException(string message) : base(message) { }
        public InvalidJMailAddressException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected InvalidJMailAddressException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
