using Api.Utils;
using CrossCutting.Configuration;
using CrossCutting.Configuration.Authorization;
using CrossCutting.Exceptions.Middlewares;
using Domain.Commands.v1.Jogos.AtualizarJogo;
using Domain.Commands.v1.Jogos.BuscarJogo;
using Domain.Commands.v1.Jogos.ListarJogos;
using Domain.Commands.v1.Jogos.CriarJogo;
using Domain.Commands.v1.Jogos.RemoverJogo;
using Domain.Commands.v1.Login;
using Domain.Enums;
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
using System.Text;
using Domain.Commands.v1.Usuarios.ListarUsuarios;
using Domain.Commands.v1.Usuarios.CriarUsuario;
using Domain.Commands.v1.Usuarios.BuscarUsuarioPorId;
using Domain.Commands.v1.Usuarios.AtualizarUsuario;
using Domain.Commands.v1.Usuarios.RemoverUsuario;
using Infrastructure.Data.Interfaces.Usuarios;
using Infrastructure.Data.Repositories.Usuarios;

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
    .AddPolicy(PoliticasDeAcesso.Admin, policy => policy.RequireRole(PerfilUsuario.Administrador.ToString()))
    .AddPolicy(PoliticasDeAcesso.SomenteUsuario, policy => policy.RequireRole(PerfilUsuario.Usuario.ToString()))
    .AddPolicy(PoliticasDeAcesso.Usuario, policy => policy.RequireRole(PerfilUsuario.Usuario.ToString(), PerfilUsuario.Administrador.ToString()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new()
    {
        Description = "Insira apenas o token JWT abaixo.",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new()
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

#region MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly));

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(CriarJogoCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(AtualizarJogoCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(RemoverJogoCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ListarJogosCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(BuscarJogoPorIdCommandHandler).Assembly));

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(CriarUsuarioCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(AtualizarUsuarioCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(RemoverUsuarioCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ListarUsuariosCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(BuscarUsuarioPorIdCommandHandler).Assembly));
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(JogoProfile));
builder.Services.AddAutoMapper(typeof(UsuarioProfile));
#endregion

#region Validators
builder.Services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();

builder.Services.AddScoped<IValidator<CriarJogoCommand>, CriarJogoCommandValidator>();
builder.Services.AddScoped<IValidator<AtualizarJogoCommand>, AtualizarJogoCommandValidator>();
builder.Services.AddScoped<IValidator<RemoverJogoCommand>, RemoverJogoCommandValidator>();
builder.Services.AddScoped<IValidator<BuscarJogoPorIdCommand>, BuscarJogoPorIdCommandValidator>();
builder.Services.AddScoped<IValidator<ListarJogosCommand>, ListarJogosCommandValidator>();

builder.Services.AddScoped<IValidator<CriarUsuarioCommand>, CriarUsuarioCommandValidator>();
builder.Services.AddScoped<IValidator<AtualizarUsuarioCommand>, AtualizarUsuarioCommandValidator>();
builder.Services.AddScoped<IValidator<RemoverUsuarioCommand>, RemoverUsuarioCommandValidator>();
builder.Services.AddScoped<IValidator<BuscarUsuarioPorIdCommand>, BuscarUsuarioPorIdCommandValidator>();
#endregion

builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<ICriptografiaService, CriptografiaService>();
builder.Services.AddScoped<IJogoRepository, JogoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

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