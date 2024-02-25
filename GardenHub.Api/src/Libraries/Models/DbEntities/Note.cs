namespace Models.DbEntities
{
    public class Note : EntityBase
    {
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string OwnerEmail { get; set; } = string.Empty;
    }
}
