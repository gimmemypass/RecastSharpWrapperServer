using System.Runtime.InteropServices;

namespace Server.Recast.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PolyDetail
    {
        public uint vertBase;          // The offset of the vertices in the dtMeshTile::detailVerts array.
        public uint triBase;           // The offset of the triangles in the dtMeshTile::detailTris array.
        public byte vertCount;        // The number of vertices in the sub-mesh.
        public byte triCount;         // The number of triangles in the sub-mesh.
    };
}