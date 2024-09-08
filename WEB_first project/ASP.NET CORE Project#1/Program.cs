using Microsoft.EntityFrameworkCore;
using ASP.NET_CORE_Project_1.Data;
using ASP.NET_CORE_Project_1.Models;
using ASP.NET_CORE_Project_1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ASP.NET_CORE_Project_1.Services;
using ASP.NET_CORE_Project_1.Extensions;



var builder = WebApplication.CreateBuilder(args); // new object WebApplicationBuilder to config new app

builder.Services.AddControllers(); // add support to work with controlers

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); // support API manipulations
builder.Services.AddSwaggerGen(); // support API manipulations

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(name:"Database"));
});

// add settings of Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityConfiguration();
builder.Services.AddAuthenticationAndAuthorization();


var app = builder.Build(); // creating an object of WEBAPPLICATION with all configs above


if (app.Environment.IsDevelopment()) // checking if ENV is Dev and covers an appropriate Swagger
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // redirecting all requests with https

app.UseAuthentication();  // checking authentication of user
app.UseAuthorization(); // checking authorization of user

app.MapControllers(); // allocate controlers between requests by my parametres

app.Run(); //  starting an app
