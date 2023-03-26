using System.Runtime.InteropServices;

namespace Server.Recast.Data
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct ExtendedConfig
    {
        public float AgentHeight;
        public float AgentRadius;
        public float AgentMaxClimb;
        public int MaxObstacles;
    };
}