using System.Numerics;

namespace RecastSharpWrapper;

[Serializable]
public class DetourObstacleData
{
    public Vector3 BottomCenter;
    public Vector3 Size;
    public Vector3 DetourObstacleSizeScale;
    public int Layer;
}
