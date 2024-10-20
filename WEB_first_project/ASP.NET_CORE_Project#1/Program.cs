using Microsoft.EntityFrameworkCore;
using ASP.NET_CORE_Project_1.Data;
using ASP.NET_CORE_Project_1.Models;
using Microsoft.AspNetCore.Identity;
using ASP.NET_CORE_Project_1.Extensions;
using ASP.NET_CORE_Project_1.Infrastructure;
using AutoMapper;
using ASP.NET_CORE_Project_1.Mappings;
using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ASP.NET_CORE_Project_1.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Logging;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(name:"Database"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityConfiguration();

builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await SeedRoles.Initialize(services);
        logger.LogInformation("Roles initialized successfully");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error during roles initialization");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty;
    });
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();