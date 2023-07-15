using Core.CrossCuttingConcern.Exceptions.Middlewares;
using Portfolio.Persistance.Contexts;
using Portfolio.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddWebApiServices(builder.Configuration);

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DisplayRequestDuration();
        c.SwaggerEndpoint("/swagger/website/swagger.json", "Website API");
        c.SwaggerEndpoint("/swagger/panel/swagger.json", "Panel API");
    });
    app.ConfigureCustomExceptionMiddleware();
}
else
{
    app.ConfigureCustomExceptionMiddleware();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
