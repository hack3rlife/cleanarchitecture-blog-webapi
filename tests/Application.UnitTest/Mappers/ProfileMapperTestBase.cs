using AutoMapper;
using BlogWebApi.Application.Profiles;

namespace Application.UnitTest.Mappers
{
    public class ProfileMapperTestBase
    {
        public readonly IMapper _mapper;

        public ProfileMapperTestBase()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProfileMapper>();
            });

            _mapper = configurationProvider.CreateMapper();
        }
    }
}