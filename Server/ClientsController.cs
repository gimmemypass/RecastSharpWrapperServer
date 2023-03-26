using System.Numerics;
using System.Runtime.InteropServices;
using LiteNetLib;
using LiteNetLib.Utils;
using RecastSharpWrapper;
using Server.Recast.Data;
using Server.Recast.Extern;
using Server.Shared.Commands;
using Shared;

namespace Server
{
    public class ClientsController
    {
        private readonly ConnectionsHolder connectionsHolder;
        private readonly NetPacketProcessor netPacketProcessor;

        public ClientsController(ConnectionsHolder connectionsHolder, NetPacketProcessor netPacketProcessor)
        {
            this.connectionsHolder = connectionsHolder;
            this.netPacketProcessor = netPacketProcessor;

            connectionsHolder.NewClientRegistered += OnNewClientRegistered;
            connectionsHolder.ClientDisconnected += OnClientDisconnected;
        }

        private void OnClientDisconnected(NetPeer peer, ClientData clientData)
        {
            RemoveAgent(clientData);
        }

        private void OnNewClientRegistered(NetPeer netPeer, ClientData clientData)
        {
            var shellPtr =
                RecastDetourExtern.allocRecastShell(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar +
                                                    "Recast.log");
            var shell = new HandleRef(this, shellPtr);
            clientData.CrowdData = new DetourCrowdData();
            var crowdData = clientData.CrowdData;
            crowdData.Shell = shell;

            var locationSize = new Vector2(20, 20);
            var tileCacheConfigHandle =
                TileCacheUtils.GenerateTileCacheForPlane(shell.Handle, locationSize, Vector3.Zero);
            crowdData.TileCacheConfig = tileCacheConfigHandle;

            clientData.RecastConfig = new RecastConfig
            {
                Layers = new List<RecastConfig.RecastLayer>
                {
                    new() { Cost = 1, LayerID = "WALKABLE" }
                },
                Filters = new List<RecastConfig.Filter>
                {
                    new() { Include = new List<string> { "WALKABLE" } }
                }
            };

            var tileCacheConfig = (TileCacheConfig)tileCacheConfigHandle.Target!;
            crowdData.TileCache = new TileCache(crowdData.Shell.Handle, tileCacheConfig,
                clientData.RecastConfig);
            IntPtr h = DetourCrowdExtern.createCrowd(crowdData.Shell.Handle, crowdData.MaxAgents,
                crowdData.AgentMaxRadius, crowdData.TileCache.NavMeshHandle.Handle);
            crowdData.Crowd = new HandleRef(this, h);

            ushort k = 0;
            var filters = clientData.RecastConfig.Filters.ToList();
            filters.Reverse();
            foreach (var filter in filters)
            {
                ushort include = 0;
                ushort exclude = 0;

                foreach (var incl in filter.Include)
                {
                    include |= clientData.RecastConfig.Areas[incl];
                }

                foreach (var excl in filter.Exclude)
                {
                    exclude |= clientData.RecastConfig.Areas[excl];
                }

                DetourCrowdExtern.setFilter(crowdData.Shell.Handle, crowdData.Crowd.Handle, k, include, exclude);
                ++k;
            }

            crowdData.RandomSample = new float[3];
            crowdData.Positions = new float[crowdData.MaxAgents * 3];
            crowdData.Velocities = new float[crowdData.MaxAgents * 3];
            crowdData.TargetStates = new byte[crowdData.MaxAgents];
            crowdData.States = new byte[crowdData.MaxAgents];
            crowdData.Partial = new bool[crowdData.MaxAgents];
            netPacketProcessor.Send(netPeer,
                new PrepareLocationNetworkCommand { LocationScale = new Vector2Serialize(locationSize)},
                DeliveryMethod.ReliableOrdered);
            
            //location is ready
            //add agent
            clientData.CharacterEntity = new Character();
            AddAgent(clientData);
            netPacketProcessor.Send(netPeer, new SpawnCharacterNetworkCommand{Position = new Vector3Serialize(clientData.CharacterEntity.Position)}, DeliveryMethod.ReliableOrdered);
        }

        public int AddAgent(ClientData clientData)
        {
            clientData.CharacterEntity.DetourAgentData = new DetourAgentData();
            var agent = clientData.CharacterEntity.DetourAgentData;
            var ap = new CrowdAgentParams
            {
                radius = agent.Radius,
                height = agent.Height,
                maxAcceleration = agent.MaxAcceleration,
                maxSpeed = agent.MaxSpeed,
                collisionQueryRange = agent.Radius * 12.0f,
                pathOptimizationRange = agent.Radius * 30.0f,
                updateFlags = agent.Flags,
                obstacleAvoidanceType = (byte)agent.AvoidanceType,
                separationWeight = agent.SeparationWeight,
                queryFilterType = (byte)agent.FilterIndex
            };
            var crowdData = clientData.CrowdData;

            int idx = DetourCrowdExtern.addAgent(crowdData.Shell.Handle, crowdData.Crowd.Handle, clientData.CharacterEntity.Position.ToFloat(), ref ap);
            agent.Idx = idx;
            return idx;
        }

        public void RemoveAgent(ClientData clientData)
        {
            var agent = clientData.CharacterEntity.DetourAgentData;
            var crowdData = clientData.CrowdData;
            DetourCrowdExtern.removeAgent(crowdData.Shell.Handle, crowdData.Crowd.Handle, agent.Idx);
        }

        ~ClientsController()
        {
            connectionsHolder.NewClientRegistered -= OnNewClientRegistered;
            connectionsHolder.ClientDisconnected -= OnClientDisconnected;
        }
    }
}