namespace Models.DTOs.GetDTOs;

public class GetGardenerMiniProfile
{
    public long Id { get; set; }

    public GetMediaDTO? Icon { get; set; }

    public string? Name { get; set; }

    public string? UserName { get; set; }

    public float? AvgRating { get; set; }
}

