using LiteNetLib.Utils;
using Shared;

namespace Server
{
    public class Server
    {
        private ConnectionsHolder connectionsHolder;
        private NetworkManager networkManager;
        private ClientsController clientsController;
        private NetPacketProcessor netPacketProcessor;

        public void Start()
        {
            connectionsHolder = new ConnectionsHolder();
            netPacketProcessor = new NetPacketProcessor();
            networkManager = new NetworkManager(connectionsHolder, netPacketProcessor);
            networkManager.Start();
            
            clientsController = new ClientsController(connectionsHolder, netPacketProcessor);
            
            netPacketProcessor.RegisterNestedType<Vector2Serialize>();
            netPacketProcessor.RegisterNestedType<Vector3Serialize>();
        }

        public void Update()
        {
            networkManager.Update();
        }
    }
}