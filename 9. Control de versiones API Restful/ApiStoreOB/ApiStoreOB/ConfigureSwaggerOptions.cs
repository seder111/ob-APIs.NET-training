using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiStoreOB
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
                Title = "API Store",
                Version = description.ApiVersion.ToString(),
                Description = "Practica de API que consume la fakestoreapi.com.",
                Contact = new OpenApiContact()
                {
                    Email = "sergiogonzalezmerino@gmail.com",
                    Name = "Sergio González",
                }
            };

            if (description.IsDeprecated)
            {
                info.Version += " Esta versión está obsoleta y no tiene mantenimiento.";
            }

            return info;
        }
       

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
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
