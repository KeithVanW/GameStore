using GameStore.Business.Services;
using GameStore.Data.Context;
using GameStore.Data.Entities;
using GameStore.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder);

ConfigureLogger(builder);

ConfigureJwt(builder);

UseCORS(builder);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

WebApplication app = builder.Build();

app.UseCors("webClient");
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureLogger(WebApplicationBuilder builder)
{
    IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

    builder.Host.UseSerilog();
}

void RegisterServices(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));
    builder.Services.AddScoped<IGameRepo, GameRepo>();
    builder.Services.AddScoped<ICartRepo, CartRepo>();
    builder.Services.AddScoped<ILibraryRepo, LibraryRepo>();

    builder.Services.AddScoped<IGameService, GameService>();
    builder.Services.AddScoped<ICartService, CartService>();
    builder.Services.AddScoped<ILibraryService, LibraryService>();

    builder.Services.AddAutoMapper(typeof(Program).Assembly);
    builder.Services.AddAutoMapper(typeof(GameService).Assembly);
}

void ConfigureJwt(WebApplicationBuilder builder)
{
    // For Identity
    builder.Services
        .AddIdentity<UserEntity, IdentityRole>()
        .AddEntityFrameworkStores<DataContext>()
        .AddDefaultTokenProviders();

    // Adding Authentication
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })

    // Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!))
        };
    });
}

void UseCORS(WebApplicationBuilder builder)
{
    builder.Services.AddCors(x => x
            .AddPolicy(name: "webClient", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }));
}