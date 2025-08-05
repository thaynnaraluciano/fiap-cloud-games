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
using Domain.Commands.v1.Promocoes.AtualizarPromocao;
using Domain.Commands.v1.Promocoes.BuscarPromocaoPorId;
using Domain.Commands.v1.Promocoes.CriarPromocao;
using Domain.Commands.v1.Promocoes.ListarPromocoes;
using Domain.Commands.v1.Promocoes.RemoverPromocao;
using Domain.Enums;
using Domain.MapperProfiles;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Data.Interfaces.Jogos;
using Infrastructure.Data.Interfaces.Promocoes;
using Infrastructure.Data.Repositories.Jogos;
using Infrastructure.Data.Repositories.Promocoes;
using Infrastructure.Services.Interfaces.v1;
using Infrastructure.Services.Services.v1;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Data.Interfaces.Usuarios;
using Infrastructure.Data.Repositories.Usuarios;
using CrossCutting.Configuration.Extensoes;
using Domain.Commands.v1.Usuarios.CriarUsuario;
using Domain.Commands.v1.Usuarios.CriarSenha;
using Domain.Commands.v1.Usuarios.ListarUsuarios;
using Domain.Commands.v1.Usuarios.RemoverUsuario;
using Domain.Commands.v1.Usuarios.AtualizarUsuario;
using Domain.Commands.v1.Usuarios.BuscarUsuarioPorId;
using Domain.Commands.v1.Usuarios.AlterarStatusUsuario;
using Domain.Commands.v1.Notificacao.Email;

using Domain.Commands.v1.Biblioteca.ConsultaBiblioteca;
using Infrastructure.Data.Interfaces.Biblioteca;
using Infrastructure.Data.Repositories.Biblioteca;
using Domain.Commands.v1.Biblioteca.ComprarJogo;

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
    .AddPolicy(PoliticasDeAcesso.Admin, policy => policy.RequireRole(PerfilUsuarioEnum.Administrador.ObterDescricao()))
    .AddPolicy(PoliticasDeAcesso.SomenteUsuario, policy => policy.RequireRole(PerfilUsuarioEnum.Usuario.ObterDescricao()))
    .AddPolicy(PoliticasDeAcesso.Usuario, policy => policy.RequireRole(PerfilUsuarioEnum.Usuario.ObterDescricao(), PerfilUsuarioEnum.Administrador.ObterDescricao()))
    .AddPolicy(PoliticasDeAcesso.CriarSenha, policy => policy.RequireRole("CriarSenha"));

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
// Login
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly));
// Jogos
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(CriarJogoCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(AtualizarJogoCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(RemoverJogoCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ListarJogosCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(BuscarJogoPorIdCommandHandler).Assembly));
//Usuarios
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(CriarUsuarioCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(AtualizarUsuarioCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(RemoverUsuarioCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ListarUsuariosCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(BuscarUsuarioPorIdCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(AlterarStatusUsuarioCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(CriarSenhaCommandHandler).Assembly));
// Promoções
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(CriarPromocaoCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(AtualizarPromocaoCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(RemoverPromocaoCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ListarPromocoesCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(BuscarPromocaoPorIdCommandHandler).Assembly));
// Notificação
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(EnviarEmailCommandHandler).Assembly));
// Biblioteca
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ConsultaBibliotecaCommandHandler).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ComprarJogoCommandHandler).Assembly));

#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(JogoProfile));
builder.Services.AddAutoMapper(typeof(UsuarioProfile));
builder.Services.AddAutoMapper(typeof(PromocaoProfile));
builder.Services.AddAutoMapper(typeof(EmailProfile));
builder.Services.AddAutoMapper(typeof(BibliotecaProfile));
#endregion

#region Validators
// Login
builder.Services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
// Jogos
builder.Services.AddScoped<IValidator<CriarJogoCommand>, CriarJogoCommandValidator>();
builder.Services.AddScoped<IValidator<AtualizarJogoCommand>, AtualizarJogoCommandValidator>();
builder.Services.AddScoped<IValidator<RemoverJogoCommand>, RemoverJogoCommandValidator>();
builder.Services.AddScoped<IValidator<BuscarJogoPorIdCommand>, BuscarJogoPorIdCommandValidator>();
builder.Services.AddScoped<IValidator<ListarJogosCommand>, ListarJogosCommandValidator>();
// Promoções
builder.Services.AddScoped<IValidator<CriarPromocaoCommand>, CriarPromocaoCommandValidator>();
builder.Services.AddScoped<IValidator<AtualizarPromocaoCommand>, AtualizarPromocaoCommandValidator>();
builder.Services.AddScoped<IValidator<RemoverPromocaoCommand>, RemoverPromocaoCommandValidator>();
builder.Services.AddScoped<IValidator<ListarPromocoesCommand>, ListarPromocoesCommandValidator>();
builder.Services.AddScoped<IValidator<BuscarPromocaoPorIdCommand>, BuscarPromocaoPorIdCommandValidator>();
// Usuário
builder.Services.AddScoped<IValidator<CriarUsuarioCommand>, CriarUsuarioCommandValidator>();
builder.Services.AddScoped<IValidator<AtualizarUsuarioCommand>, AtualizarUsuarioCommandValidator>();
builder.Services.AddScoped<IValidator<RemoverUsuarioCommand>, RemoverUsuarioCommandValidator>();
builder.Services.AddScoped<IValidator<BuscarUsuarioPorIdCommand>, BuscarUsuarioPorIdCommandValidator>();
builder.Services.AddScoped<IValidator<AlterarStatusUsuarioCommand>, AlterarStatusUsuarioCommandValidator>();
builder.Services.AddScoped<IValidator<ListarUsuariosCommand>, ListarUsuariosCommandValidator>();
builder.Services.AddScoped<IValidator<CriarSenhaCommand>, CriarSenhaCommandValidator>();
// Notificação
builder.Services.AddScoped<IValidator<EnviarEmailCommand>, EnviarEmailCommandValidator>();
// Biblioteca
builder.Services.AddScoped<IValidator<ConsultaBibliotecaCommand>, ConsultaBibliotecaCommandValidator>();
builder.Services.AddScoped<IValidator<ComprarJogoCommand>, ComprarJogoCommandValidator>();
#endregion

#region Interfaces
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<ICriptografiaService, CriptografiaService>();
builder.Services.AddSingleton<IEmailTemplateService, EmailTemplateService>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddScoped<IJogoRepository, JogoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPromocaoRepository, PromocaoRepository>();
builder.Services.AddScoped<IBibliotecaRepository, BibliotecaRepository>();
#endregion

builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

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