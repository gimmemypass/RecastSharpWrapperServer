namespace Server.Recast.Data
{
    [System.Serializable]
    public struct PolyMeshDetailData
    {
        public uint[] meshes;
        public float[] verts;
        public byte[] tris;
        public int nmeshes;
        public int nverts;
        public int ntris;
    }
}