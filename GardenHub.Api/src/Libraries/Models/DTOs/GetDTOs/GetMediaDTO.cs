namespace Models.DTOs.GetDTOs;

public class GetMediaDTO
{
    public long Id;

    public string Url { get; set; }

    public string Type { get; set; }

    public GetProjectDTO Project { get; set; }
}
