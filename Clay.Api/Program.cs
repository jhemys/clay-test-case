using Clay.Api.Middlewares;
using Clay.Application.Extensions;
using Clay.Infrastructure.ServicesExtension;
using Microsoft.CodeAnalysis.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Clay API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string [] { }
        }
    });
});

builder.Services.AddApplicationServices();
builder.Services.AddRepositories();

var jwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "name=JwtSecret:Value";
builder.Services.AddJwtAuthentication(jwtSecretKey);

var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTIONSTRING") ?? "name=ConnectionStrings:ClayDbContext";
builder.Services.AddDatabaseConnection(connectionString);

builder.Services.AddSingleton<ILogger>(provider => provider.GetRequiredService<ILogger<ExceptionMiddleware>>());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Services.MigrateDatabase();

app.Run();
