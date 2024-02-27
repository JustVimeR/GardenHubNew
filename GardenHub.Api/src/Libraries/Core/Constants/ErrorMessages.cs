﻿namespace Core.Constants;

public class ErrorMessages
{
    public const string InvalidColumnForSorting = "Invalid sorting column: '{0}'." +
        " Please provide a valid column for sorting.";

    public const string UpdateEntityWithNoId = "Couldn't update entity of type {0} since it's Id was not" +
        " provided.";
}
