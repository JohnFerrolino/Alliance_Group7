using AutoMapper;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Data.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BaseCode.API
{
    public partial class Startup
    {
        private void ConfigureMapper(IServiceCollection services)
        {
            var Config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Applicant, Applicant>();
                cfg.CreateMap<ApplicantViewModel, Applicant>();

                cfg.CreateMap<Position, PositionViewModel>()
                .ForMember(dest => dest.PositionRequirements, opt => opt.MapFrom(src => src.PositionRequirements.Select(res => res.Description)));
                cfg.CreateMap<PositionViewModel, Position>()
                .ForMember(dest => dest.PositionRequirements, opt => opt.MapFrom(src => src.PositionRequirements.Select(res => new PositionRequirements { Description = res })));

                cfg.CreateMap<PositionRequirements, PositionRequirementsViewModel>();
                cfg.CreateMap<PositionRequirementsViewModel, PositionRequirements>();

                cfg.CreateMap<Status, StatusViewModel>();
                cfg.CreateMap<StatusViewModel, Status>();
            });

            services.AddSingleton(Config.CreateMapper());
        }
    }
}