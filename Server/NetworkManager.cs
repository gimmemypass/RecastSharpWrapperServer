using LiteNetLib;
using LiteNetLib.Utils;
using Server.Debugger;

namespace Server
{
    public class NetworkManager
    {
        private const int LocalPort = 4242;
        private const string ServerKey = "tellmewhy";
    
        private NetManager netManager;
        private EventBasedNetListener listener;

        private readonly ConnectionsHolder connectionsHolder;
        private readonly NetPacketProcessor packetProcessor;

        public NetworkManager(ConnectionsHolder connectionsHolder, NetPacketProcessor packetProcessor)
        {
            this.connectionsHolder = connectionsHolder;
            this.packetProcessor = packetProcessor;
        }

        public void Start()
        {
            listener = new EventBasedNetListener();
            netManager = new NetManager(listener);
            netManager.Start(LocalPort);
            RecastDebug.Log($"Server started, port : {LocalPort}");
            listener.ConnectionRequestEvent += ListenerOnConnectionRequestEvent;
            listener.PeerConnectedEvent += ListenerOnPeerConnectedEvent;
            listener.PeerDisconnectedEvent += ListenerOnPeerDisconnectedEvent;
            listener.NetworkReceiveEvent += ListenerOnNetworkReceiveEvent;
        }

        public void Update()
        {
            netManager.PollEvents();
        }

        private void ListenerOnNetworkReceiveEvent(NetPeer peer, NetPacketReader reader, DeliveryMethod deliverymethod)
        {
            packetProcessor.ReadAllPackets(reader, peer);
        }

        private void ListenerOnConnectionRequestEvent(ConnectionRequest request)
        {
            RecastDebug.Log($"Connection request from {request.RemoteEndPoint.Address} : {request.RemoteEndPoint.Port}");
            var netPeer = request.AcceptIfKey(ServerKey);
            if (netPeer != null)
            {
                RecastDebug.Log("Connection request accepted");
            }
            else
                RecastDebug.Log("Connection request rejected");
        }

        private void ListenerOnPeerConnectedEvent(NetPeer peer)
        {
            RecastDebug.Log($"{peer.EndPoint} connected to server");
            connectionsHolder.Register(peer, new ClientData());
        }

        private void ListenerOnPeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectinfo)
        {
            RecastDebug.Log($"{peer.EndPoint} disconnected from server");
            connectionsHolder.Unregister(peer);
        }
    }
}