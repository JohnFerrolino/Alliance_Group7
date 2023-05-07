using BaseCode.API.Authentication;
using BaseCode.Data;
using BaseCode.Data.Contracts;
using BaseCode.Data.Repositories;
using BaseCode.Domain.Contracts;
using BaseCode.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BaseCode.API
{
    public partial class Startup
    {
        private void ConfigureDependencies(IServiceCollection services)
        {            
            // Common
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ClaimsProvider, ClaimsProvider>();

            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IApplicantService, ApplicantService>();
             services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IStatusService, StatusService>();            

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IApplicantRepository, ApplicantRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();


        }
    }
}
