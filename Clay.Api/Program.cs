using Clay.Api.Middlewares;
using Clay.Application.Extensions;
using Clay.Infrastructure.ServicesExtension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddJwtAuthentication();
builder.Services.AddDatabaseConnection();
builder.Services.AddApplicationServices();
builder.Services.AddRepositories();

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
