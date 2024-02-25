namespace Models.DTOs.GetDTOs;

public class GetMediaDTO
{
    public long Id;

    public required string Url { get; set; }

    public required string Type { get; set; }
}
