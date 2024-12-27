using Application.Application;
using Application.Interface;
using Domain.Interface;
using Domain.Interface.InterfaceService;
using Domain.Service;
using Entities.Entity;
using Infrastructure.Repository;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared;
using WebAPI.Token;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configurando o DbContext com Banco de Dados
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Infrastructure"));
});

builder.Services.AddIdentity<UserEntity, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Interface e Repositorios
builder.Services.AddScoped<INews, NewsRepository>();
builder.Services.AddScoped<IUser, UserRepository>();

// Interface e Serviço
builder.Services.AddScoped<INewsService, NewsService>();

// Interface Aplicação
builder.Services.AddScoped<INewsApplication, NewsApplication>();
builder.Services.AddScoped<IUserApplication, UserApplication>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = Const.TestSecurityBearer,
            ValidAudience = Const.TestSecurityBearer,
            IssuerSigningKey = JWTSecurityKey.Create(Const.SecretKeyToken)
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"OnAuthenticationFailed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine($"OnTokenValidated: {context.SecurityToken}");
                return Task.CompletedTask;
            }
        };
    });




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
