using Models.Enums;

namespace Models.DbEntities;

public interface IMedia
{
    string Url { get; set; }

    MediaType Type { get; set; }
}
