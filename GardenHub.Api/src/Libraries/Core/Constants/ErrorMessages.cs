namespace Core.Constants;

public class ErrorMessages
{
    public const string InvalidEmailSettings = "Test email wasn't sent, update email creadentioals";

    public const string InvalidColumnForSorting = "Invalid sorting column: '{0}'." +
        " Please provide a valid column for sorting.";

    public const string UpdateEntityWithNoId = "Couldn't update entity of type {0} since it's Id was not" +
        " provided.";

    public const string CouldNotUpdateNotOwnedEntity = "Couldn't update {0} with id {1} since" +
        " it's owned by another user.";

    public const string CouldNotDeleteNotOwnedEntity = "Couldn't delete {0} with id {1} since" +
        " it's owned by another user.";

    public const string CouldNotReferenceNotOwnedEntity = "Couldn't reference {0} with id {1} since" +
         " it's owned by another user.";

    public const string GardenerNotRelatedToProject = "Gardener with id {0} is not related" +
        " to project with id {1}.";

    public const string DerivedWorkTypeCantBeParent = "Derived work type can't be parent.";

    public const string EntityAlreadyExists = "Entity '{0}' already exists.";
}
