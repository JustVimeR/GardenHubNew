using Models.Enums;

namespace Models.DbEntities;

public class Media : EntityBase, IMedia
{
    public required string Url { get; set; }

    public required MediaType Type { get; set; }
}
