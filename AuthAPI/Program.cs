using APIGateway.Configuration;
using AuthAPI.Configuration;
using AuthAPI.Configuration.ServicesConfiguration;
using AuthAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureMessageHandlers();
builder.Services.AddControllers();
builder.Services.ConfigureSwagger();
builder.Services.RegisterRepositories();
builder.Services.ConfigureOptions(builder.Configuration);
builder.Services.ConfigureAdditionalServices();
builder.Services.SingletonServicesConfigure(builder.Configuration);
builder.Services.ScopedServicesConfigure();
builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.ConfigureMassTransit(builder.Configuration);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AppDbContext")));

var app = builder.Build();

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