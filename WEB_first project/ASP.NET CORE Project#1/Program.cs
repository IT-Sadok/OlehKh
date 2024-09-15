using Microsoft.EntityFrameworkCore;
using ASP.NET_CORE_Project_1.Data;
using ASP.NET_CORE_Project_1.Models;
using ASP.NET_CORE_Project_1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ASP.NET_CORE_Project_1.Services;
using ASP.NET_CORE_Project_1.Extensions;
using ASP.NET_CORE_Project_1.DTO;
using Microsoft.AspNetCore.Authorization;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(name:"Database"));
});


builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityConfiguration();
builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapPost("/api/account/register", async ([FromBody] SignUpModel model, IRegistrationService registrationService, IJwtTokenService jwtTokenService) =>
{
    var result = await registrationService.RegisterUserAsync(model);
    if (result.IsSuccess)
    {
        var token = await jwtTokenService.GenerateJwtTokenAsync(result.User);
        return Results.Ok(new { Token = token });
    }
    return Results.BadRequest(result.Errors);
});

app.MapPost("/api/account/login", async ([FromBody] SignInModel model, ILoginService loginService, IJwtTokenService jwtTokenService) =>
{
    var result = await loginService.LoginUserAsync(model);
    if (result.IsSuccess)
    {
        var token = await jwtTokenService.GenerateJwtTokenAsync(result.User);
        return Results.Ok(new { Token = token });
    }
    return Results.BadRequest(result.Errors);
});

app.MapGet("/api/account/me", [Authorize] async (HttpContext context) =>
{
    var userName = context.User.Identity?.Name;
    return Results.Ok(new { UserName = userName });
});


app.Run();
