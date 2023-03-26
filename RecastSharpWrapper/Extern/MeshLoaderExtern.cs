using System.Runtime.InteropServices;

namespace Server.Recast.Extern
{
    public static class MeshLoaderExtern
    {
        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getInputGeometryFromObj(IntPtr shell, IntPtr meshLoader, string filePath, int filePathLen);

        [DllImport("Recast", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr allocMeshLoader(IntPtr shell);
    }
}