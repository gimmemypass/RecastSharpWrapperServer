using System.Numerics;
using System.Runtime.InteropServices;
using Server.Recast.Data;
using Server.Recast.Extern;

namespace RecastSharpWrapper;

public static class TileCacheUtils
{
    public static GCHandle GenerateTileCacheForPlane(IntPtr shell, Vector2 size, Vector3 pos)
    {
        var vertices = GetPlaneVertices(size.X, size.Y);
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] += pos;
        }
        var tris = GetPlaneTriangles();
        return GenerateTileCache(shell, vertices, tris);
    }

    private static GCHandle GenerateTileCache(IntPtr shell, Vector3[] vectorVertices, int[] tris)
    {
        IntPtr ptr = RecastDetourExtern.DefaultConfig();
        Config config = (Config)Marshal.PtrToStructure(ptr, typeof(Config))!;
        config.cs = 0.1f;

        var extendedConfig = new ExtendedConfig
        {
            AgentHeight = config.walkableHeight * config.ch,
            AgentMaxClimb = config.walkableClimb * config.ch,
            AgentRadius = config.walkableRadius * config.ch,
            MaxObstacles = 60
        };

        var recastConfig = new RecastConfig
        {
            Layers = new List<RecastConfig.RecastLayer>
                { new() { LayerID = "WALKABLE", Cost = 1 } },
            Filters = new List<RecastConfig.Filter>
                { new() { Include = new List<string> { "WALKABLE" } } }
        };

        float[] vertices = new float[vectorVertices.Length * 3];
        for (int i = 0; i < vectorVertices.Length; i++)
        {
            vertices[3 * i] = vectorVertices[i].X;
            vertices[3 * i + 1] = vectorVertices[i].Y;
            vertices[3 * i + 2] = vectorVertices[i].Z;
        }

        var tileCacheConfig = RecastUtils.BakeTileCache(shell, config, extendedConfig, vertices, vectorVertices.Length * 3,
            tris, tris.Length / 3, recastConfig);
        return tileCacheConfig;
    }

    private static Vector3[] GetPlaneVertices(float x, float z)
    {
        return new Vector3[]
        {
            new Vector3(0, 0, 0) + new Vector3(-x / 2f, 0, -z / 2f),
            new Vector3(0, 0, z) + new Vector3(-x / 2f, 0, -z / 2f),
            new Vector3(x, 0, z) + new Vector3(-x / 2f, 0, -z / 2f),
            new Vector3(x, 0, 0) + new Vector3(-x / 2f, 0, -z / 2f),
        };
    }

    private static int[] GetPlaneTriangles()
    {
        return new[]
        {
            0, 1, 2,
            0, 2, 3
        };
    }
    private static Vector3[] GetCubeVertices(float x, float y, float z)
    {
        Vector3 vertice0 = new Vector3 (-x * .5f, -y * .5f, z * .5f);
        Vector3 vertice1 = new Vector3 (x * .5f, -y * .5f, z * .5f);
        Vector3 vertice2 = new Vector3 (x * .5f, -y * .5f, -z * .5f);
        Vector3 vertice3 = new Vector3 (-x * .5f, -y * .5f, -z * .5f);    
        Vector3 vertice4 = new Vector3 (-x * .5f, y * .5f, z * .5f);
        Vector3 vertice5 = new Vector3 (x * .5f, y * .5f, z * .5f);
        Vector3 vertice6 = new Vector3 (x * .5f, y * .5f, -z * .5f);
        Vector3 vertice7 = new Vector3 (-x * .5f, y * .5f, -z * .5f);
        Vector3[] vertices = {
// Bottom Polygon
            vertice0, vertice1, vertice2, vertice0,
// Left Polygon
            vertice7, vertice4, vertice0, vertice3,
// Front Polygon
            vertice4, vertice5, vertice1, vertice0,
// Back Polygon
            vertice6, vertice7, vertice3, vertice2,
// Right Polygon
            vertice5, vertice6, vertice2, vertice1,
// Top Polygon
            vertice7, vertice6, vertice5, vertice4
        } ;
        return vertices;
    }

    private static int[] GetCubeTriangles ()
    {
        int[] triangles = new int[]
        {
// Cube Bottom Side Triangles
            3, 1, 0,
            3, 2, 1,    
// Cube Left Side Triangles
            3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
            3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
// Cube Front Side Triangles
            3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
            3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
// Cube Back Side Triangles
            3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
            3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
// Cube Rigth Side Triangles
            3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
            3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
// Cube Top Side Triangles
            3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
            3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,
        } ; 
        return triangles;
    } 
}