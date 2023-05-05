using Contracts.Utils;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GeekBurguer_Promotion_Service.Contracts.SwaggerExclude
{
    public class SwaggerExcludeFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var excludedProperties = context.Type.GetProperties().Where(p => p.GetCustomAttributes(true).OfType<SwaggerExcludeAttribute>().Any());

            foreach (var excludedProperty in excludedProperties)
            {
                var excludedPropertyName = excludedProperty.Name.ToCamelCase();
                if (schema.Properties.ContainsKey(excludedPropertyName))
                {
                    schema.Properties.Remove(excludedPropertyName);
                }
            }
        }
    }
}
