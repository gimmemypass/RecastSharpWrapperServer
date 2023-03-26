using System.Numerics;
using System.Runtime.InteropServices;
using Server.Recast.Data;
using Server.Recast.Extern;

namespace RecastSharpWrapper
{
    public sealed class TileCache : IDisposable
    {
        private readonly IntPtr shell;

        #region Private Attributes
        private IntPtr _tileCache = new(0);
        private IntPtr _navMesh = new(0);
        private IntPtr _navQuery = new IntPtr(0);
        #endregion
        
        #region Handles
        private HandleRef _tileCacheHandle;
        private HandleRef _navMeshHandle;
        private HandleRef _navQueryHandle;
        #endregion

        public TileCache(IntPtr shell, TileCacheConfig tileCacheConfig, RecastConfig recastConfig)
        {
            this.shell = shell;
            recastConfig.SetupAreas(shell);
            bool result = RecastDetourExtern.loadFromTileCacheHeaders(shell, ref tileCacheConfig.header, tileCacheConfig.tileHeaders,
                tileCacheConfig.tilesData, ref _tileCache, ref _navMesh, ref _navQuery);

            if (!result)
                throw new ArgumentException("Invalid navmesh data");

            _tileCacheHandle = new HandleRef(this, _tileCache);
            _navMeshHandle = new HandleRef(this, _navMesh);
            _navQueryHandle = new HandleRef(this, _navQuery);
        }
        
        // Provide access to 3rd party code
        public HandleRef TileCacheHandle => _tileCacheHandle;

        public HandleRef NavMeshHandle => _navMeshHandle;

        public HandleRef NavQueryHandle => _navQueryHandle;

        public uint AddObstacle(DetourObstacleData obstacleData, Vector3 pos, float rotation)
        {
            var bottom1 = obstacleData.BottomCenter;
            var obstacleSize = obstacleData.Size;
            obstacleSize.X *= obstacleData.DetourObstacleSizeScale.X;
            obstacleSize.Y *= obstacleData.DetourObstacleSizeScale.Y;
            obstacleSize.Z *= obstacleData.DetourObstacleSizeScale.Z;
            
            bottom1.X -= obstacleSize.X / 2;
            bottom1.Z += obstacleSize.Z / 2;

            var bottom2 = obstacleData.BottomCenter;
            bottom2.X += obstacleSize.X / 2;
            bottom2.Z += obstacleSize.Z / 2;

            var bottom3 = obstacleData.BottomCenter;
            bottom3.X += obstacleSize.X / 2;
            bottom3.Z -= obstacleSize.Z / 2;

            var bottom4 = obstacleData.BottomCenter;
            bottom4.X -= obstacleSize.X / 2;
            bottom4.Z -= obstacleSize.Z / 2;

            bottom1 = Rotate2D(bottom1, -rotation);
            bottom2 = Rotate2D(bottom2, -rotation);
            bottom3 = Rotate2D(bottom3, -rotation);
            bottom4 = Rotate2D(bottom4, -rotation);

            bottom1 += pos;
            bottom2 += pos;
            bottom3 += pos;
            bottom4 += pos;
             
            float[] vertices =
            {
                bottom1.X, bottom1.Y, bottom1.Z,
                bottom2.X, bottom2.Y, bottom2.Z,
                bottom3.X, bottom3.Y, bottom3.Z,
                bottom4.X, bottom4.Y, bottom4.Z
            };
            float[] position = { 0f, 0f, 0f };
            int result = 0;
            var obstacleRef = RecastDetourExtern.addObstacle(shell, _tileCacheHandle.Handle, position, vertices, 4,
                obstacleSize.Y,
                ref result
                );
            return obstacleRef;
        }

        public ObstacleState GetObstacleState(uint obstacleRef)
        {
            return (ObstacleState)RecastDetourExtern.getObstacleState(shell, _tileCacheHandle.Handle, obstacleRef);
        }

        public void RemoveObstacle(uint obstacleRef)
        {
            RecastDetourExtern.removeObstacle(shell, _tileCacheHandle.Handle, obstacleRef);
        }
        private Vector3 Rotate2D(Vector3 v, float degrees)
        {
            const float deg2Rad = 0.01745329f;
            
            float sin = MathF.Sin(degrees * deg2Rad);
            float cos = MathF.Cos(degrees * deg2Rad);
             
            float tx = v.X;
            float tz = v.Z;
            v.X = cos * tx - sin * tz;
            v.Z = sin * tx + cos * tz;
            return v;
        }


        public void Dispose()
        {
            Cleanup();
            GC.SuppressFinalize(this);
        }

        private void Cleanup()
        {
            _tileCacheHandle = new HandleRef(this, IntPtr.Zero);
            _navMeshHandle = new HandleRef(this, IntPtr.Zero);
            _navQueryHandle = new HandleRef(this, IntPtr.Zero);
        }


    }
}