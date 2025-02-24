﻿using System.Collections.Generic;

namespace Models.DTOs.GetDTOs;

public class GetChatDTO
{
    public long Id { get; set; }

    public List<GetChatMessageDTO>? ChatMessages { get; set; }

    public GetUserMiniProfile? InterlocutorProfile { get; set; }
}
