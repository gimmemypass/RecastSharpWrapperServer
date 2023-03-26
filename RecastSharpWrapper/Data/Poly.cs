using System.Runtime.InteropServices;

namespace Server.Recast.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Poly
    {
        /// Index to first link in linked list. (Or #DT_NULL_LINK if there is no link.)
        public uint firstLink;

        /// The indices of the polygon's vertices.
        /// The actual vertices are located in dtMeshTile::verts.
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public ushort[] verts;

        /// Packed data representing neighbor polygons references and flags for each edge.
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public ushort[] neis;

        /// The user defined polygon flags.
        public ushort flags;

        /// The number of vertices in the polygon.
        public byte vertCount;
    }
}