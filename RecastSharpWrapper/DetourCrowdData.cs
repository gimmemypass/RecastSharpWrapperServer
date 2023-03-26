using System.Runtime.InteropServices;

namespace RecastSharpWrapper;

    public sealed class DetourCrowdData
    {
        public GCHandle TileCacheConfig;

        public int MaxAgents = 1024;
        public float AgentMaxRadius = 2;
        
        // Use a HandleRef to avoid race conditions;
        // see the GC-Safe P/Invoke Code section
        public HandleRef Shell;
        public HandleRef Crowd;
        public TileCache TileCache;
        
#region Internal Library Memory Wrappers
        public float[] RandomSample;
        public float[] Positions;
        public float[] Velocities;
        public byte[] TargetStates;
        public byte[] States;
        public bool[] Partial;
#endregion
    }
