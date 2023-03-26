using System.Runtime.InteropServices;

namespace Server.Recast.Data
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Config
    {
        public int width;
        public int height;
        public int tileSize;
        public int borderSize;
        public float cs;
        public float ch;
        public fixed float bmin[3];
        public fixed float bmax[3];
        public float walkableSlopeAngle;
        public int walkableHeight;
        public int walkableClimb;
        public int walkableRadius;
        public int maxEdgeLen;
        public float maxSimplificationError;
        public int minRegionArea;
        public int mergeRegionArea;
        public int maxVertsPerPoly;
        public float detailSampleDist;
        public float detailSampleMaxError;
    };
}