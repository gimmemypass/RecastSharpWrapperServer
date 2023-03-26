using System.Runtime.InteropServices;
using Server.Recast.Data;

namespace Server.Recast.Extern
{
    public static class DetourCrowdExtern
    {
        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr createCrowd(IntPtr shell, int maxAgents, float maxRadius, IntPtr navmesh);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern void setFilter(IntPtr shell,IntPtr crowd, int filter, ushort include, ushort exclude);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern int addAgent(IntPtr shell,IntPtr crowd, float[] p, ref CrowdAgentParams ap);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getAgent(IntPtr shell,IntPtr crowd, int idx);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern void updateAgent(IntPtr shell,IntPtr crowd, int idx, ref CrowdAgentParams ap);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern void removeAgent(IntPtr shell,IntPtr crowd, int idx);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern void setMoveTarget(IntPtr shell,IntPtr navquery, IntPtr crowd, int idx, float[] p, bool adjust, int filterIndex);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern void resetPath(IntPtr shell,IntPtr crowd, int idx);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern void updateTick(IntPtr shell,IntPtr tileCache, IntPtr nav, IntPtr crowd, float dt, float[] positions, float[] velocities, byte[] states, byte[] targetStates, bool[] partial, ref int nagents);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool isPointValid(IntPtr shell,IntPtr crowd, float[] targetPoint);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool randomPoint(IntPtr shell,IntPtr crowd, float[] targetPoint);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool randomPointInCircle(IntPtr shell,IntPtr crowd, float[] initialPoint, float maxRadius, float[] targetPoint);
    }
}