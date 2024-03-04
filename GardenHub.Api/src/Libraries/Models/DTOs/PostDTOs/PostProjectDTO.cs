using System;
using System.Collections.Generic;
namespace Models.DTOs.PostDTOs;

public class PostProjectDTO
{
    public required string Title { get; set; }
    public string? Location { get; set; }
    public decimal Budget { get; set; }
    public string? Description { get; set; }

    public int NumberOfRequriedGardeners { get; set; }

    public bool IsCompleted { get; set; } = false;

    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }


    public List<PostIdDTO>? WorkTypes { get; set; }
    //public List<PostMediaDTO>? Medias { get; set; }
}
