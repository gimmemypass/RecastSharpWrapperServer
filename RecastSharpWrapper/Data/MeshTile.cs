using System.Runtime.InteropServices;

namespace Server.Recast.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MeshTile
    {
        public uint salt;

        public uint linksFreeList;
        public IntPtr header;
        public IntPtr polys;
        public IntPtr verts;
        public IntPtr links;
        public IntPtr detailMeshes;

        public IntPtr detailVerts;
        public IntPtr detailTris;

        public IntPtr bvTree;
        public IntPtr offMeshCons;

        public IntPtr data;
        public int dataSize;
        public int flags;
        public IntPtr next;
    }
}