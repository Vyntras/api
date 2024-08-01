using Microsoft.EntityFrameworkCore;
using CardCom.Api.Data;
using CardCom.Api.Interfaces;
using CardCom.Api.Repositories;
using DotNetEnv;

Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);


var allowedOriginUrl = builder.Configuration["Cors:AllowedOrigins"];
Console.WriteLine($"Connection String: {allowedOriginUrl}");
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowedOrigin",
        policy =>
        {
            policy.WithOrigins(allowedOriginUrl!).AllowAnyHeader().AllowAnyMethod();
        });
});


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICardRepository, CardRepository>();

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

app.MapControllers();

app.Run();
