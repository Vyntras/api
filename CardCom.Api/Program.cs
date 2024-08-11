using Microsoft.EntityFrameworkCore;
using CardCom.Api.Data;
using CardCom.Api.Interfaces;
using CardCom.Api.Repositories;
using DotNetEnv;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);


var allowedOriginUrl = builder.Configuration["Cors:AllowedOrigins"];

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowedOrigin",
        policy =>
        {
            policy.WithOrigins(allowedOriginUrl!).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
});


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddControllers();

var GoogleAuthClientId = builder.Configuration["Authentication:Google:ClientId"];
var GoogleAuthSecret = builder.Configuration["Authentication:Google:ClientSecret"];

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options => {
    options.ClientId = GoogleAuthClientId!;
    options.ClientSecret = GoogleAuthSecret!;
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors("AllowedOrigin");

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
