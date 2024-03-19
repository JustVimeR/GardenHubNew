using AutoMapper;
using Data.IdentityModels;
using Models;
using Models.DbEntities;
using Models.DTOs.Account;
using Models.DTOs.GetDTOs;
using Models.DTOs.PostDTOs;
using System.Linq;
using System.Reflection;

namespace WebApi;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PaginationQuery, PaginationFilter>();
        CreateMap<SortQuery, SortFilter>();
        CreateMap<SearchQuery, SearchFilter>();

        CreateMap<PostIdDTO, WorkType>().ReverseMap();

        var types = Assembly.GetAssembly(typeof(EntityBase))!.GetTypes();
        var derivedTypes = types.Where(t => t.IsSubclassOf(typeof(EntityBase)));

        foreach (var type in derivedTypes)
        {
            CreateMap(type, type)
                .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember == default));
        }

        CreateMap<City, City>();
        CreateMap<UserProfile, UserProfile>()
            .ForMember(d => d.UserName, o => o.Ignore())
            .ForMember(d => d.Email, o => o.Ignore());
        CreateMap<WorkType, WorkType>();
        CreateMap<Project, Project>()
            .ForMember(d => d.CustomerId, o => o.Ignore())
            .ForMember(d => d.Customer, o => o.Ignore());

        CreateMap<PostCityDTO, City>().ReverseMap();
        CreateMap<PostFeedbackDTO, Feedback>().ReverseMap();
        CreateMap<PostMediaDTO, Media>().ReverseMap();
        CreateMap<PostProjectDTO, Project>().ReverseMap();
        CreateMap<PostUserProfileDTO, UserProfile>().ReverseMap();

        CreateMap<PostWorkTypeDTO, WorkType>().ReverseMap();
        CreateMap<PostDerivedWorkTypeDTO, WorkType>().ReverseMap();
        CreateMap<WorkType, PostWorkTypeDTO>();

        CreateMap<City, GetCityDTO>();
        CreateMap<Feedback, GetFeedbackDTO>()
            .ForMember(d => d.CustomerName, o => o.MapFrom(s => s.Customer.UserName))
            .ForMember(d => d.ProjectTitle, o => o.MapFrom(s => s.Project.Title))
            .ForPath(dest => dest.GardenerName, o => o.MapFrom(src => src.Gardener.UserName));


        CreateMap<Media, GetMediaDTO>();
        CreateMap<Project, GetProjectDTO>()
            .ForMember(d => d.CustomerName, o => o.MapFrom(s => s.Customer.UserName));
        CreateMap<UserProfile, GetUserProfileDTO>();
        CreateMap<WorkType, GetWorkTypeDTO>();
        CreateMap<WorkType, GetDerivedWorkTypeDTO>();

        CreateMap<ApplicationUser, UserDto>()
            .ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName))
            .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Email));


        CreateMap<IEntityBase, City>().ForMember(d => d.Id, o => o.Ignore());
        CreateMap<IEntityBase, Feedback>().ForMember(d => d.Id, o => o.Ignore());
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
