using Microsoft.EntityFrameworkCore;
using UserAPI.Configuration;
using UserAPI.Configuration.ServicesConfiguration;
using UserAPI.Data;

var services = WebApplication.CreateBuilder(args);
services.Services.ConfigureOptions(services.Configuration);
services.Services.RegisterRepositories();
services.Services.ConfigureSwagger();
services.Services.ConfigureMessageHandlers();
services.Services.ConfigureAdditionalServices();
services.Services.ConfigureHangfire(services.Configuration);
services.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(services.Configuration.GetConnectionString("AppDbContext")));
services.Services.MassTransitConfigure(services.Configuration);
services.Services.ConfigureMessageHandlers();
services.Services.ScopedServicesConfigure();
services.Services.SingletonServicesConfigure(services.Configuration);

services.Services.ConfigureAuth(services.Configuration);

var app = services.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<AppDbContext>()!;
    dbContext.Database.Migrate();
}

app.Run();