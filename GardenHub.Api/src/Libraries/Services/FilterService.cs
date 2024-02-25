using AutoMapper;
using Core.Constants;
using Models;
using Models.DbEntities;
using System;
using System.Reflection;

namespace Services;

public class FilterService
{
    private readonly IMapper _mapper;

    public FilterService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public ServiceResult<(PaginationFilter PaginationFilter, SortFilter SortFilter)> GetFilters<T>(
        PaginationQuery paginationQuery, SortQuery sortQuery)
    {
        ServiceResult<(PaginationFilter PaginationFilter, SortFilter SortFilter)> serviceResult = new();

        PaginationFilter paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);
        SortFilter sortFilter = GetSortFilter<T>(sortQuery);

        if (sortFilter.PropertyInfo == null)
        {
            serviceResult.Successful = false;
            serviceResult.Message = string.Format(ErrorMessages.InvalidColumnForSorting,
                sortFilter.SortBy);

            return serviceResult;
        }

        serviceResult.Data = (paginationFilter, sortFilter);

        return serviceResult;
    }

    private SortFilter GetSortFilter<T>(SortQuery sortQuery)
    {
        SortFilter sortFilter = _mapper.Map<SortFilter>(sortQuery);

        if (string.IsNullOrEmpty(sortFilter.SortBy))
        {
            switch (typeof(T))
            {
                case Type order when order == typeof(Project):
                    sortFilter.SortBy = nameof(Project.CreatedAt);
                    sortFilter.Descending = true;
                    break;

                default:
                    sortFilter.SortBy = nameof(IEntityBase.Id);
                    break;
            }
        }

        sortFilter.PropertyInfo = typeof(T).GetProperty(sortFilter.SortBy,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        return sortFilter;
    }
}
