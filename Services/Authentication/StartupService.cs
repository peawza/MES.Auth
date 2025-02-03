using Authentication.Repositories;
using Authentication.Services;
using Utils.Services;

namespace Authentication
{
    public class StartupService
    {
        public static void InitialService(IServiceCollection services)
        {
            /* --- Repositories --- */
            services.AddTransient<IApplicationRepository, ApplicationRepository>();
            services.AddTransient<IEmailService, EmailService>();
            //EmailService : IEmailService
            services.AddTransient<ISystemRepository, SystemRepository>();
            services.AddTransient<ISSS030Repository, SSS030Repository>();
            services.AddTransient<ISSS040Repository, SSS040Repository>();
            services.AddTransient<ISSS050Repository, SSS050Repository>();
            services.AddTransient<ISSS060Repository, SSS060Repository>();
            services.AddTransient<ISSS070Repository, SSS070Repository>();
            services.AddTransient<ISSS091Repository, SSS091Repository>();


            services.AddTransient<IResourcesRepository, ResourcesRepository>();

            /* --- Services --- */
            services.AddTransient<ISystemService, SystemService>();
            services.AddTransient<ISSS030Service, SSS030Service>();
            services.AddTransient<ISSS040Service, SSS040Service>();
            services.AddTransient<ISSS050Service, SSS050Service>();
            services.AddTransient<ISSS060Service, SSS060Service>();
            services.AddTransient<ISSS070Service, SSS070Service>();
            services.AddTransient<ISSS091Service, SSS091Service>();


            services.AddTransient<IResouresService, ResouresService>();
        }
    }
}
