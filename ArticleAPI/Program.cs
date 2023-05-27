using ArticleAPI.Configuration;
using ArticleAPI.Configuration.ServicesConfiguration;
using ArticleAPI.DailyJobs;
using ArticleAPI.Data; 
using FluentValidation;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AppDbContext")));
builder.Services.ConfigureOptions(builder.Configuration);
builder.Services.ConfigureMessageHandlers();
builder.Services.ScopedServicesConfigure();
builder.Services.SingletonServicesConfigure(builder.Configuration);
builder.Services.RegisterRepositories();
builder.Services.ConfigureSwagger();
builder.Services.AddScoped<SendNewArticlesToUsers>();
builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.ConfigureAdditionalServices();
builder.Services.ConfigureHangfire(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.MassTransitConfigure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHangfireDashboard("/dashboard");
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCaching();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<AppDbContext>()!;
    dbContext.Database.Migrate();
}
 
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var jobForSendNewArticlesToUsers = serviceProvider
        .GetRequiredService<SendNewArticlesToUsers>();
    jobForSendNewArticlesToUsers.Execute("0 12 * * *");
}

app.Run();