using System.Collections.Generic;

namespace Models.DbEntities
{
    public class City :EntityBase
    {
        public string Name { get; set; }
        public List<GardenerProfile> Gardeners { get; set; }
    }
}