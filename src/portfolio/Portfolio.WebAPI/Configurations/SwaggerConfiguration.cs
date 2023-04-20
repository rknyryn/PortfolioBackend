using Microsoft.OpenApi.Models;

namespace Portfolio.WebAPI.Configurations;

public static class SwaggerConfiguration
{
    #region Methods

    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.SwaggerDoc("website", new OpenApiInfo
            {
                Version = "website",
                Title = "Portfolio WebAPI",
                Description = "",
                TermsOfService = new Uri("https://www.linkedin.com/in/rknyryn/"),
                Contact = new OpenApiContact
                {
                    Name = "Kaan Yarayan",
                    Email = "rknyryn@gmail.com"
                }
            });
            c.SwaggerDoc("panel", new OpenApiInfo
            {
                Version = "panel",
                Title = "Portfolio WebAPI",
                Description = "",
                TermsOfService = new Uri("https://www.linkedin.com/in/rknyryn/"),
                Contact = new OpenApiContact
                {
                    Name = "Kaan Yarayan",
                    Email = "rknyryn@gmail.com"
                }
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description =
                    "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        services.AddCors(opt =>
        {
            opt.AddPolicy(name: "CorsPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }

    #endregion Methods
}
