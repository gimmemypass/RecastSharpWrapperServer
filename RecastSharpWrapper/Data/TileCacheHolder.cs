using System.Runtime.InteropServices;

namespace Server.Recast.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TileCacheHolder
    {
        public Config cfg;
        public ExtendedConfig ecfg;

        public IntPtr allocator;
        public IntPtr compressor;
        public IntPtr processor;

        public InputGeometry geom;
        public IntPtr chunkyMesh;

        public IntPtr navMesh;
        public IntPtr navQuery;
    }
}