using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace BaseCode.API
{
    public partial class Startup
    {
        private void ConfigureMVC(IServiceCollection services)
        {
            services.AddMvc().AddWebApiConventions();
            services.AddMvc()
           .AddJsonOptions(options =>
           {
               options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
               options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
           });
        }
    }
}
