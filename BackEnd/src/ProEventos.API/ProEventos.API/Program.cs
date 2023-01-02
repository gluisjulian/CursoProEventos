using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Interface;
using ProEventos.Application.Interface.Implementations;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Interface;
using ProEventos.Persistence.Interface.Implementations;

var builder = WebApplication.CreateBuilder(args);


//Database
builder.Services.AddDbContext<ProEventosContext>(context => context.UseSqlServer(builder.Configuration.GetConnectionString("ProEventos")));


//Dependecy Injection
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IEventoPersistence, EventoPersistence>();
builder.Services.AddScoped<IGeralPersistence, GeralPersistence>();



builder.Services.AddControllers()
    .AddNewtonsoftJson(
        x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


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

app.UseAuthorization();

app.MapControllers();

app.Run();
