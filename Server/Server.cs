using LiteNetLib;
using LiteNetLib.Utils;
using Server.Debugger;

namespace Server;

public class Server
{
    private NetManager netManager;
    private EventBasedNetListener listener;

    public void Start()
    {
        listener = new EventBasedNetListener();
        netManager = new NetManager(listener);
        var result = netManager.Start(4242);
        Debug.Log("Server started");
        
        listener.ConnectionRequestEvent += ListenerOnConnectionRequestEvent;
        listener.PeerConnectedEvent += ListenerOnPeerConnectedEvent;

    }

    public void Update()
    {
        netManager.PollEvents();
    }

    private void ListenerOnConnectionRequestEvent(ConnectionRequest request)
    {
        Debug.Log($"Connection request from {request.RemoteEndPoint.Address} : {request.RemoteEndPoint.Port}");
        var netPeer = request.Accept();
        if (netPeer != null)
        {
            Debug.Log("Connection request accepted");
        }
        else
            Debug.Log("Connection request rejected");
    }

    private void ListenerOnPeerConnectedEvent(NetPeer peer)
    {
        Debug.Log($"We got connection {peer.EndPoint}");
        NetDataWriter writer = new NetDataWriter();
        writer.Put("Hello client");
        peer.Send(writer, DeliveryMethod.ReliableOrdered);
    }
}