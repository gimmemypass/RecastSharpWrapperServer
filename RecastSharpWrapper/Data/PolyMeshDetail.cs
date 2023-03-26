using System.Runtime.InteropServices;

namespace Server.Recast.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PolyMeshDetail
    {
        public IntPtr meshes;
        public IntPtr verts;
        public IntPtr tris;
        public int nmeshes;
        public int nverts;
        public int ntris;
    }
}