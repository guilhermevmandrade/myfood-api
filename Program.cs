using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyFood.Data;
using MyFood.Data.Repositories;
using MyFood.Data.Repositories.Interfaces;
using MyFood.Middlewares;
using MyFood.Security;
using MyFood.Services;
using MyFood.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Adicionando configuração da conexão com o banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<DbSession>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

// Adicionando configuração do JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<JwtService>();

// Registrando repositorios
builder.Services.AddTransient<IFoodRepository, FoodRepository>();
builder.Services.AddTransient<IMealRepository, MealRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

// Registrando serviços
builder.Services.AddTransient<IFoodService, FoodService>();
builder.Services.AddTransient<IMealService, MealService>();
builder.Services.AddTransient<IUserService, UserService>();

// Configurando autenticação JWT
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings.Secret))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware(typeof(GlobalExceptionHandlerMiddleware));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
