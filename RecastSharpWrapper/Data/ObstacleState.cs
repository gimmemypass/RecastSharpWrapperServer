namespace Server.Recast.Data
{
    [Flags]
    public enum ObstacleState : byte
    {
        DT_OBSTACLE_EMPTY,
        DT_OBSTACLE_PROCESSING,
        DT_OBSTACLE_PROCESSED,
        DT_OBSTACLE_REMOVING,
    }
}