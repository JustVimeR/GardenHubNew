using AutoMapper;
using Data.IdentityModels;
using Models.DbEntities;
using WebApi;

namespace GardenHub.Tests
{
    public class MapperTest
    {
        private readonly IMapper mapper;

        public MapperTest()
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void Test1()
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                FirstName = "nikita"
            };
            UserProfile userProfile = mapper.Map<UserProfile>(applicationUser);

            Assert.Equal(1, 1);

        }

        [Fact]
        public void Test2()
        {
            WorkType workType = new WorkType
            {
                Label = "tree planting",
                CreatedAt = DateTime.Now,
                CreatedBy = "nikita",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "nikita",
            };
            City city = mapper.Map<City>(workType);

            Assert.Equal(1, 1);

        }


    }
}