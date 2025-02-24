﻿using System.Text.Json.Serialization;

namespace Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RecordStatus
{
    Active,
    Deleted,
    Archived,
    Forgotten
}
