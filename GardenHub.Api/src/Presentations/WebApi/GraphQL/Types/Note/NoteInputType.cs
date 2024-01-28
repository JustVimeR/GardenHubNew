using GraphQL.Types;

namespace WebApi.GraphQL.Types.Note
{
    public class NoteInputType : InputObjectGraphType
    {
        public NoteInputType()
        {
            Name = "noteInput";
            Field<StringGraphType>("title");
            Field<NonNullGraphType<StringGraphType>>("category");
            Field<NonNullGraphType<StringGraphType>>("description");
        }
    }
}
