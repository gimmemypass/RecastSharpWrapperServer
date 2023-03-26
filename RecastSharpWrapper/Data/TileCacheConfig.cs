using System.Runtime.InteropServices;

namespace Server.Recast.Data
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TileCacheConfig
    {
        public Config config;
        public TileCacheSetHeader header;
        public IntPtr tileHeaders;
        public IntPtr tilesData;
    }
}