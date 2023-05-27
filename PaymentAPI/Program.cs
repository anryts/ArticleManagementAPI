using Common.Middlewares;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Configuration;
using PaymentAPI.Configuration.ServicesConfiguration;
using PaymentAPI.Data;
using UserAPI.Configuration;

var services = WebApplication.CreateBuilder(args);
services.Services.AddControllers();
services.Services.ConfigureSwagger();
services.Services.ConfigureAuth(services.Configuration);
services.Services.AddEndpointsApiExplorer();
services.Services.AddSwaggerGen();
services.Services.MassTransitConfigure(services.Configuration);
services.Services.ConfigureOptions(services.Configuration);
services.Services.ConfigureAdditionalServices();
services.Services.SingletonServicesConfigure(services.Configuration);
services.Services.RegisterRepositories();
services.Services.ScopedServicesConfigure();
services.Services.AddMediatR(typeof(Program));
services.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(services.Configuration.GetConnectionString("AppDbContext")));
var app = services.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
}

app.Run();