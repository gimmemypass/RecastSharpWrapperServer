using System.Runtime.InteropServices;

namespace Server.Recast.Data
{
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct TileCacheTileHeader
    {
        public uint tileRef;
        public int dataSize;
    };
}