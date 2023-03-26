using System.Collections.Concurrent;
using System.Diagnostics;
using LiteNetLib;

namespace Server
{
    public class ConnectionsHolder
    {
        private ConcurrentDictionary<NetPeer, ClientData> peerToClients = new();
        public event Action<NetPeer, ClientData> NewClientRegistered;
        public event Action<NetPeer, ClientData> ClientDisconnected;

        public bool Register(NetPeer netPeer, ClientData clientData)
        {
            var result = peerToClients.TryAdd(netPeer, clientData);
            if(result)
                NewClientRegistered?.Invoke(netPeer, clientData);
            return result;
        }

        public bool Unregister(NetPeer netPeer)
        {
            var result = peerToClients.TryRemove(netPeer, out var clientData);
            if(result)
                ClientDisconnected?.Invoke(netPeer, clientData);
            return result;
        }

        public bool TryGetClientByPeer(NetPeer netPeer, out ClientData clientData)
        {
            return peerToClients.TryGetValue(netPeer, out clientData);
        }

        public IEnumerator<ClientData> GetClients()
        {
            foreach (var connection in peerToClients)
            {
                yield return connection.Value;
            }
        }
    }
}