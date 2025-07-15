using Domain.Commands.v1.Jogos.AtualizarJogo;
using Domain.Commands.v1.Jogos.BuscarJogo;
using Domain.Commands.v1.Jogos.BuscarTodosJogosCommand;
using Domain.Commands.v1.Jogos.CriarJogo;
using Domain.Commands.v1.Jogos.RemoverJogo;
using Domain.MapperProfiles;
using FluentValidation;
using Infrastructure.Data.Interfaces.Jogos;
using Infrastructure.Data.Repositories.Jogos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(CriarJogoCommand).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(AtualizarJogoCommand).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(RemoverJogoCommand).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ListarJogosCommand).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(BuscarJogoPorIdCommand).Assembly));
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(JogoProfile));
#endregion

#region Validators
builder.Services.AddScoped<IValidator<CriarJogoCommand>, CriarJogoCommandValidator>();
builder.Services.AddScoped<IValidator<AtualizarJogoCommand>, AtualizarJogoCommandValidator>();
builder.Services.AddScoped<IValidator<RemoverJogoCommand>, RemoverJogoCommandValidator>();
builder.Services.AddScoped<IValidator<BuscarJogoPorIdCommand>, BuscarJogoPorIdCommandValidator>();
#endregion

builder.Services.AddScoped<IJogoRepository, JogoRepository>();

//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
