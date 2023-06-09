﻿namespace Server.Recast.Data
{
    [Flags]
    public enum DetourUpdateFlags : byte
    {
        DT_CROWD_ANTICIPATE_TURNS = 1,
        DT_CROWD_OBSTACLE_AVOIDANCE = 2,
        DT_CROWD_SEPARATION = 4,
        DT_CROWD_OPTIMIZE_VIS = 8,			// Use #dtPathCorridor::optimizePathVisibility() to optimize the agent path.
        DT_CROWD_OPTIMIZE_TOPO = 16,		// Use dtPathCorridor::optimizePathTopology() to optimize the agent path.
    };
}