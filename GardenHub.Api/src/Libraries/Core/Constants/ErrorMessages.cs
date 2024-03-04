namespace Core.Constants;

public class ErrorMessages
{
    public const string InvalidColumnForSorting = "Invalid sorting column: '{0}'." +
        " Please provide a valid column for sorting.";

    public const string UpdateEntityWithNoId = "Couldn't update entity of type {0} since it's Id was not" +
        " provided.";

    public const string CouldNotUpdateNotOwnedEntity = "Couldn't update {0} with id {1} since" +
        " it's owned by another user.";

    public const string CouldNotDeleteNotOwnedEntity = "Couldn't delete {0} with id {1} since" +
        " it's owned by another user.";

    public const string DerivedWorkTypeCantBeParent = "Derived work type can't be parent.";
}
