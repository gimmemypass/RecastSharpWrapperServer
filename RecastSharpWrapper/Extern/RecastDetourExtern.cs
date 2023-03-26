using System.Runtime.InteropServices;
using Server.Recast.Data;

namespace Server.Recast.Extern
{
    public static class RecastDetourExtern
    {
        //Debug
        public delegate void DebugCallback(IntPtr request, int size);
    
        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RegisterDebugCallback(DebugCallback cb);
    
        //Shell
        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr allocRecastShell(string logPath);   
    
        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern int pointerSize();

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr DefaultConfig();
    
        //TileCache
        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool handleTileCacheBuild(IntPtr shell, ref Config cfg, ref ExtendedConfig ecfg, ref InputGeometry geom, ref IntPtr tileCache, ref IntPtr navMesh, ref IntPtr navQuery);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern void getTileCacheHeaders(IntPtr shell,ref TileCacheSetHeader header, ref IntPtr tilesHeader, IntPtr tileCache, IntPtr navMesh);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool loadFromTileCacheHeaders(IntPtr shell,ref TileCacheSetHeader header, IntPtr tilesHeader, IntPtr data, ref IntPtr tileCache, ref IntPtr navMesh, ref IntPtr navQuery);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern void addFlag(IntPtr shell,ushort area, ushort cost);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getTileCacheTile(IntPtr shell,IntPtr tileCache, int i);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint addObstacle(IntPtr shell,IntPtr tileCache, float[] pos, float[] verts, int nverts, float height, ref int result);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern void removeObstacle(IntPtr shell,IntPtr tileCache, uint reference);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte getObstacleState(IntPtr shell,IntPtr tileCache, uint reference);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getObstacles(IntPtr shell,IntPtr tileCache, ref int nobstacles);

    
        // [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        // public static extern void addConvexVolume(IntPtr shell,float[] verts, int nverts, float hmax, float hmin, int area);
        // [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        // public static extern uint addCylinderObstacle(IntPtr shell,IntPtr tileCache, float[] pos, float radius, float height, ref int result);
        // [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        // public static extern uint addAreaFlags(IntPtr shell,IntPtr tileCache, IntPtr crowd, float[] center, float[] verts, int nverts, float height, ushort flags);
        // [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        // public static extern void removeAreaFlags(IntPtr shell,IntPtr tileCache, uint reference);
    }
}