using JMail.NET.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMail.NET.Datastore
{
    public static class DnsRecordsCache
    {
        static DnsRecordsCache()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1000 * 60 * 59);
                    Console.WriteLine("[WORKER][DNS] Purging DNS cache");
                    Purge();
                }
            });
        }

        private static ConcurrentDictionary<string, RelayAddresses> _cache = new ConcurrentDictionary<string, RelayAddresses>();

        public static RelayAddresses Get(string domain)
        {
            if (_cache.TryGetValue(domain, out var result))
                return result;
            return null;
        }

        public static bool Contains(string domain) => _cache.ContainsKey(domain);
        public static void Override(string domain, RelayAddresses addresses) => _cache[domain] = addresses;
        public static void InsertAddress(string domain, RelayAddress address) => _cache[domain].Add(address);
        public static void RemoveAddress(string domain, Predicate<RelayAddress> filter) => _cache[domain].Remove(filter);
        public static void RemoveDomain(string domain, out RelayAddresses value) => _cache.Remove(domain, out value);
        public static void Purge() => _cache.Clear();

    }


}
