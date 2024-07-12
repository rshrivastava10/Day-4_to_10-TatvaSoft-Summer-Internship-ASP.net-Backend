using Business_Logic_Layer;
using Business_Logic_Layer.JWTService;
using Data_Logic_Layer;
using Data_Logic_Layer.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(db => db.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<BALLogin>();
builder.Services.AddScoped<DALLogin>();
builder.Services.AddScoped<BALAdminUser>();
builder.Services.AddScoped<DALAdminUser>();
builder.Services.AddScoped<JwtService>();

// Register the DALMission and BALMission services
builder.Services.AddScoped<IMission, DALMission>();
builder.Services.AddScoped<BALMission>();

// Register the DALMissionTheme and BALMissionTheme services
builder.Services.AddScoped<IMissionTheme, DALMissionTheme>();
builder.Services.AddScoped<BALMissionTheme>();

// Register the DALMissionSkill and BALMissionSkill services
builder.Services.AddScoped<IMissionSkill, DALMissionSkill>();
builder.Services.AddScoped<BALMissionSkill>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "localhost",
            ValidAudience = "localhost",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    }
);

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt Token authorize",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();  // Ensure the authentication middleware is used
app.UseAuthorization();

app.MapControllers();

app.Run();
