using System.Runtime.InteropServices;

namespace Server.Recast.Data
{
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct dtNavMeshParams
    {
        public fixed float orig[3]; // The world space origin of the navigation mesh's tile space. [(x, y, z)]
        public float tileWidth; // The width of each tile. (Along the x-axis.)
        public float tileHeight; // The height of each tile. (Along the z-axis.)
        public int maxTiles; // The maximum number of tiles the navigation mesh can contain.
        public int maxPolys; // The maximum number of polygons each tile can contain.
    };
}