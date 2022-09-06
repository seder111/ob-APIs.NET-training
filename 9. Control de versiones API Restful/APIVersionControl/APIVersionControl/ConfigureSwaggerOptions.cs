using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace APIVersionControl
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {

        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }


        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Mi .Net Api Restful",
                Version = description.ApiVersion.ToString(),
                Description = "Mi primera api con versiones.",
                Contact = new OpenApiContact()
                {
                    Email = "sergiogonzalezmerino@gmailcom",
                    Name = "Sergio",
                }
            };

            if(description.IsDeprecated)
            {
                info.Version += " Esta versión is Deprecated";
            }

            return info;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach(var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
          
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
            
        }

       
    }
}
