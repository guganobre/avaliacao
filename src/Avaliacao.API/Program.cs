using Avaliacao.Infrastructure.Context;
using Avaliacao.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddCors();
services.AddControllers();
services.AddRouting(options => options.LowercaseUrls = true);

services.ConfigureServicesInfrastructure(builder.Configuration);
services.ConfigureServicesApplication();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//services.AddOpenApi();

// Adicionar Swagger
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

await app.Services.ExecuteMigrationsAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Avaliação API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Redirecionar raiz para Swagger
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger", permanent: false);
    return Task.CompletedTask;
});

app.MapControllers();

app.Run();