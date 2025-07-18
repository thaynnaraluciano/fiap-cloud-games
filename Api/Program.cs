using Api.Utils;
using CrossCutting.Configuration;
using CrossCutting.Exceptions.Middlewares;
using Domain.Commands.v1.Jogos.AtualizarJogo;
using Domain.Commands.v1.Jogos.BuscarJogo;
using Domain.Commands.v1.Jogos.BuscarTodosJogosCommand;
using Domain.Commands.v1.Jogos.CriarJogo;
using Domain.Commands.v1.Jogos.RemoverJogo;
using Domain.Commands.v1.Login;
using Domain.MapperProfiles;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Data.Interfaces.Jogos;
using Infrastructure.Data.Repositories.Jogos;
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
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(CriarJogoCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(AtualizarJogoCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(RemoverJogoCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ListarJogosCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(BuscarJogoPorIdCommandHandler).Assembly));
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(JogoProfile));
#endregion

#region Validators
builder.Services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
builder.Services.AddScoped<IValidator<CriarJogoCommand>, CriarJogoCommandValidator>();
builder.Services.AddScoped<IValidator<AtualizarJogoCommand>, AtualizarJogoCommandValidator>();
builder.Services.AddScoped<IValidator<RemoverJogoCommand>, RemoverJogoCommandValidator>();
builder.Services.AddScoped<IValidator<BuscarJogoPorIdCommand>, BuscarJogoPorIdCommandValidator>();
#endregion

builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<ICriptografiaService, CriptografiaService>();
builder.Services.AddScoped<IJogoRepository, JogoRepository>();

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