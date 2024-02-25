namespace Models.DbEntities;

public class ProjectMedia : EntityBase, IMedia
{
    public required string Url { get; set; }

    public required string Type { get; set; }

    public long ProjectId { get; set; }

    public Project? Project { get; set; }
}