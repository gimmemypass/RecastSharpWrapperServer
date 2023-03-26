namespace Server.Recast.Data
{
    [System.Serializable]
    public struct PolyMeshData
    {
        public ushort[] verts;
        public ushort[] polys;
        public ushort[] regs;
        public ushort[] flags;
        public byte[] areas;
        public int nverts;
        public int npolys;
        public int maxpolys;
        public int nvp;
        public float[] bmin;
        public float[] bmax;
        public float cs;
        public float ch;
        public int borderSize;
    }
}