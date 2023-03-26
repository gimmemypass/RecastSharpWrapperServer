using Server.Recast.Data;

namespace Server;

[Serializable]
public sealed class DetourAgentData
{
    public float Radius = 0.3f;
    public float Height = 2.0f;
    public float MaxSpeed = 6.0f;
    public float MaxAcceleration = 5.0f;
    public bool IsFlying;
    public float SeparationWeight = 0.5f;
            
    public byte Flags = (byte)(
        0
        | DetourUpdateFlags.DT_CROWD_OBSTACLE_AVOIDANCE 
        | DetourUpdateFlags.DT_CROWD_SEPARATION 
        | DetourUpdateFlags.DT_CROWD_OPTIMIZE_VIS 
        | DetourUpdateFlags.DT_CROWD_OPTIMIZE_TOPO 
        | DetourUpdateFlags.DT_CROWD_ANTICIPATE_TURNS
    );
    // public byte Flags = 0;
    public DetourObstacleAvoidanceType AvoidanceType = DetourObstacleAvoidanceType.HIGH;
    public int FilterIndex = 0;
    public int Idx = -1;
}