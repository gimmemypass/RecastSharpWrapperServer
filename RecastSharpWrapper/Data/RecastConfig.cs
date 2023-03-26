using Server.Recast.Extern;

namespace Server.Recast.Data
{
    [Serializable]
    public class RecastConfig
    {
        public List<RecastLayer> Layers = new() { new RecastLayer { LayerID = "WALKABLE", Cost = 1 } };

        public List<Filter> Filters = new() { new Filter() };

        public Dictionary<string, ushort> Areas = new();

        public void SetupAreas(IntPtr shell)
        {
            ushort n = 1;
            foreach (var layer in Layers)
            {
                Areas.Add(layer.LayerID, n);
                RecastDetourExtern.addFlag(shell, n, layer.Cost);
                n *= 2;
            }
        } 
        
        [Serializable]
        public class RecastLayer
        {
            public string LayerID;
            public ushort Cost;
        }

        [Serializable]
        public class Filter
        {
            public List<string> Include = new();
            public List<string> Exclude = new();
        }
    }
}