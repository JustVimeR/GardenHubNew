using System;
using System.Collections.Generic;

namespace Models.DTOs.GetDTOs;

public class GetProjectDTO
{
    public long Id;

    public string Title { get; set; }
    public string Location { get; set; }
    public decimal Budget { get; set; }
    public string Description { get; set; }

    public int NumberOfRequriedGardeners { get; set; }

    public bool IsCompleted { get; set; }
    public bool IsVerified { get; set; }

    public DateTime PublicationDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public List<GetWorkTypeDTO> WorkTypes { get; set; }
    public List<GetMediaDTO> Medias { get; set; }
}

