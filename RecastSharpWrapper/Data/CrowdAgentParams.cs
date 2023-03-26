using System.Runtime.InteropServices;

namespace Server.Recast.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CrowdAgentParams
    {
        public float radius;						// Agent radius. [Limit: >= 0]
        public float height;						// Agent height. [Limit: > 0]
        public float maxAcceleration;				// Maximum allowed acceleration. [Limit: >= 0]
        public float maxSpeed;						// Maximum allowed speed. [Limit: >= 0]

        /// Defines how close a collision element must be before it is considered for steering behaviors. [Limits: > 0]
        public float collisionQueryRange;

        public float pathOptimizationRange;		// The path visibility optimization range. [Limit: > 0]

        /// How aggresive the agent manager should be at avoiding collisions with this agent. [Limit: >= 0]
        public float separationWeight;

        /// Flags that impact steering behavior. (See: #UpdateFlags)
        public byte updateFlags;

        // The index of the avoidance configuration to use for the agent.
        // [Limits: 0 <= value <= #DT_CROWD_MAX_OBSTAVOIDANCE_PARAMS]
        public byte obstacleAvoidanceType;

        /// The index of the query filter used by this agent.
        public byte queryFilterType;

        /// User defined data attached to the agent.
        public IntPtr userData;
    };
}