using API.Setup;
using Domain.Options;
using Infrastructure.Setup;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var secret = builder.Configuration.GetSection("Authentication:Secret").Value;
builder.Services.AddJwtAuthentication(secret);

builder.Services.Configure<Secrets>(builder.Configuration);

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenConfig();

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.RegisterServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseReDoc(config =>
{
    config.DocumentTitle = "ReDoc";
    config.SpecUrl = "/swagger/v1/swagger.json";
});

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
