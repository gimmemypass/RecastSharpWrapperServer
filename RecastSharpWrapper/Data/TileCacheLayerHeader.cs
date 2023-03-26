using System.Runtime.InteropServices;

namespace Server.Recast.Data
{
    public struct TileCacheLayerHeader
    {
        public int magic;                              // Data magic
        public int version;                            // Data version
        public int tx, ty, tlayer;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] bmin;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] bmax;
        public ushort hmin, hmax;              // Height min/max range
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte width, height;            // Dimension of the layer.
        public byte minx, maxx, miny, maxy;   // Usable sub-region.
    };
}