using System.Runtime.InteropServices;

namespace Server.Recast.Data
{
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct dtTileCacheParams
    {
        public fixed float orig[3];
        public float cs, ch;
        public int width, height;
        public float walkableHeight;
        public float walkableRadius;
        public float walkableClimb;
        public float maxSimplificationError;
        public int maxTiles;
        public int maxObstacles;
    };
    
}