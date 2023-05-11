using AutoMapper;

namespace BackendApis.Helper
{
    public static class AutoMapperService
    {
        public static T2 Map<T1, T2>(T1 parameter)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T1, T2>());
            var mapper = new Mapper(config);
            var dto = mapper.Map<T2>(parameter);
            return dto;
        }
       
    }
}
