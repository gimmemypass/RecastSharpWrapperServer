using System.Runtime.InteropServices;

namespace Server.Recast.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct InputGeometry
    {
        public IntPtr verts;
        public int nverts;
        public IntPtr tris;
        public int ntris;
    };
}