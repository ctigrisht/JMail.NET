using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMail.NET.Models
{
    public record RelayAddresses
    {
        public RelayAddresses(IEnumerable<RelayAddress> addresses)
        {
            Addresses = addresses.ToList();
        }
        public RelayAddresses()
        {

        }

        public readonly List<RelayAddress> Addresses = new List<RelayAddress>();
        public void Add(RelayAddress address) => Addresses.Add(address);
        public void Remove(Predicate<RelayAddress> filter) => Addresses.RemoveAll(filter);
    }

    public record RelayAddress
    {
        public string Address { get; set; }
        public int Port { get; set; }
    }
}
