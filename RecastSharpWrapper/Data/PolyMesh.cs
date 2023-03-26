using System.Runtime.InteropServices;

namespace Server.Recast.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PolyMesh
    {
        public IntPtr verts;  // The mesh vertices. [Form: (x, y, z) * #nverts]
        public IntPtr polys;  // Polygon and neighbor data. [Length: #maxpolys * 2 * #nvp]
        public IntPtr regs;   // The region id assigned to each polygon. [Length: #maxpolys]
        public IntPtr flags;  // The user defined flags for each polygon. [Length: #maxpolys]
        public IntPtr areas;   // The area id assigned to each polygon. [Length: #maxpolys]
        public int nverts;             // The number of vertices.
        public int npolys;             // The number of polygons.
        public int maxpolys;           // The number of allocated polygons.
        public int nvp;                // The maximum number of vertices per polygon.
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.R4, SizeConst = 3)]
        public float[] bmin;          // The minimum bounds in world space. [(x, y, z)]
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.R4, SizeConst = 3)]
        public float[] bmax;          // The maximum bounds in world space. [(x, y, z)]
        public float cs;               // The size of each cell. (On the xz-plane.)
        public float ch;               // The height of each cell. (The minimum increment along the y-axis.)
        public int borderSize;          // The AABB border size used to generate the source data from which the mesh was derived.
    }
}