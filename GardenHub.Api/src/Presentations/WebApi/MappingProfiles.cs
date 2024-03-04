using AutoMapper;
using Data.IdentityModels;
using Models;
using Models.DbEntities;
using Models.DTOs.Account;
using Models.DTOs.GetDTOs;
using Models.DTOs.PostDTOs;

namespace WebApi;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PaginationQuery, PaginationFilter>();
        CreateMap<SortQuery, SortFilter>();
        CreateMap<SearchQuery, SearchFilter>();

        CreateMap<PostIdDTO, WorkType>();

        CreateMap<GardenerProfile, GardenerProfile>();
        CreateMap<City, City>();
        CreateMap<WorkType, WorkType>();
        CreateMap<Project, Project>()
            .ForMember(d => d.CustomerId, o => o.Ignore());

        CreateMap<PostCityDTO, City>();
        CreateMap<PostCustomerProfileDTO, CustomerProfile>();
        CreateMap<PostFeedbackDTO, Feedback>();
        CreateMap<PostGardenerProfileDTO, GardenerProfile>();
        CreateMap<PostMediaDTO, Media>();
        CreateMap<PostProjectDTO, Project>();
        CreateMap<PostUserProfileDTO, UserProfile>();

        CreateMap<PostWorkTypeDTO, WorkType>();
        CreateMap<PostDerivedWorkTypeDTO, WorkType>();

        CreateMap<City, GetCityDTO>();
        CreateMap<CustomerProfile, GetCustomerProfileDTO>();
        CreateMap<Feedback, GetFeedbackDTO>();
        CreateMap<GardenerProfile, GetGardenerProfileDTO>();
        CreateMap<Media, GetMediaDTO>();
        CreateMap<Project, GetProjectDTO>();
        CreateMap<UserProfile, GetUserProfileDTO>();
        CreateMap<WorkType, GetWorkTypeDTO>();
        CreateMap<WorkType, GetDerivedWorkTypeDTO>();

        CreateMap<ApplicationUser, UserDto>()
            .ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName))
            .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Email));


        CreateMap<IEntityBase, City>().ForMember(d => d.Id, o => o.Ignore());
        CreateMap<IEntityBase, CustomerProfile>().ForMember(d => d.Id, o => o.Ignore());
        CreateMap<IEntityBase, Feedback>().ForMember(d => d.Id, o => o.Ignore());
        CreateMap<IEntityBase, GardenerProfile>().ForMember(d => d.Id, o => o.Ignore());
        CreateMap<IEntityBase, Media>().ForMember(d => d.Id, o => o.Ignore());
        CreateMap<IEntityBase, Project>().ForMember(d => d.Id, o => o.Ignore());
        CreateMap<IEntityBase, UserProfile>().ForMember(d => d.Id, o => o.Ignore());
        CreateMap<IEntityBase, WorkType>().ForMember(d => d.Id, o => o.Ignore());



        CreateMap<ApplicationUser, UserProfile>()
           .ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName))
           .ForMember(d => d.Name, o => o.MapFrom(s => s.FirstName))
           .ForMember(d => d.Surname, o => o.MapFrom(s => s.LastName))
           .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
           .ForMember(d => d.IdentityId, o => o.MapFrom(s => s.Id))
           .ForMember(d => d.Id, o => o.Ignore());
    }
}
