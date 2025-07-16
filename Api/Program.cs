using Api.Utils;
using CrossCutting.Configuration;
using CrossCutting.Configuration.AppModels;
using CrossCutting.Exceptions.Middlewares;
using Domain.Commands.v1.Login;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Services.Interfaces.v1;
using Infrastructure.Services.Services.v1;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Emissor"],
            ValidAudience = builder.Configuration["Jwt:Audiencia"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Chave"]!))
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("PoliticaDeAdmin", policy =>
        policy.RequireRole("Administrador"))
    .AddPolicy("PoliticaDeUsuario", policy =>
        policy.RequireRole("Usuario"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

#region MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly));
#endregion

#region Validators
builder.Services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
#endregion

builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<ICriptografiaService, CriptografiaService>();

builder.Services.Configure<AppSettings>(builder.Configuration);

// TO DO: configs de banco
builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<MiddlewareTratamentoDeExcecoes>();

app.Run();