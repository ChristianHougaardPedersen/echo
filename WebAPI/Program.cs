using Application.DAOInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using Application.ProviderInterfaces;
using EfcDataAccess;
using EfcDataAccess.DAOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EchoContext>();
builder.Services.AddScoped<IUserDAO, UserEfcDAO>();
builder.Services.AddScoped<IUserProvider, UserEfcDAO>();
builder.Services.AddScoped<IUserLogic, UserLogic>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();