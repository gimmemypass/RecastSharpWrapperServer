using System.Runtime.InteropServices;

namespace Server.Recast.Data
{
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct TileCacheSetHeader
    {
        public int magic;
        public int version;
        public int numTiles;

        public dtNavMeshParams meshParams;
        public dtTileCacheParams cacheParams;
    }
}