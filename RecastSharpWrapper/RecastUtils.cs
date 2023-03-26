using System.Runtime.InteropServices;
using Server.Recast.Data;
using Server.Recast.Extern;

namespace RecastSharpWrapper;


public static class RecastUtils
{

    public static GCHandle BakeTileCache(IntPtr shell, Config config, ExtendedConfig ecfg, float[] verts, int nverts,
        int[] tris, int ntris, RecastConfig recastConfig)
    {
        IntPtr vertsPtr = Marshal.AllocHGlobal(verts.Length * sizeof(float));
        Marshal.Copy(verts, 0, vertsPtr, verts.Length);

        IntPtr trisPtr = Marshal.AllocHGlobal(tris.Length * sizeof(int));
        Marshal.Copy(tris, 0, trisPtr, tris.Length);

        InputGeometry geom = new InputGeometry()
        {
            verts = vertsPtr,
            tris = trisPtr,
            nverts = nverts,
            ntris = ntris
        };
        return BakeTileCache(shell, config, ecfg, recastConfig, geom);
    }

    public static GCHandle BakeTileCache(IntPtr shell, Config config, ExtendedConfig ecfg,
        RecastConfig recastConfig,
        InputGeometry geom)
    {

        ushort k = 1;
        foreach (var layer in recastConfig.Layers)
        {
            RecastDetourExtern.addFlag(shell, k, 1);
            k *= 2;
        }

        IntPtr tileCache = new IntPtr(0);
        IntPtr navMesh = new IntPtr(0);
        IntPtr navQuery = new IntPtr(0);
        RecastDetourExtern.handleTileCacheBuild(shell, ref config, ref ecfg, ref geom, ref tileCache, ref navMesh, ref navQuery);
        
        IntPtr tileHeaderPtr = new IntPtr(0);
        
        
        var tileCacheConfig = new TileCacheConfig();
        
        var tileCacheConfigHandle = GCHandle.Alloc(tileCacheConfig, GCHandleType.Pinned);
        // TileCacheSetHeader header = default;

        RecastDetourExtern.getTileCacheHeaders(shell, ref tileCacheConfig.header, ref tileHeaderPtr, tileCache, navMesh);
        // tileCacheConfig.header = header;
        
        // unsafe
        // {
        //     float[] pointer = header.meshParams.orig;
        //     fixed (float* pointerToFirst = pointer)
        //     {
        //         IntPtr p = new IntPtr(pointerToFirst);
        //         // dtNavMeshParams meshParams = default;
        //         // meshParams.orig = ;
        //         // header.meshParams = meshParams;
        //         
        //         // dtTileCacheParams cacheParams = default;
        //         // cacheParams.orig = p;
        //         // header.cacheParams = cacheParams;
        //     }
        // } 
        
        
        // Copy to asset
        tileCacheConfig.config = config;

        // Copy sizes
        int structSize = Marshal.SizeOf(typeof(TileCacheTileHeader));
        var tileHeaders = new TileCacheTileHeader[tileCacheConfig.header.numTiles];
        for (uint i = 0; i < tileCacheConfig.header.numTiles; ++i)
        {
            tileHeaders[i] =
                (TileCacheTileHeader)Marshal.PtrToStructure(new IntPtr(tileHeaderPtr.ToInt64() + (structSize * i)),
                    typeof(TileCacheTileHeader))!;
        }

        // Copy data
        int dataSize = 0;
        int start = 0;
        for (uint i = 0; i < tileCacheConfig.header.numTiles; ++i)
        {
            dataSize += tileHeaders[i].dataSize;
        }

        var tilesData = new byte[dataSize];

        for (uint i = 0; i < tileCacheConfig.header.numTiles; ++i)
        {
            IntPtr tilePtr = RecastDetourExtern.getTileCacheTile(shell, tileCache, (int)i);
            CompressedTile tile = (CompressedTile)Marshal.PtrToStructure(tilePtr, typeof(CompressedTile))!;

            if (tileHeaders[i].dataSize > 0)
            {
                Marshal.Copy(tile.data, tilesData, start, tileHeaders[i].dataSize);
                start += tileHeaders[i].dataSize;
            }
        }
        tileCacheConfig.tileHeaders = tileHeaderPtr;

        IntPtr tilesDataPtr = Marshal.AllocHGlobal(Marshal.SizeOf(tilesData[0]) * dataSize); 
        Marshal.Copy(tilesData, 0, tilesDataPtr, dataSize);
        tileCacheConfig.tilesData = tilesDataPtr;

        return tileCacheConfigHandle;
    }

    private static void CopyArray(IntPtr from, byte[] dest, int size)
    {
        if (size > 0)
        {
            Marshal.Copy(from, dest, 0, size);
        }
    }

    private static void CopyArray(IntPtr from, ushort[] dest, int size)
    {
        if (size > 0)
        {
            {
                short[] tmp = new short[size];
                Marshal.Copy(from, tmp, 0, size);

                System.Buffer.BlockCopy(tmp, 0, dest, 0, size * sizeof(ushort));
            }
        }
    }

    private static void CopyArray(IntPtr from, uint[] dest, int size)
    {
        if (size > 0)
        {
            {
                int[] tmp = new int[size];
                Marshal.Copy(from, tmp, 0, size);

                Buffer.BlockCopy(tmp, 0, dest, 0, size * sizeof(uint));
            }
        }
    }

    private static void CopyArray(IntPtr from, float[] dest, int size)
    {
        if (size > 0)
        {
            Marshal.Copy(from, dest, 0, size);
        }
    }

    private static void CopyArray<T>(T[] from, ref T[] dest, int size)
    {
        dest = new T[size];
        Buffer.BlockCopy(from, 0, dest, 0, size * Marshal.SizeOf(typeof(T)));
    }
}