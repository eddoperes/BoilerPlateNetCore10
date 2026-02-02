
using BoilerPlateNetCore10.API;

using BoilerPlateNetCore10.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

using BoilerPlateNetCore10.Infra.IoC;

using Microsoft.OpenApi;
using Scalar.AspNetCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddInfrastructureAPI(builder.Configuration);

using var log = new LoggerConfiguration()
    .WriteTo.File("logs/log-global-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Services.AddSingleton<Serilog.ILogger>(log);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new CustomExceptionFilter(log));
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BoilerPlateNetCore10.API",
        Version = "v1",
    });    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer + space + token"
    });
    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("bearer", document)] = []
    });   
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Aplica qualquer migração pendente e cria o banco se não existir
    dbContext.Database.Migrate();
}


app.UseExceptionHandler("/error");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{

    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapScalarApiReference();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



