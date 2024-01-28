using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.Interfaces;

namespace Services.Concrete
{
    public class MediaService : Service<Media>, IMediaService
    {
        public MediaService(IMediaRepository repository) : base(repository)
        {
        }
    }
}
