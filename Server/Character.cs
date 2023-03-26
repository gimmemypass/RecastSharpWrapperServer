using System.Numerics;

namespace Server
{
    public interface IEntity
    {
        public int ShortId { get; set;}
        public Vector3 Position { get; set; }
        public Vector3 Rotataion { get; set; }
    }

    public class Character : IEntity
    {
        public int ShortId { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotataion { get; set; }

        public DetourAgentData DetourAgentData;
    }
}