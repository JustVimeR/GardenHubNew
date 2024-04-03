using AutoMapper;

namespace Core.Extensions;

public static class MappingExtensions
{
    public static void SelectiveMap<T, TAssertion>(this IMapper mapper, T src, T dest)
    {
        TAssertion assertion = mapper.Map<TAssertion>(src);

        mapper.Map(assertion, dest);
    }
}
