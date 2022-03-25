namespace YoumaconSecurityOps.Core.AutoMapper.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMappingServices(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new YoumaAutoMappingProfile());
            });

            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);


            return services;
        }
    }
}
