using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reflection.Metadata;
using static Networking.Config;

namespace Networking
{
    
    public class NetworkAddress
    {
        private string Hostname;
        private int AddressLength;
        private bool Resolved;
        private SocketAddress Address;
        /*
        public struct NetworkAddressStorage(SocketAddress address, int addressLength)
        {
            AddressLength = addressLength;
            Address = address;
            Resolved = addressLength != 0;
            Hostname = "\0";
        }
        */
        
    }
}