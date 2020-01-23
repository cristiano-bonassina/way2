using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Way2.Presentation.WebApi.Conventions
{
    public class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new OpenApiPaths();
            foreach (var swaggerDocPath in swaggerDoc.Paths)
            {
                paths.Add(swaggerDocPath.Key.Replace("v{version}", swaggerDoc.Info.Version), swaggerDocPath.Value);
            }
            swaggerDoc.Paths = paths;
        }
    }
}
