namespace Server.Recast.Data
{
    public struct CompressedTile
    {
        public uint salt;                      // Counter describing modifications to the tile.
        public IntPtr header;
        public IntPtr compressed;
        public int compressedSize;
        public IntPtr data;
        public int dataSize;
        public uint flags;
        public IntPtr next;
    };
}