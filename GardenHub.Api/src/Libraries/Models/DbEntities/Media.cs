using System.ComponentModel.DataAnnotations;

namespace Models.DbEntities
{
    public class Media: EntityBase
    {
        public string Url { get; set; }

        public string Type { get; set; }

        public Project Project{ get; set; }
    }
}