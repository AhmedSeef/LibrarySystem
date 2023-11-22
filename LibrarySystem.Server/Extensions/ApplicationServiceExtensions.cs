using LibrarySystem.Application;

namespace LibrarySystem.Server.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            return AppStartUp.AddApplicationServices(services, config);
        }
    }
}
